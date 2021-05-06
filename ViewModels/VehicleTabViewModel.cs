using SimplyEmployeeTracker.Models;
using SimplyEmployeeTracker.Other;
using SimplyEmployeeTracker.Views;
using SimplyEmployeeTracker.Views.Vehicles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimplyEmployeeTracker.DataAccess.GetData;

namespace SimplyEmployeeTracker.ViewModels
{
    public class VehicleTabViewModel : ViewModelBase
    {

        #region Properties
        public ObservableCollection <VehicleModel>Vehicles { get; set; }
        #endregion

        #region Commands
        public RelayCommand<object> RefreshVehiclesCommand { get; private set; }
        public async void RefreshVehicles(object e)
        {

            var _vehicles = await VehicleQueryAsync();
            var Ids = new List<int>();
            foreach (VehicleModel _vehicle in Vehicles) { 
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

        public RelayCommand<object>OpenReturnVehicleWindowCommand { get; set; }
        public void OpenReturnVehicleWindow(object e)
        {
            var window = new ReturnVehicleWindow();
            window.Show();
        }

        public RelayCommand<object>OpenRecordInvoiceWindowCommand { get; set; }
        public void OpenRecordInvoiceWindow(object e)
        {
            var window = new RecordInvoiceWindow();
            window.Show();
        }

        public RelayCommand<object> OpenAddVehicleWindowCommand { get; set; }
        public void OpenAddVehicleWindow(object e)
        {
            var window = new CreateVehicleWindow();
            window.Show();
        }

        #endregion

        #region Constructor
        public VehicleTabViewModel()
        {
            Vehicles = new ObservableCollection<VehicleModel>();
            RefreshVehiclesCommand = new RelayCommand<object>(RefreshVehicles);
            OpenAssignVehicleWindowCommand = new RelayCommand<object>(OpenAssignVehicleWindow);
            OpenReturnVehicleWindowCommand = new RelayCommand<object>(OpenReturnVehicleWindow);
            OpenRecordInvoiceWindowCommand = new RelayCommand<object>(OpenRecordInvoiceWindow);
            OpenAddVehicleWindowCommand = new RelayCommand<object>(OpenAddVehicleWindow);
        }

        #endregion


    }
}
