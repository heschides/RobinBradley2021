using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobinBradley2021.Models.Equipment
{
    public class EquipmentAssignmentLineItemModel
    {
        public int Id { get; set; }
        public int EquipmentAssignmentId { get; set; }
        public EquipmentModel Equipment { get; set; }
        public bool IsResolved { get; set; }
    }
}
