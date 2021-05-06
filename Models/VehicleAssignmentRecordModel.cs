using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyEmployeeTracker.Models
{
    public class VehicleAssignmentRecordModel
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime DateOut { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime DueDate { get; set; }
        public Condition ConditionOut{ get; set; }
        public Condition ConditionIn { get; set; }
        public bool IsResolved { get; set; }
        public enum Condition { Excellent, Good, Fair, Poor }

    }

    

}
