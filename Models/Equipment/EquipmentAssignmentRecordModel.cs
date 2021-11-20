using RobinBradley2021.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobinBradley2021.Models
{
   public class EquipmentAssignmentRecordModel
    {
        public int Id{ get; set; }
        public string Description { get; set; }
        public ObservableCollection<EquipmentAssignmentLineItemModel> LineItems { get; set; }
        public EmployeeModel AssigneePerson { get; set; }
        public DepartmentModel AssigneeDepartment { get; set; }
        public DateTime DateOut { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime DueDate { get; set; }
        public string ConditionOut { get; set; }
        public string ConditionIn { get; set; }
        public JobsiteModel Jobsite { get; set; }
        public bool IsDepartment { get; set; }
        public bool IsStandardIssue { get; set; }
        public bool IsResolved { get; set; }
    }
}
