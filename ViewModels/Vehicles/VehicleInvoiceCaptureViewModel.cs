using RobinBradley2021.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobinBradley2021.ViewModels.Vehicles
{
    class VehicleInvoiceCaptureViewModel
    {
        //PROPERTIES
        public ObservableCollection<VehicleModel> Vehicles { get; set; }
        public InvoiceLineItemModel LineOne { get; set; }
        public InvoiceLineItemModel LineTwo { get; set; }
        public InvoiceLineItemModel LineThree { get; set; }
        public InvoiceLineItemModel LineFour { get; set; }
        public InvoiceLineItemModel LineFive { get; set; }
        public InvoiceLineItemModel LineSix { get; set; }
        public InvoiceLineItemModel LineSeven { get; set; }
        public InvoiceLineItemModel LineEight { get; set; }
        public InvoiceLineItemModel LineNine { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceNumber { get; set; }
        public string Provider { get; set; }
        public VehicleModel SelectedVehicle { get; set; }
        
        //COMMANDS
        public RelayCommand<object> CaptureVehicleInvoiceCommand { get; set; }
        public RelayCommand<object> LoadInitialDataCommand { get; set; }
        
        //METHODS
        public async void LoadInitialData (object e)
        {
            var _vehicles = new ObservableCollection<VehicleModel>();
            _vehicles = await DataAccess.VehicleRepository.VehicleQueryAsync();
            foreach (VehicleModel _vehicle in _vehicles)
            {
                Vehicles.Add(_vehicle);
            }
        }
        
        public void CaptureVehicleInvoice(object e)
        {
            var _invoice = new VehicleInvoiceModel();
            _invoice.LineItems = new ObservableCollection<InvoiceLineItemModel>();
            if (LineOne != null) { _invoice.LineItems.Add(LineOne); }
            if (LineTwo != null) { _invoice.LineItems.Add(LineTwo); }
            if (LineThree != null) { _invoice.LineItems.Add(LineThree); }
            if (LineFour != null) { _invoice.LineItems.Add(LineFour); }
            if (LineFive != null) { _invoice.LineItems.Add(LineFive); }
            if (LineSix != null) { _invoice.LineItems.Add(LineSix); }
            if (LineSeven != null) { _invoice.LineItems.Add(LineSeven); }
            if (LineEight != null) { _invoice.LineItems.Add(LineEight); }
            if (LineNine != null) { _invoice.LineItems.Add(LineNine); }
            _invoice.InvoiceDate = InvoiceDate;
            _invoice.InvoiceNumber = InvoiceNumber;
            _invoice.Provider = Provider;
            _invoice.VehicleServiced = SelectedVehicle;
            DataAccess.VehicleRepository.CreateVehicleInvoiceRecord(_invoice);
            LineOne = null;
            LineTwo = null;
            LineThree = null;
            LineFour = null;
            LineFive = null;
            LineSix = null;
            LineSeven = null;
            LineEight = null;
            LineNine = null;
            InvoiceDate = DateTime.MinValue;
            Provider = null;
        }

        //CONSTRUCTOR
        public VehicleInvoiceCaptureViewModel()
        {
            Vehicles = new ObservableCollection<VehicleModel>();
            LoadInitialDataCommand = new RelayCommand<object>(LoadInitialData);
            CaptureVehicleInvoiceCommand = new RelayCommand<object>(CaptureVehicleInvoice);
            SelectedVehicle = new VehicleModel();
            LineOne = new InvoiceLineItemModel();
            LineTwo = new InvoiceLineItemModel();
            LineThree = new InvoiceLineItemModel();
            LineFour = new InvoiceLineItemModel();
            LineFive = new InvoiceLineItemModel();
            LineSix = new InvoiceLineItemModel();
            LineSeven = new InvoiceLineItemModel();
            LineEight = new InvoiceLineItemModel();
            LineNine = new InvoiceLineItemModel();
        }

    }
}
