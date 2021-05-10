using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobinBradley2021.Models
{
   public class InvoiceLineItemModel
    {
        public int Id { get; set; }
        public ClassOption Class { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }

        public enum ClassOption {[Display(Name = "Routine Maintenance")] RoutineMaintenance, [Display(Name = "Mechanical Failure")] MechanicalFailure, [Display(Name = "Accident Damage")] AccidentDamage }
    }
}
