using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobinBradley2021.Models
{
    public class VehicleInvoiceModel
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public string Provider { get; set; }
        public DateTime InvoiceDate { get; set; }
        public ObservableCollection<InvoiceLineItemModel>LineItems { get; set; }
    }
}
