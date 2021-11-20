using RobinBradley2021.Models.Administration;
using RobinBradley2021.Models.BindingLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RobinBradley2021.Models
{
    public class VehicleAssignmentRecordModel
    {
        public int Id { get; set; }
        public VehicleModel AssignedVehicle { get; set; }
        public EmployeeModel Assignee { get; set; }
        public JobsiteModel Jobsite { get; set; }
        public DateTime DateOut { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime DueDate { get; set; }
        public string ConditionOut{ get; set; }
        public string ConditionIn { get; set; }
        public bool IsResolved { get; set; }
        public bool IsStandardIssue { get; set; }
        public string FuelLevelOut { get; set; }
        public string FuelLevelIn { get; set; }

    }

    

}
