using Dapper;
using RobinBradley2021.Models;
using RobinBradley2021.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RobinBradley2021.DataAccess.SqlConnection;


namespace RobinBradley2021.DataAccess
{
    class EquipmentRepository
    {
        static readonly string database = "WorkDeskDB";

        //GET DATA
        public static async Task<ObservableCollection<EquipmentAssignmentRecordModel>> GetEquipmentAssignments()
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString(database)))
            {
                var assignments = new Dictionary<int, EquipmentAssignmentRecordModel>();
                await connection.QueryAsync<EquipmentAssignmentRecordModel>("dbo.AnotherTry", new[]
                    {
                    typeof(EquipmentAssignmentRecordModel),
                    typeof(JobsiteModel),
                    typeof(EmployeeModel),
                    typeof(EquipmentAssignmentLineItemModel),
                    typeof(EquipmentModel)},
                    obj =>
                    {
                        EquipmentAssignmentRecordModel _record = obj[0] as EquipmentAssignmentRecordModel;
                        JobsiteModel _jobsite = obj[1] as JobsiteModel;
                        EmployeeModel _employee = obj[2] as EmployeeModel;
                        EquipmentAssignmentLineItemModel _lineItem = obj[3] as EquipmentAssignmentLineItemModel;
                        EquipmentModel _equipment = obj[4] as EquipmentModel;

                        var _recordEntity = new EquipmentAssignmentRecordModel();
                        var _lineItemCollection = new ObservableCollection<EquipmentAssignmentLineItemModel>();
                        if (!assignments.TryGetValue(_record.Id, out _recordEntity))
                        {
                            assignments.Add(_record.Id, _recordEntity = _record);
                        }

                        if (_recordEntity.LineItems == null)
                        {
                            _recordEntity.LineItems = new ObservableCollection<EquipmentAssignmentLineItemModel>();
                        }
                        if(!_recordEntity.LineItems.Any(x => x.Id == _lineItem.Id))
                        {
                            _lineItem.Equipment = _equipment;
                            _recordEntity.LineItems.Add(_lineItem);
                        }    

                        if (_recordEntity.Jobsite == null)
                        {
                            _recordEntity.Jobsite = _jobsite;
                        }
                        if (_recordEntity.AssigneePerson == null)
                        {
                            _recordEntity.AssigneePerson = _employee;
                        }

                        return _recordEntity;
                    });

                var result = assignments.Values.ToList();
                var assignmentsCollection = new ObservableCollection<EquipmentAssignmentRecordModel>(result);
        
                            
                
                return assignmentsCollection;
            }
        }
        public static async Task<ObservableCollection<EquipmentClassModel>> EquipmentClassQueryAsync()
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString(database)))
            {
                var equipmentClasses = await connection.QueryAsync<EquipmentClassModel>("dbo.spGetEquipmentClasses_All", commandType: System.Data.CommandType.StoredProcedure);
                var result = equipmentClasses.ToList();
                var equipmentClassesCollection = new ObservableCollection<EquipmentClassModel>(result);
                return equipmentClassesCollection;
            }
        }
        public async static Task<ObservableCollection<EquipmentModel>> EquipmentQueryAsync()
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString(database)))
            {
                var equipment = new Dictionary<int, EquipmentModel>();
                await connection.QueryAsync("dbo.spGetEquipmentData_All", new[]
            {
                    typeof(EquipmentModel),
            }
                , obj =>
                {
                    var equipmentModel = obj[0] as EquipmentModel;
                    var equipmentEntity = new EquipmentModel();
                    if (!equipment.TryGetValue(equipmentModel.Id, out equipmentEntity))
                    {
                        equipment.Add(equipmentModel.Id, equipmentEntity = equipmentModel);
                    }

                    return equipmentEntity;
                }); ;
                var result = equipment.Values.ToList();
                var equipmentCollection = new ObservableCollection<EquipmentModel>(result);
                return equipmentCollection;
            }
        }

        //SEND DATA
        public static EquipmentModel CreateItem(EquipmentModel newItem)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString("WorkDeskDB")))
            {
                var p = new DynamicParameters();
                p.Add("@InventoryId", newItem.InventoryId);
                p.Add("@Description", newItem.Description);
                p.Add("@ClassId", newItem.Class.Id);
                p.Add("@Brand", newItem.Brand);
                p.Add("@PurchaseDate", newItem.PurchaseDate);
                p.Add("@StatusId", newItem.Status.Id);
                p.Add("@Price", newItem.Price);
                p.Add("@Model", newItem.Model);
                p.Add("@SerialNumber", newItem.SerialNumber);
                p.Add("@WarrantyMonths", newItem.WarrantyMonths);
                p.Add("@CICRequired", false);
                p.Add("@id", 0, dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                connection.Execute("dbo.spEquipment_Insert", p, commandType: System.Data.CommandType.StoredProcedure);
                newItem.Id = p.Get<int>("@id");

                return newItem;
            }
        }
        public static void CreateEquipmentAssignmentRecord(EquipmentAssignmentRecordModel newAssignment)
        {
            //foreach (EquipmentModel item in newAssignment.SelectedItems)
            //{
            //    using (var connection = new System.Data.SqlClient.SqlConnection(CnnString("WorkDeskDB")))
            //    {
            //        var p = new DynamicParameters();
            //        p.Add("@EmployeeId", newAssignment.AssigneePerson.Id);
            //        p.Add("@EquipmentId", item.Id);
            //        if (newAssignment.Id == 0)
            //        {
            //            p.Add("@DepartmentId", null);
            //        }
            //        else
            //        {
            //            p.Add("@DepartmentId", newAssignment.AssigneeDepartment.Id);
            //        }
            //        p.Add("@DateOut", newAssignment.DateOut);
            //        p.Add("@IsStandardIssue", newAssignment.IsStandardIssue);
            //        p.Add("@IsDepartment", newAssignment.IsDepartment);
            //        if (newAssignment.IsStandardIssue == false)
            //        {
            //            p.Add("@DueDate", newAssignment.DueDate);
            //            p.Add("@JobsiteId", newAssignment.Jobsite);
            //        }
            //        else
            //        {
            //            p.Add("@DueDate", null);
            //            p.Add("@JobsiteId", null);
            //        }
            //        connection.Execute("dbo.spEquipmentAssignmentRecord_Insert", p, commandType: System.Data.CommandType.StoredProcedure);
            //    }
            //}
        }

        //EDIT DATA

        //DELETE DATA

    }
}
