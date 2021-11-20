using Dapper;
using RobinBradley2021.Models;
using RobinBradley2021.Models.Employees;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RobinBradley2021.DataAccess.SqlConnection;


namespace RobinBradley2021.DataAccess
{
    class EmployeeRepository
    {
        static readonly string database = "WorkDeskDB";

        //GET DATA
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
                    typeof(CertificationRecordModel),
                    typeof(RestrictionModel),
                    typeof(DocumentModel)

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
                CertificationRecordModel CertificationRecordModel = obj[7] as CertificationRecordModel;
                RestrictionModel restrictionModel = obj[8] as RestrictionModel;
                DocumentModel documentModel = obj[9] as DocumentModel;

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
                //list<phonemodel>
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
                    employeeEntity.Certifications = new ObservableCollection<CertificationRecordModel>();
                }
                if (CertificationRecordModel != null)
                {
                    if (!employeeEntity.Certifications.Any(x => x.Id == CertificationRecordModel.Id))
                    {
                        employeeEntity.Certifications.Add(CertificationRecordModel);
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
                //documents
                if (employeeEntity.Documents == null)
                {
                    employeeEntity.Documents = new ObservableCollection<DocumentModel>();
                }
                if (documentModel != null)
                {
                    if (!employeeEntity.Documents.Any(x => x.Id == documentModel.Id))
                    {
                        employeeEntity.Documents.Add(documentModel);
                    }
                }
                //profile picture
                if (employeeEntity.ProfilePhoto == null)
                {
                    employeeEntity.ProfilePhoto = new DocumentModel();
                    employeeEntity.ProfilePhoto.Name = @"C:\Users\jwhit\source\repos\Robin-Bradley2021\Images\defaultUserImage.jpg";
                }


                return employeeEntity;
            }); ;
                var result = employees.Values.ToList();
                var employeeCollection = new ObservableCollection<EmployeeModel>(result);
                return employeeCollection;
            }
        }
        public static async Task<ObservableCollection<EmployeeModel>> Employees_VehiclesEligibleQueryAsync()
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString(database)))
            {
                var employees_VehiclesEligible = await connection.QueryAsync<EmployeeModel>("dbo.spGetEmployees_VehicleEligible", commandType: System.Data.CommandType.StoredProcedure);
                var result = employees_VehiclesEligible.ToList();
                var employees_VehiclesEligibleCollection = new ObservableCollection<EmployeeModel>(result);
                return employees_VehiclesEligibleCollection;
            }
        }
        public static async Task<EmployeeModel> SelectedEmployeeQueryAsync(int Id)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString(database)))
            {

                var selectedEmployee = await connection.QuerySingleAsync<EmployeeModel>("dbo.spGetSelectedEmployee", Id, commandType: System.Data.CommandType.StoredProcedure);
                return selectedEmployee;
            }
        }

        //SEND DATA
        public static EmployeeModel CreateEmployee(EmployeeModel e)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString("WorkDeskDB")))
            {
                var p = new DynamicParameters();
                p.Add("@FirstName", e.FirstName);
                p.Add("@LastName", e.LastName);
                p.Add("@NickName", e.Nickname);
                p.Add("@HireDate", e.HireDate);
                p.Add("@JobTitleId", e.JobTitle.Id);
                p.Add("@DepartmentId", e.Department.Id);
                p.Add("@EmployeeStatusId", e.Status.Id); //TODO: DERIVE EMPLOYEE.STATUS FROM STATUS TABLE
                p.Add("@id", 0, dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                connection.Execute("dbo.spEmployees_Insert", p, commandType: System.Data.CommandType.StoredProcedure);
                e.Id = p.Get<int>("@id");

                var phones = new DynamicParameters();
                foreach (PhoneModel phoneModel in e.Phones)
                {
                    phones.Add("@Number", phoneModel.Number);
                    phones.Add("@Type", phoneModel.Type);
                    phones.Add("@EmployeeId", e.Id);
                    connection.Execute("dbo.spPhones_Insert", phones, commandType: System.Data.CommandType.StoredProcedure);
                }

                var emails = new DynamicParameters();
                foreach (EmailModel emailModel in e.Emails)
                {
                    emails.Add("@Address", emailModel.Address);
                    emails.Add("@Type", emailModel.Type);
                    emails.Add("@EmployeeId", e.Id);
                    connection.Execute("dbo.spEmails_Insert", emails, commandType: System.Data.CommandType.StoredProcedure);
                }

                var certifications = new DynamicParameters();
                foreach (CertificationRecordModel certification in e.Certifications)
                {
                    DateTime thisDay = DateTime.Today;
                    certifications.Add("@CertificationNameId", certification.Id);
                    certifications.Add("@CertificationInitialDate", thisDay);
                    certifications.Add("@CertificationEndDate", certification.DateEnd);
                    certifications.Add("@EmployeeId", e.Id);
                    connection.Execute("dbo.spEmployeesCertificationType_Insert", certifications, commandType: System.Data.CommandType.StoredProcedure);
                }

                var restrictions = new DynamicParameters();
                foreach (RestrictionModel restriction in e.Restrictions)
                {
                    DateTime thisDay = DateTime.Today;
                    restrictions.Add("@RestrictionNameId", restriction.Id);
                    restrictions.Add("@RestrictionInitialDate", thisDay);
                    restrictions.Add("@RestrictionEndDate", restriction.EndDate);
                    restrictions.Add("@EmployeeId", e.Id);
                    connection.Execute("dbo.spEmployeesRestrictionTypes_Insert", restrictions, commandType: System.Data.CommandType.StoredProcedure);
                }
                return e;
            }
        }
        //EDIT DATA
        public async Task<EmployeeModel> EditEmployee(EmployeeModel e)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString(database)))
            {
                var employee = new DynamicParameters();
                employee.Add("@FirstName", e.FirstName);
                employee.Add("@LastName", e.LastName);
                employee.Add("@Nickname", e.Nickname);
                employee.Add("@JobTitleId", e.JobTitle.Id);
                employee.Add("@DepartmentId", e.Department.Id);
                employee.Add("@EmployeeStatusId", e.Status.Id);
                connection.Execute("dbo.spEmployees_Update", employee, commandType: System.Data.CommandType.StoredProcedure);

                var result = await EmployeeRepository.SelectedEmployeeQueryAsync(e.Id);
                return result;
            }
        }
        //DELETE DATA
        public static void DeleteEmployee(EmployeeModel e)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString("WorkdeskDB")))
            {
                var p = new DynamicParameters();
                p.Add("@id", e.Id);

                connection.Execute("dbo.spEmployees_Delete", p, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
