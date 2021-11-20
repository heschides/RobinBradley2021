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
        public int RecordId { get; set; }
        public bool IsRoutineMaintenance{ get; set; }
        public bool IsMechanicalRepair { get; set; }
        public bool IsAccidentDamage { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
    }
}
