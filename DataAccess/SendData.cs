using SimplyEmployeeTracker.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimplyEmployeeTracker.DataAccess.SqlConnect;
using static SimplyEmployeeTracker.Functions.GenerateRandom;


namespace SimplyEmployeeTracker.DataAccess
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
                p.Add("@NickName", e.NickName);
                p.Add("@HireDate", e.HireDate);
                p.Add("@JobTitleID", e.JobTitle.Id);
                p.Add("@DepartmentID", e.Department.Id);
                p.Add("@EmployeeStatusID", e.Status.ID); //TODO: DERIVE EMPLOYEE.STATUS FROM STATUS TABLE
                p.Add("@id", 0, dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                connection.Execute("dbo.spEmployees_Insert", p, commandType: System.Data.CommandType.StoredProcedure);
                e.ID = p.Get<int>("@id");

                var phones = new DynamicParameters();
                foreach (PhoneModel phoneModel in e.Phones)
                {
                    phones.Add("@Number", phoneModel.Number);
                    phones.Add("@Type", phoneModel.Type);
                    phones.Add("@EmployeeID", e.ID);
                    connection.Execute("dbo.spPhones_Insert", phones, commandType: System.Data.CommandType.StoredProcedure);
                }

                var emails = new DynamicParameters();
                foreach (EmailModel emailModel in e.Emails)
                {
                    emails.Add("@Address", emailModel.Address);
                    emails.Add("@Type", emailModel.Type);
                    emails.Add("@EmployeeID", e.ID);
                    connection.Execute("dbo.spEmails_Insert", emails, commandType: System.Data.CommandType.StoredProcedure);
                }

                var certifications = new DynamicParameters();
                foreach (CertificationModel certification in e.Certifications)
                {
                    DateTime thisDay = DateTime.Today;
                    certifications.Add("@CertificationNameID", certification.ID);
                    certifications.Add("@CertificationInitialDate", thisDay);
                    certifications.Add("@CertificationEndDate", certification.ExpirationDate);
                    certifications.Add("@EmployeeID", e.ID);
                    connection.Execute("dbo.spEmployeesCertificationType_Insert", certifications, commandType: System.Data.CommandType.StoredProcedure);
                }

                var restrictions = new DynamicParameters();
                foreach (RestrictionModel restriction in e.Restrictions)
                {
                    DateTime thisDay = DateTime.Today;
                    restrictions.Add("@RestrictionNameID", restriction.ID);
                    restrictions.Add("@RestrictionInitialDate", thisDay);
                    restrictions.Add("@RestrictionEndDate", restriction.EndDate);
                    restrictions.Add("@EmployeeID", e.ID);
                    connection.Execute("dbo.spEmployeesRestrictionTypes_Insert", restrictions, commandType: System.Data.CommandType.StoredProcedure);
                }
                return e;
            }
        }

        internal static EquipmentModel CreateItem(EquipmentModel newItem)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString("WorkDeskDB")))
            {
                var p = new DynamicParameters();
                p.Add("@InventoryID", newItem.InventoryID);
                p.Add("@Description", newItem.Description);
                p.Add("@ClassID", newItem.Class.Id);
                p.Add("@Brand", newItem.Brand);
                p.Add("@PurchaseDate", newItem.PurchaseDate);
                p.Add("@StatusID", newItem.Status.ID);
                p.Add("@Price", newItem.Price);
                p.Add("@Model", newItem.Model);
                p.Add("@SerialNumber", newItem.SerialNumber);
                p.Add("@WarrantyMonths", newItem.WarrantyMonths);
                p.Add("@CICRequired", false);
                p.Add("@id", 0, dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                connection.Execute("dbo.spEquipment_Insert", p, commandType: System.Data.CommandType.StoredProcedure);
                newItem.ID = p.Get<int>("@id");

                return newItem;
            }
        }


    }
}

