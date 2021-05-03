using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyEmployeeTracker.Models
{
   public class InvoiceLineItemModel
    {
        public int Id { get; set; }
        public enum Class { [Display(Name ="Routine Maintenance")] RoutineMaintenance, [Display(Name ="Mechanical Failure")] MechanicalFailure, [Display(Name = "Accident Damage")] AccidentDamage }
        public string Description { get; set; }
        public decimal Cost { get; set; }
    }
}
