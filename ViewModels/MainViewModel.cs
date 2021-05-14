using GalaSoft.MvvmLight;
using RobinBradley2021.DataAccess;
using RobinBradley2021.Models;
using RobinBradley2021.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RobinBradley2021.ViewModels.EmployeeTabViewModel;
namespace RobinBradley2021.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        //PROPERTIES
        public EquipmentTabViewModel EquipmentTabVM { get; private set; }
        public EmployeeTabViewModel EmployeeTabVM { get; private set; }
        public EditEmployeeWindowViewModel EditEmployeeVM { get; private set; }
        public VehicleTabViewModel VehicleTabVM { get; private set; }
        public DashboardViewModel DashboardVM { get; private set; }
        //COMMANDS
        public RelayCommand<object> LoadInitialDataCommand { get; set; }
        //METHODS
        //The loops in the LoadInitialData Method allow the asynchronous queries to be completed before the binding takes place.
        public async void LoadInitialData(object e)
        {
            var employees = await GetData.EmployeeQueryAsync();
            foreach (EmployeeModel _employee in employees)
            {
                EmployeeTabVM.Employees.Add(_employee);
            }

            var equipment = await GetData.EquipmentQueryAsync();
            var eIds = new List<int>();
            foreach (EquipmentModel _equipment in EquipmentTabVM.Equipment) { eIds.Add(_equipment.Id); }
            foreach (EquipmentModel _equipment in equipment)
            {
                if (eIds.Contains(_equipment.Id))
                { }
                else
                {
                    EquipmentTabVM.Equipment.Add(_equipment);
                }
            }

            var vehicles = await GetData.VehicleQueryAsync();
            var vIds = new List<int>();
            foreach (VehicleModel _vehicle in VehicleTabVM.Vehicles) { vIds.Add(_vehicle.Id); }
            foreach (VehicleModel _vehicle in vehicles)
            {
                if (vIds.Contains(_vehicle.Id))
                { }
                else { VehicleTabVM.Vehicles.Add(_vehicle); }

                DashboardVM.LoadInitialData();
            }
        }

        //CONSTRUCTOR
        public MainViewModel()
        {
            EquipmentTabVM = new EquipmentTabViewModel();
            EmployeeTabVM = new EmployeeTabViewModel();
            EditEmployeeVM = new EditEmployeeWindowViewModel();
            VehicleTabVM = new VehicleTabViewModel();
            DashboardVM = new DashboardViewModel();
            
            LoadInitialDataCommand = new RelayCommand<object>(LoadInitialData);
        }

    }
}

