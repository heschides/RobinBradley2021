using Dapper;
using RobinBradley2021.Models;
using System.Collections.ObjectModel;
using static RobinBradley2021.DataAccess.SqlConnect;
using System;
using System.Threading.Tasks;

namespace RobinBradley2021.DataAccess
{
    public class EditData
    {
        static readonly string database = "WorkDeskDB";

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

                var result = await GetData.SelectedEmployeeQueryAsync(e.Id);
                return result;
            }
        }
    }
}
