using Dapper;
using SimplyEmployeeTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimplyEmployeeTracker.DataAccess.SqlConnect;

namespace SimplyEmployeeTracker.DataAccess
{
    public static class DeleteData
    {
        public static void DeleteEmployee(EmployeeModel e)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString("WorkdeskDB")))
            {
                var p = new DynamicParameters();
                p.Add("@id", e.ID);

                connection.Execute("dbo.spEmployees_Delete", p, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
