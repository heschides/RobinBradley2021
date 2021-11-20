using GalaSoft.MvvmLight;
using RobinBradley2021.Models;
using RobinBradley2021.Views.Vehicles;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using static RobinBradley2021.DataAccess.VehicleRepository;

namespace RobinBradley2021.ViewModels
{
    public class VehicleTabViewModel : ViewModelBase
    {

        // Properties
        public ObservableCollection<VehicleModel> Vehicles { get; set; }
        public ObservableCollection<VehicleAssignmentRecordModel> Assignments { get; set; }
        public ObservableCollection<VehicleInvoiceModel> Invoices { get; set; }
        public List<VehicleModel> BlankList = new List<VehicleModel>();

        private VehicleModel _selectedVehicle;
        public VehicleModel SelectedVehicle
        {
            get { return _selectedVehicle; }
            set
            {
                _selectedVehicle = value;
                RaisePropertyChanged(nameof(AssignmentCollection));
                RaisePropertyChanged(nameof(InvoiceCollection));
            }
        }

        private VehicleInvoiceModel _selectedInvoice;
        public VehicleInvoiceModel SelectedInvoice
        {
            get { return _selectedInvoice; }
            set
            {
                _selectedInvoice = value;
                RaisePropertyChanged(nameof(LineItemCollection));
            }
        }


        // CollectionViews
        private ICollectionView _assignmentCollection;
        public ICollectionView AssignmentCollection
        {
            get
            {
                var _assignmentCollectionView = new CollectionViewSource { Source = Assignments }.View;
                if (SelectedVehicle == null)
                {
                    return _assignmentCollection;
                }
                _assignmentCollectionView.Filter = item => item is VehicleAssignmentRecordModel model && model.AssignedVehicle.Id.ToString() == SelectedVehicle.Id.ToString();
                return _assignmentCollectionView;
            }
            set { _assignmentCollection = value; }
        }
        private ICollectionView _invoiceCollection;
        public ICollectionView InvoiceCollection
        {
            get
            {
                var _invoiceCollectionView = new CollectionViewSource { Source = Invoices }.View;
                if (SelectedVehicle == null)
                {
                    return _invoiceCollection; 
                }
                _invoiceCollectionView.Filter = item => item is VehicleInvoiceModel model && model.VehicleServiced.Id.ToString() == SelectedVehicle.Id.ToString();
                return _invoiceCollectionView;
            }
            set { _invoiceCollection = value; }
        }
        private ICollectionView _lineItemCollection;
        public ICollectionView LineItemCollection
        {
            get
            {

                if (SelectedInvoice == null)
                {
                   return _lineItemCollection;
                }
                var _lineItemCollectionView = new CollectionViewSource { Source = SelectedInvoice.LineItems }.View;
                _lineItemCollectionView.Filter = item => item is InvoiceLineItemModel model && model.RecordId.ToString() == SelectedInvoice.Id.ToString();
                return _lineItemCollectionView;
            }

            set { _lineItemCollection = value; }
        }



        // Commands
        public RelayCommand<object> RefreshVehiclesCommand { get; private set; }
        public async void RefreshVehicles(object e)
        {

            var _vehicles = await VehicleQueryAsync();
            var Ids = new List<int>();
            foreach (VehicleModel _vehicle in Vehicles)
            {
                Ids.Add(_vehicle.Id);
            }
            foreach (VehicleModel _vehicle in _vehicles)
            {
                if (Ids.Contains(_vehicle.Id))
                { }
                else { Vehicles.Add(_vehicle); }
            }
        }
        public RelayCommand<object> OpenAssignVehicleWindowCommand { get; set; }
        public void OpenAssignVehicleWindow(object e)
        {
            var window = new AssignVehicleWindow();
            window.Show();
        }
        public RelayCommand<object> OpenReturnVehicleWindowCommand { get; set; }
        public void OpenReturnVehicleWindow(object e)
        {
            var window = new ReturnVehicleWindow();
            window.Show();
        }
        public RelayCommand<object> OpenRecordInvoiceWindowCommand { get; set; }
        public void OpenRecordInvoiceWindow(object e)
        {
            var window = new VehicleInvoiceCaptureWindow();
            window.Show();
        }
        public RelayCommand<object> OpenAddVehicleWindowCommand { get; set; }
        public void OpenAddVehicleWindow(object e)
        {
            var window = new CreateVehicleWindow();
            window.Show();
        }



        #region Constructor
        public VehicleTabViewModel()
        {
            Vehicles = new ObservableCollection<VehicleModel>();
            RefreshVehiclesCommand = new RelayCommand<object>(RefreshVehicles);
            OpenAssignVehicleWindowCommand = new RelayCommand<object>(OpenAssignVehicleWindow);
            OpenReturnVehicleWindowCommand = new RelayCommand<object>(OpenReturnVehicleWindow);
            OpenRecordInvoiceWindowCommand = new RelayCommand<object>(OpenRecordInvoiceWindow);
            OpenAddVehicleWindowCommand = new RelayCommand<object>(OpenAddVehicleWindow);
            Invoices = new ObservableCollection<VehicleInvoiceModel>();
            SelectedInvoice = new VehicleInvoiceModel();
            SelectedInvoice.LineItems = new ObservableCollection<InvoiceLineItemModel>();
            InvoiceCollection = CollectionViewSource.GetDefaultView(BlankList);
            LineItemCollection = CollectionViewSource.GetDefaultView(SelectedInvoice.LineItems);
            Assignments = new ObservableCollection<VehicleAssignmentRecordModel>();
            AssignmentCollection = CollectionViewSource.GetDefaultView(BlankList);
        }

        #endregion


    }
}
