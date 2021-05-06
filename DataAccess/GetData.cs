using Dapper;
using System;
using SimplyEmployeeTracker.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using static SimplyEmployeeTracker.DataAccess.SqlConnect;

namespace SimplyEmployeeTracker.DataAccess
{
    public class GetData
    {
        static string database = "WorkDeskDB";
        public static async Task<ObservableCollection<CertificationModel>> CertificationQueryAsync()
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString(database)))
            {
                var certifications = await connection.QueryAsync<CertificationModel>("dbo.spGetCertificationTypes_All", commandType: System.Data.CommandType.StoredProcedure);
                var result = certifications.ToList();
                var certificationCollection = new ObservableCollection<CertificationModel>(result);
                return certificationCollection;
            }
        }
        public static async Task<ObservableCollection<DepartmentModel>> DepartmentQueryAsync()
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString(database)))
            {
                var departments = await connection.QueryAsync<DepartmentModel>("dbo.spGetDepartments_All", commandType: System.Data.CommandType.StoredProcedure);
                var result = departments.ToList();
                var departmentsCollection = new ObservableCollection<DepartmentModel>(result);
                return departmentsCollection;
            }
        }
        public static async Task<ObservableCollection<EmployeeModel>> EmployeeQueryAsync()
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString(database)))
            {
                var employees = new Dictionary<int, EmployeeModel>();
                await connection.QueryAsync<EmployeeModel>("dbo.spGetEmployeeData_All", new[]
            {
                    typeof(EmployeeModel),
                    typeof(EmailModel),
                    typeof(JobTitleModel),
                    typeof(PhoneModel),
                    typeof(DepartmentModel),
                    typeof(EmployeeStatusModel),
                    typeof(CitationModel),
                    typeof(CertificationModel),
                    typeof(EquipmentAssignmentRecordModel),
                    typeof(RestrictionModel)
            }
            , obj =>
            {
                EmployeeModel employeeModel = obj[0] as EmployeeModel;
                EmailModel emailModel = obj[1] as EmailModel;
                JobTitleModel titleModel = obj[2] as JobTitleModel;
                PhoneModel phoneModel = obj[3] as PhoneModel;
                DepartmentModel departmentModel = obj[4] as DepartmentModel;
                EmployeeStatusModel statusModel = obj[5] as EmployeeStatusModel;
                CitationModel citationModel = obj[6] as CitationModel;
                CertificationModel certificationModel = obj[7] as CertificationModel;
                EquipmentAssignmentRecordModel equipmentAssignmentRecord = obj[8] as EquipmentAssignmentRecordModel;
                RestrictionModel restrictionModel = obj[9] as RestrictionModel;

                //employeemodel
                var employeeEntity = new EmployeeModel();
                if (!employees.TryGetValue(employeeModel.Id, out employeeEntity))
                {
                    employees.Add(employeeModel.Id, employeeEntity = employeeModel);
                }

                //list<emailmodel>
                if (employeeEntity.Emails == null)
                {
                    employeeEntity.Emails = new ObservableCollection<EmailModel>();
                }
                if (emailModel != null)
                {
                    if (!employeeEntity.Emails.Any(x => x.Id == emailModel.Id))
                    {
                        employeeEntity.Emails.Add(emailModel);
                    }
                }

                //phonemodel
                if (employeeEntity.Phones == null)
                {
                    employeeEntity.Phones = new ObservableCollection<PhoneModel>();
                }
                if (phoneModel != null)
                {
                    if (!employeeEntity.Phones.Any(x => x.Id == phoneModel.Id))
                    {
                        employeeEntity.Phones.Add(phoneModel);
                    }
                }

                //title
                if (employeeEntity.JobTitle == null)
                {
                    if (titleModel != null)
                    {
                        employeeEntity.JobTitle = titleModel;
                    }
                }

                //department
                if (employeeEntity.Department == null)
                {
                    if (departmentModel != null)
                    {
                        employeeEntity.Department = departmentModel;
                    }
                }

                //status
                if (employeeEntity.Status == null)
                {
                    if (statusModel != null)
                    {
                        employeeEntity.Status = statusModel;
                    }
                }

                //citation
                if (employeeEntity.Citations == null)
                {
                    employeeEntity.Citations = new ObservableCollection<CitationModel>();
                }
                if (citationModel != null)
                {
                    if (!employeeEntity.Citations.Any(x => x.Id == citationModel.Id))
                    {
                        employeeEntity.Citations.Add(citationModel);
                    }
                }

                //certification
                if (employeeEntity.Certifications == null)
                {
                    employeeEntity.Certifications = new ObservableCollection<CertificationModel>();
                }
                if (certificationModel != null)
                {
                    if (!employeeEntity.Certifications.Any(x => x.Id == certificationModel.Id))
                    {
                        employeeEntity.Certifications.Add(certificationModel);
                    }
                }

                //restriction
                if (employeeEntity.Restrictions == null)
                {
                    employeeEntity.Restrictions = new ObservableCollection<RestrictionModel>();
                }
                if (restrictionModel != null)
                {
                    if (!employeeEntity.Restrictions.Any(x => x.Id == restrictionModel.Id))
                    {
                        employeeEntity.Restrictions.Add(restrictionModel);
                    }
                }

                //equipment record
                if (employeeEntity.EquipmentAssignments == null)
                {
                    employeeEntity.EquipmentAssignments = new ObservableCollection<EquipmentAssignmentRecordModel>();
                }
                if (equipmentAssignmentRecord != null)
                {
                    if (!employeeEntity.EquipmentAssignments.Any(x => x.Id == equipmentAssignmentRecord.Id))
                    {
                        employeeEntity.EquipmentAssignments.Add(equipmentAssignmentRecord);
                    }
                }
                return employeeEntity;
            }); ;

                var result = employees.Values.ToList();
                var employeeCollection = new ObservableCollection<EmployeeModel>(result);
                return employeeCollection;
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
                    typeof(EquipmentAssignmentRecordModel)
            }
                , obj =>
                  {
                      var equipmentModel = obj[0] as EquipmentModel;
                      var equipmentAssignmentRecord = obj[1] as EquipmentAssignmentRecordModel;

                      var equipmentEntity = new EquipmentModel();
                      if (!equipment.TryGetValue(equipmentModel.Id, out equipmentEntity))
                      {
                          equipment.Add(equipmentModel.Id, equipmentEntity = equipmentModel);
                      }
                      if (equipmentEntity.Assignments == null)
                      {
                          equipmentEntity.Assignments = new ObservableCollection<EquipmentAssignmentRecordModel>();
                      }
                      if (equipmentAssignmentRecord != null)
                      {
                          if (!equipmentEntity.Assignments.Any(x => x.Id == equipmentAssignmentRecord.Id))
                          {
                              equipmentEntity.Assignments.Add(equipmentAssignmentRecord);
                          }
                      }
                      return equipmentEntity;
                  }); ;
                var result = equipment.Values.ToList();
                var equipmentCollection = new ObservableCollection<EquipmentModel>(result);
                return equipmentCollection;
            }
        }
        public static async Task<ObservableCollection<JobTitleModel>> JobTitleQueryAsync()
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString(database)))
            {
                var jobTitles = await connection.QueryAsync<JobTitleModel>("dbo.spGetJobTitles_All", commandType: System.Data.CommandType.StoredProcedure);
                var result = jobTitles.ToList();
                var jobTitlesCollection = new ObservableCollection<JobTitleModel>(result);
                return jobTitlesCollection;
            }
        }
        public static async Task<ObservableCollection<RestrictionModel>> RestrictionQueryAsync()
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString(database)))
            {
                var restrictions = await connection.QueryAsync<RestrictionModel>("dbo.spGetRestrictionTypes_All", commandType: System.Data.CommandType.StoredProcedure);
                var result = restrictions.ToList();
                var restrictionCollection = new ObservableCollection<RestrictionModel>(result);
                return restrictionCollection;
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
        public static async Task<ObservableCollection<JobsiteModel>> JobsiteQueryAsync()
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString(database)))
            {
                var jobsites = await connection.QueryAsync<JobsiteModel>("dbo.spGetJobsites_All", commandType: System.Data.CommandType.StoredProcedure);
                var result = jobsites.ToList();
                var jobsitesCollection = new ObservableCollection<JobsiteModel>(result);
                return jobsitesCollection;
            }
        }
        public static async Task<ObservableCollection<EquipmentAssignmentRecordModel>> EquipmentAssignmentQueryAsync()
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString(database)))
            {
                var assignments = await connection.QueryAsync<EquipmentAssignmentRecordModel>("dbo.spGetEquipmentAssignments_All", commandType: System.Data.CommandType.StoredProcedure);
                var result = assignments.ToList();
                var assignmentsCollection = new ObservableCollection<EquipmentAssignmentRecordModel>(result);
                return assignmentsCollection;
            }
        }
        public static async Task<ObservableCollection<VehicleAssignmentRecordModel>> VehicleAssignmentQueryAsync()
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString(database)))
            {
                var assignments = await connection.QueryAsync<VehicleAssignmentRecordModel>("dbo.spGetVehicleAssignments_All", commandType: System.Data.CommandType.StoredProcedure);
                var result = assignments.ToList();
                var assignmentsCollection = new ObservableCollection<VehicleAssignmentRecordModel>(result);
                return assignmentsCollection;
            }
        }
        public async static Task<ObservableCollection<VehicleModel>> VehicleQueryAsync()
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString(database)))
            {
                var vehicles = new Dictionary<int, VehicleModel>();
                await connection.QueryAsync("dbo.spGetVehicleData_All", new[]
            {
                    typeof(VehicleModel),
                    typeof(VehicleAssignmentRecordModel),
                    typeof(VehicleInvoiceModel),
                    typeof(InvoiceLineItemModel)
            }
                , obj =>

                {
                    var vehicleModel = obj[0] as VehicleModel;
                    var vehicleAssignmentRecordModel = obj[1] as VehicleAssignmentRecordModel;
                    var vehicleInvoiceModel = obj[2] as VehicleInvoiceModel;
                    var invoiceLineItemModel = obj[3] as InvoiceLineItemModel;


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
                        if (!vehicleEntity.Assignments.Any(x => x.Id == vehicleAssignmentRecordModel.Id))
                        {
                            vehicleEntity.Assignments.Add(vehicleAssignmentRecordModel);
                        }

                        if (vehicleEntity.InvoiceHistory == null)
                        {
                            vehicleEntity.InvoiceHistory = new ObservableCollection<VehicleInvoiceModel>();
                        }
                        if (!vehicleEntity.InvoiceHistory.Any(x => x.Id == vehicleInvoiceModel.Id))
                        {
                            vehicleEntity.InvoiceHistory.Add(vehicleInvoiceModel);
                        }
                        foreach (VehicleInvoiceModel item in vehicleEntity.InvoiceHistory)
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
                    return vehicleEntity;
                }); ;

                var result = vehicles.Values.ToList();
                var vehicleCollection = new ObservableCollection<VehicleModel>(result);
                return vehicleCollection;
            }
        }



    }
}





