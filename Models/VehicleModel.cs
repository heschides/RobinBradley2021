using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RobinBradley2021.Models.Enums;

namespace RobinBradley2021.Models
{
    public class VehicleModel
    {
        public int Id { get; set; }
        public string FleetNumber { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string VIN { get; set; }
        public int Year { get; set; }
        public VehicleClass Class {get; set;} 
        public string GasCardNumber { get; set; }
        public DateTime RegistrationDueDate { get; set; }
        public DateTime InspectionDueDate { get; set; }
        public DateTime PurchaseDate { get; set; }
        public bool IsAssigned { get; set; }
        public GenericCondition Condition { get; set; }
        public ObservableCollection<VehicleInvoiceModel>InvoiceHistory { get; set; } 
        public ObservableCollection<VehicleAssignmentRecordModel> Assignments { get; set; }
    }
}
