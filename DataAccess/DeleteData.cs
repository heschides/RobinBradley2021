using Dapper;
using RobinBradley2021.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RobinBradley2021.DataAccess.SqlConnect;

namespace RobinBradley2021.DataAccess
{
    public static class DeleteData
    {
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
