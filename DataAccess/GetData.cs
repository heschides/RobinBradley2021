using Dapper;
using SimplyEmployeeTracker.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using static SimplyEmployeeTracker.DataAccess.SqlConnect;

namespace SimplyEmployeeTracker.DataAccess
{
    public static class GetData
    {
        public static async Task<ObservableCollection<CertificationModel>> CertificationQueryAsync()
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString("WorkDeskDB")))
            {
                var certifications = await connection.QueryAsync<CertificationModel>("dbo.spGetCertificationTypes_All", commandType: System.Data.CommandType.StoredProcedure);
                var result = certifications.ToList();
                var certificationCollection = new ObservableCollection<CertificationModel>(result);
                return certificationCollection;
            }

        }
        public static async Task<ObservableCollection<DepartmentModel>> DepartmentQueryAsync()
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString("WorkDeskDB")))
            {
                var departments = await connection.QueryAsync<DepartmentModel>("dbo.spGetDepartments_All", commandType: System.Data.CommandType.StoredProcedure);
                var result = departments.ToList();
                var departmentsCollection = new ObservableCollection<DepartmentModel>(result);
                return departmentsCollection;
            }
        }
        public static async Task<ObservableCollection<EmployeeModel>> EmployeeQueryAsync()
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString("WorkDeskDB")))
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
                EmployeeModel employeeEntity = new EmployeeModel();
                if (!employees.TryGetValue(employeeModel.ID, out employeeEntity))
                {
                    employees.Add(employeeModel.ID, employeeEntity = employeeModel);
                }

                //list<emailmodel>
                if (employeeEntity.Emails == null)
                {
                    employeeEntity.Emails = new ObservableCollection<EmailModel>();
                }
                if (emailModel != null)
                {
                    if (!employeeEntity.Emails.Any(x => x.ID == emailModel.ID))
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
                    if (!employeeEntity.Phones.Any(x => x.ID == phoneModel.ID))
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
                    if (!employeeEntity.Citations.Any(x => x.ID == citationModel.ID))
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
                    if (!employeeEntity.Certifications.Any(x => x.ID == certificationModel.ID))
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
                    if (!employeeEntity.Restrictions.Any(x => x.ID == restrictionModel.ID))
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
                    if (!employeeEntity.EquipmentAssignments.Any(x => x.ID == equipmentAssignmentRecord.ID))
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
        public static async Task<ObservableCollection<EquipmentModel>> EquipmentQueryAsync()
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString("WorkDeskDB")))
            {
                var equipment = new Dictionary<int, EquipmentModel>();
                await connection.QueryAsync<EquipmentModel>("dbo.spGetEquipmentData_All", new[]
            {

                    typeof(EquipmentModel),
                    typeof(EquipmentAssignmentRecordModel)

            }
                , obj =>
                  {
                      EquipmentModel equipmentModel = obj[0] as EquipmentModel;
                      EquipmentAssignmentRecordModel equipmentAssignmentRecord = obj[1] as EquipmentAssignmentRecordModel;

                      EquipmentModel equipmentEntity = new EquipmentModel();
                      if (!equipment.TryGetValue(equipmentModel.ID, out equipmentEntity))
                      {
                          equipment.Add(equipmentModel.ID, equipmentEntity = equipmentModel);
                      }
                      if (equipmentEntity.Assignments == null)
                      {
                          equipmentEntity.Assignments = new ObservableCollection<EquipmentAssignmentRecordModel>();
                      }
                      if (equipmentAssignmentRecord != null)
                      {
                          if (!equipmentEntity.Assignments.Any(x => x.ID == equipmentAssignmentRecord.ID))
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
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString("WorkDeskDB")))
            {
                var jobTitles = await connection.QueryAsync<JobTitleModel>("dbo.spGetJobTitles_All", commandType: System.Data.CommandType.StoredProcedure);
                var result = jobTitles.ToList();
                var jobTitlesCollection = new ObservableCollection<JobTitleModel>(result);
                return jobTitlesCollection;
            }
        }
        public static async Task<ObservableCollection<RestrictionModel>> RestrictionQueryAsync()
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString("WorkDeskDB")))
            {
                var restrictions = await connection.QueryAsync<RestrictionModel>("dbo.spGetRestrictionTypes_All", commandType: System.Data.CommandType.StoredProcedure);
                var result = restrictions.ToList();
                var restrictionCollection = new ObservableCollection<RestrictionModel>(result);
                return restrictionCollection;
            }
        }

        public static async Task<ObservableCollection<EquipmentClassModel>> EquipmentClassQueryAsync()
    {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString("WorkDeskDB")))
            {
                var equipmentClasses = await connection.QueryAsync<EquipmentClassModel>("dbo.spGetEquipmentClasses_All", commandType: System.Data.CommandType.StoredProcedure);
                var result = equipmentClasses.ToList();
                var equipmentClassesCollection = new ObservableCollection<EquipmentClassModel>(result);
                return equipmentClassesCollection;
            }
    }
    }
}





