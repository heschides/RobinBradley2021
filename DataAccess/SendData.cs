using RobinBradley2021.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RobinBradley2021.DataAccess.SqlConnect;
using static RobinBradley2021.Functions.GenerateRandom;


namespace RobinBradley2021.DataAccess
{
    public class SendData
    {
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
                foreach (CertificationModel certification in e.Certifications)
                {
                    DateTime thisDay = DateTime.Today;
                    certifications.Add("@CertificationNameId", certification.Id);
                    certifications.Add("@CertificationInitialDate", thisDay);
                    certifications.Add("@CertificationEndDate", certification.ExpirationDate);
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
            foreach (EquipmentModel item in newAssignment.SelectedItems)
            {
                using (var connection = new System.Data.SqlClient.SqlConnection(CnnString("WorkDeskDB")))
                {
                    var p = new DynamicParameters();
                    p.Add("@EmployeeId", newAssignment.EmployeeId);
                    p.Add("@EquipmentId", item.Id);
                    if (newAssignment.Id == 0)
                    {
                        p.Add("@DepartmentId", null);
                    }
                    else
                    {
                        p.Add("@DepartmentId", newAssignment.DepartmentId);
                    }
                    p.Add("@DateOut", newAssignment.DateOut);
                    p.Add("@IsStandardIssue", newAssignment.IsStandardIssue);
                    p.Add("@IsDepartment", newAssignment.IsDepartment);
                    if (newAssignment.IsStandardIssue == false)
                    {
                        p.Add("@DueDate", newAssignment.DueDate);
                        p.Add("@JobsiteId", newAssignment.Jobsite);
                    }
                    else
                    {
                        p.Add("@DueDate", null);
                        p.Add("@JobsiteId", null);
                    }
                    connection.Execute("dbo.spEquipmentAssignmentRecord_Insert", p, commandType: System.Data.CommandType.StoredProcedure);
                }
            }
        }
    }
}

