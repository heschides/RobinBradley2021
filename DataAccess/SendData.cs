using SimplyEmployeeTracker.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimplyEmployeeTracker.DataAccess.SqlConnect;

namespace SimplyEmployeeTracker.DataAccess
{
    public static class SendData
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
                p.Add("@StatusID", e.Status.ID); //TODO: DERIVE EMPLOYEE.STATUS FROM STATUS TABLE
                p.Add("@id", 0, dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                connection.Execute("dbo.spEmployees_Insert", p, commandType: System.Data.CommandType.StoredProcedure);
                e.ID = p.Get<int>("@id");

                var phones = new DynamicParameters();
                foreach (PhoneModel phoneModel in e.Phones) {
                    phones.Add("@Number", phoneModel.Number);
                    phones.Add("@Type", phoneModel.Type);
                    phones.Add("@EmployeeID", e.ID);
                    connection.Execute("dbo.spPhones_Insert", phones, commandType: System.Data.CommandType.StoredProcedure);   }

                var emails = new DynamicParameters();
                foreach (EmailModel emailModel in e.Emails)
                {
                    emails.Add("@Address", emailModel.Address);
                    emails.Add("@Type", emailModel.Type);
                    emails.Add("@EmployeeID", e.ID);
                    connection.Execute("dbo.spEmails_Insert", emails, commandType: System.Data.CommandType.StoredProcedure);
                }

                return e;
            }
        }
    }
}
