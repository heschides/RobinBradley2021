using RobinBradley2021.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobinBradley2021.Models
{
    public class EquipmentModel
    {
        public int Id { get; set; }
        public string InventoryId { get; set; }
        public string Description { get; set; }
        public EquipmentClassModel Class { get; set; }
        public EquipmentStatusModel Status { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public DateTime  PurchaseDate { get; set; }
        public decimal Price { get; set; }
        public bool CICIsRequired { get; set; }       
        public int WarrantyMonths { get; set; }
        public ObservableCollection<EquipmentAssignmentRecordModel> Assignments { get; set; }
        public ObservableCollection<DocumentModel> Documents { get; set; }
        public ObservableCollection<CICRecordModel>CICs { get; set; }
    }
}
