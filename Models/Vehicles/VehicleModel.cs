using RobinBradley2021.Models.Vehicles;
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
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string VIN { get; set; }
        public string RegistrationNumber { get; set; }
        public string Year { get; set; }
        public VehicleClassModel Class {get; set;} 
        public string GasCardNumber { get; set; }
        public string TransponderNumber { get; set; }
        public Month? RegistrationMonth { get; set; }
        public Month? InspectionMonth { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal? Price { get; set; }
        public int? WarrantyMonths { get; set; }
        public bool IsAssigned { get; set; }
        public GenericCondition Condition { get; set; }
        public ObservableCollection<VehicleInvoiceModel>InvoiceHistory { get; set; } 
        public ObservableCollection<VehicleAssignmentRecordModel> Assignments { get; set; }
    }
}
