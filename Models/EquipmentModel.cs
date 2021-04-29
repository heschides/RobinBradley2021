using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyEmployeeTracker.Models
{
    public class EquipmentModel
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _inventoryID;
        public string InventoryID
        {
            get { return _inventoryID; }
            set { _inventoryID = value; }
        }

        private string  _description;
        public string  Description
        {
            get { return _description; }
            set { _description = value; }
        }
        private EquipmentClassModel _class;
        public EquipmentClassModel Class
        {
            get { return _class; }
            set { _class = value; }
        }

        private EquipmentStatusModel _status;
        public EquipmentStatusModel Status
        {
            get { return _status; }
            set { _status = value; }
        }
        private string _brand;
        public string Brand
        {
            get { return _brand; }
            set { _brand = value; }
        }
        private string _model;
        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }

        private string _serialNumber;
        public string SerialNumber
        {
            get { return _serialNumber; }
            set { _serialNumber = value; }
        }

        private string _purchaseOrderNumber;
        public string PurchaseOrderNumber
        {
            get { return _purchaseOrderNumber; }
            set { _purchaseOrderNumber = value; }
        }

        private DateTime  _purchaseDate;
        public DateTime  PurchaseDate
        {
            get { return _purchaseDate; }
            set { _purchaseDate = value; }
        }

        private decimal _price;
        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        private bool _cicIsRequred;
        public bool CICIsRequired
        {
            get { return _cicIsRequred; }
            set { _cicIsRequred = value; }
        }
        private int _warrantyMonths;
        public int WarrantyMonths
        {
            get { return _warrantyMonths; }
            set { _warrantyMonths = value; }
        }

        private ObservableCollection<EquipmentAssignmentRecordModel> _assignments;
        public ObservableCollection<EquipmentAssignmentRecordModel> Assignments
        {
            get { return _assignments; }
            set { _assignments = value; }
        }
        public ObservableCollection<DocumentModel> _documents;
        public ObservableCollection<DocumentModel> Documents
        {
            get { return _documents; }
            set { _documents = value; }
        }
    }
}
