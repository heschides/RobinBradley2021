using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyEmployeeTracker.Models
{
   public class EquipmentAssignmentRecordModel
    {
        public int ID { get; set; }
        public EquipmentModel AssignedItem { get; set; }
        public List<EquipmentModel> SelectedItems { get; set; }
        public int EmployeeID { get; set; }
        public int DepartmentID { get; set; }
        public DateTime DateOut { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime DueDate { get; set; }
        public string ConditionOut { get; set; }
        public string ConditionIn { get; set; }
        public JobsiteModel Jobsite { get; set; }
        public bool IsDepartment { get; set; }
        public bool IsStandardIssue { get; set; }

    }
}
