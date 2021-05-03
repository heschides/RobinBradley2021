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
        public enum ConditionOut { Excellent, Good, Fair, Poor  }
        public enum ConditionIn { Excellent, Good, Fair, Poor, Unacceptable }
        public bool IsResolved { get; set; }
    }
}
