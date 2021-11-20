using Dapper;
using RobinBradley2021.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static RobinBradley2021.DataAccess.SqlConnection;
using System.Text;
using System.Threading.Tasks;
using RobinBradley2021.Models.Employees;

namespace RobinBradley2021.DataAccess
{
    //GET DATA
    
    class AdministrationRepository
    {
        static readonly string database = "WorkDeskDB";

        public static async Task<ObservableCollection<CertificationRecordModel>> CertificationQueryAsync()
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString(database)))
            {
                var certifications = await connection.QueryAsync<CertificationRecordModel>("dbo.spGetCertificationTypes_All", commandType: System.Data.CommandType.StoredProcedure);
                var result = certifications.ToList();
                var certificationCollection = new ObservableCollection<CertificationRecordModel>(result);
                return certificationCollection;
            }
        }
        public static async Task<ObservableCollection<CitationModel>> CitationQueryAsync()
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString(database)))
            {
                var citations = await connection.QueryAsync<CitationModel>("dbo.spGetCitationTypes_All", commandType: System.Data.CommandType.StoredProcedure);
                var result = citations.ToList();
                var citationCollection = new ObservableCollection<CitationModel>(result);
                return citationCollection;
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
        public static async Task<ObservableCollection<EmployeeStatusModel>> EmployeeStatusQueryAsync()
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(CnnString(database)))
            {
                var employeeStatuses = await connection.QueryAsync<EmployeeStatusModel>("dbo.spGetEmployeeStatuses_All ", commandType: System.Data.CommandType.StoredProcedure);
                var result = employeeStatuses.ToList();
                var employeeStatusesCollection = new ObservableCollection<EmployeeStatusModel>(result);
                return employeeStatusesCollection;
            }
        }
    }
}
