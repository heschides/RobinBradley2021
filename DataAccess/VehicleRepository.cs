using Dapper;
using RobinBradley2021.Models;
using RobinBradley2021.Models.Vehicles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using static RobinBradley2021.DataAccess.SqlConnection;
using System.Threading.Tasks;

namespace RobinBradley2021.DataAccess
{
    public class VehicleRepository
    {
        static readonly string database = "WorkDeskDB";


        //GET DATA
        public async static Task<ObservableCollection<VehicleModel>> VehicleQueryAsync()
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString(database)))
            {
                var vehicles = new Dictionary<int, VehicleModel>();
                await connection.QueryAsync("dbo.spGetVehicleData_All", new[]
            {
                    typeof(VehicleModel),
                    typeof(VehicleAssignmentRecordModel),
                    typeof(EmployeeModel),
                    typeof(JobsiteModel),
                    typeof(VehicleInvoiceModel),
                    typeof(InvoiceLineItemModel),
                    typeof(VehicleClassModel)

            }
                , obj =>
                {
                    var vehicleModel = obj[0] as VehicleModel;
                    var vehicleAssignmentRecordModel = obj[1] as VehicleAssignmentRecordModel;
                    var employeeModel = obj[2] as EmployeeModel;
                    var jobsiteModel = obj[3] as JobsiteModel;
                    var vehicleInvoiceModel = obj[4] as VehicleInvoiceModel;
                    var invoiceLineItemModel = obj[5] as InvoiceLineItemModel;
                    var vehicleClassModel = obj[6] as VehicleClassModel;

                    var vehicleEntity = new VehicleModel();
                    if (!vehicles.TryGetValue(vehicleModel.Id, out vehicleEntity))
                    {
                        vehicles.Add(vehicleModel.Id, vehicleEntity = vehicleModel);
                    }
                    if (vehicleEntity.Assignments == null)
                    {
                        vehicleEntity.Assignments = new ObservableCollection<VehicleAssignmentRecordModel>();
                    }
                    if (vehicleAssignmentRecordModel != null)
                    {
                        vehicleAssignmentRecordModel.AssignedVehicle = vehicleModel;
                        vehicleAssignmentRecordModel.Assignee = employeeModel;
                        vehicleAssignmentRecordModel.Jobsite = jobsiteModel;
                        if (!vehicleEntity.Assignments.Any(x => x.Id == vehicleAssignmentRecordModel.Id))
                        {
                            vehicleEntity.Assignments.Add(vehicleAssignmentRecordModel);
                        }
                        if (vehicleEntity.InvoiceHistory == null)
                        {
                            vehicleEntity.InvoiceHistory = new ObservableCollection<VehicleInvoiceModel>();
                        }

                        if (vehicleEntity.InvoiceHistory.Count > 0)
                        {
                            if (!vehicleEntity.InvoiceHistory.Any(x => x.Id == vehicleInvoiceModel.Id))
                            {
                                vehicleEntity.InvoiceHistory.Add(vehicleInvoiceModel);
                            }
                            foreach (VehicleInvoiceModel item in vehicleEntity.InvoiceHistory)
                                if (item != null)
                                {
                                    if (item.LineItems == null)
                                    {
                                        item.LineItems = new ObservableCollection<InvoiceLineItemModel>();
                                        if (item.LineItems.Any(x => x.Id == invoiceLineItemModel.Id))
                                        {
                                            item.LineItems.Add(invoiceLineItemModel);
                                        }
                                    }
                                    else
                                    {
                                        if (item.LineItems.Any(x => x.Id == invoiceLineItemModel.Id))
                                        {
                                            item.LineItems.Add(invoiceLineItemModel);
                                        }
                                    }
                                }
                        }
                    }
                    if (vehicleEntity.Class == null)
                    {
                        vehicleEntity.Class = vehicleClassModel;
                    }

                    return vehicleEntity;
                }); ;
                var result = vehicles.Values.ToList();
                var vehicleCollection = new ObservableCollection<VehicleModel>(result);
                return vehicleCollection;
            }
        }
        public static async Task<ObservableCollection<VehicleAssignmentRecordModel>> VehicleAssignmentQueryAsync()
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString(database)))
            {
                var assignments = new Dictionary<int, VehicleAssignmentRecordModel>();
                await connection.QueryAsync<VehicleAssignmentRecordModel>("dbo.spGetVehicleAssignments_All", new[]
            {
                    typeof(VehicleAssignmentRecordModel),
                    typeof(VehicleModel),
                    typeof(EmployeeModel)
                }
            , obj =>
            {
                VehicleAssignmentRecordModel _assignment = obj[0] as VehicleAssignmentRecordModel;
                VehicleModel _vehicle = obj[1] as VehicleModel;
                EmployeeModel _employee = obj[2] as EmployeeModel;
                var _assignmentEntity = new VehicleAssignmentRecordModel();
                if (!assignments.TryGetValue(_assignment.Id, out _assignmentEntity))
                {
                    assignments.Add(_assignment.Id, _assignmentEntity = _assignment);
                }

                if (_vehicle != null)
                {
                    _assignment.AssignedVehicle = _vehicle;
                }
                if (_employee != null)
                {
                    _assignment.Assignee = _employee;
                }
                return _assignmentEntity;
            }); ;
                var result = assignments.Values.ToList();
                var assignmentsCollection = new ObservableCollection<VehicleAssignmentRecordModel>(result);
                return assignmentsCollection;
            }
        }
        public static async Task<ObservableCollection<VehicleClassModel>> VehicleClassQueryAsync()
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString(database)))
            {
                var vehicleClasses = await connection.QueryAsync<VehicleClassModel>("dbo.spGetVehicleClasses_All", commandType: System.Data.CommandType.StoredProcedure);
                var result = vehicleClasses.ToList();
                var vehicleClassesCollection = new ObservableCollection<VehicleClassModel>(result);
                return vehicleClassesCollection;
            }
        }
        public static async Task<ObservableCollection<VehicleModel>> VehiclesAvailableQueryAsync()
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString(database)))
            {
                var vehiclesAvailable = await connection.QueryAsync<VehicleModel>("dbo.spGetVehicles_Available", commandType: System.Data.CommandType.StoredProcedure);
                var result = vehiclesAvailable.ToList();
                var vehiclesAvailableCollection = new ObservableCollection<VehicleModel>(result);
                return vehiclesAvailableCollection;
            }
        }
        public static async Task<ObservableCollection<VehicleInvoiceModel>> GetVehicleInvoiceRecordsAsync()
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString(database)))
            {
                var invoices = new Dictionary<int, VehicleInvoiceModel>();
                await connection.QueryAsync<VehicleInvoiceModel>("dbo.spGetVehicleInvoices_All", new []
                 {
                   typeof(VehicleInvoiceModel),
                   typeof(InvoiceLineItemModel),
                   typeof(VehicleModel)
                 }
                    , obj =>
              {
                  var invoiceModel = obj[0] as VehicleInvoiceModel;
                  var lineItemModel = obj[1] as InvoiceLineItemModel;
                  var vehicleModel = obj[2] as VehicleModel;
                  var invoiceEntity = new VehicleInvoiceModel();
                  if (!invoices.TryGetValue(invoiceModel.Id, out invoiceEntity))
                  {
                      invoices.Add(invoiceModel.Id, invoiceEntity = invoiceModel);
                  }

                  if (invoiceEntity.LineItems == null)
                  {
                      invoiceEntity.LineItems = new ObservableCollection<InvoiceLineItemModel>();
                  }
                  if (lineItemModel != null)
                  {
                      if (!invoiceEntity.LineItems.Any(x => x.Id == lineItemModel.Id))
                      {
                          invoiceEntity.LineItems.Add(lineItemModel);
                      }
                  }
                  if (vehicleModel != null)
                  {
                      invoiceEntity.VehicleServiced = vehicleModel;
                  }

                  return invoiceEntity;
              }); ;
                var result = invoices.Values.ToList();
                var invoicesCollection = new ObservableCollection<VehicleInvoiceModel>(result);
                return invoicesCollection;
            }
        }

        //SEND DATA
        public static VehicleModel CreateVehicle(VehicleModel newVehicle)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString("WorkDeskDB")))
            {
                var p = new DynamicParameters();
                p.Add("@FleetNumber", newVehicle.FleetNumber);
                p.Add("@Manufacturer", newVehicle.Make);
                p.Add("@Model", newVehicle.Model);
                p.Add("@VIN", newVehicle.VIN);
                p.Add("@RegistrationNumber", newVehicle.RegistrationNumber);
                p.Add("@Year", newVehicle.Year);
                p.Add("@Color", newVehicle.Color);
                p.Add("@ClassId", newVehicle.Class.Id);
                p.Add("@GasCardNumber", newVehicle.GasCardNumber);
                p.Add("@RegistrationDueDate", newVehicle.RegistrationMonth);
                p.Add("@InspectionDueDate", newVehicle.InspectionMonth);
                p.Add("@PurchaseDate", newVehicle.PurchaseDate);
                p.Add("@IsAssigned", newVehicle.IsAssigned);
                p.Add("@Condition", newVehicle.Condition);
                p.Add("@Price", newVehicle.Price);
                p.Add("@WarrantyMonths", newVehicle.WarrantyMonths);
                p.Add("@id", 0, dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                connection.Execute("dbo.spVehicle_Insert", p, commandType: System.Data.CommandType.StoredProcedure);
                newVehicle.Id = p.Get<int>("@id");

                return newVehicle;
            }
        }
        public static void CreateVehicleAssignmentRecord(VehicleAssignmentRecordModel newAssignment)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString("WorkDeskDB")))
            {
                var p = new DynamicParameters();
                p.Add("@EmployeeId", newAssignment.Assignee.Id);
                p.Add("@VehicleId", newAssignment.AssignedVehicle.Id);
                p.Add("@DateOut", newAssignment.DateOut);
                if (newAssignment.IsStandardIssue == false)
                {
                    p.Add("@DueDate", newAssignment.DueDate);
                    p.Add("@JobsiteId", newAssignment.Jobsite.Id);
                    p.Add("@IsStandardIssue", newAssignment.IsStandardIssue);
                }
                else
                {
                    p.Add("@DueDate", null);
                    p.Add("@JobsiteId", null);
                    p.Add("@IsStandardIssue", newAssignment.IsStandardIssue);
                }
                p.Add("@FuelLevelOut", newAssignment.FuelLevelOut);
                p.Add("@ConditionOut", newAssignment.ConditionOut);
                connection.Execute("dbo.spVehicleAssignmentRecord_Insert", p, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        public static void CreateVehicleInvoiceRecord(VehicleInvoiceModel newRecord)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString("WorkDeskDB")))
            {
                var p = new DynamicParameters();
                p.Add("@InvoiceNumber", newRecord.InvoiceNumber);
                p.Add("@InvoiceDate", newRecord.InvoiceDate);
                p.Add("@VehicleId", newRecord.VehicleServiced.Id);
                //     p.Add("@LineItems", newRecord.LineItems);
                p.Add("@Provider", newRecord.Provider);
                p.Add("@id", 0, dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                connection.Execute("dbo.spVehicleInvoiceRecord_Insert", p, commandType: System.Data.CommandType.StoredProcedure);
                newRecord.Id = p.Get<int>("@id");
                var lineItems = new DynamicParameters();
                foreach (InvoiceLineItemModel _item in newRecord.LineItems)
                    if (_item.Description != null)
                    {
                        {
                            lineItems.Add("@Description", _item.Description);
                            lineItems.Add("@Cost", _item.Cost);
                            lineItems.Add("@IsRoutineMaintenance", _item.IsRoutineMaintenance);
                            lineItems.Add("@IsMechanicalRepair", _item.IsMechanicalRepair);
                            lineItems.Add("@IsAccidentDamage", _item.IsAccidentDamage);
                            lineItems.Add("@InvoiceId", newRecord.Id);
                            connection.Execute("dbo.spVehicleInvoiceLineItem_Insert", lineItems, commandType: System.Data.CommandType.StoredProcedure);
                        }

                    }

            }

        }
    }

}



//DELETE DATA


