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
        public EquipmentTabViewModel EquipmentTabVM { get; set; }
        public EmployeeTabViewModel EmployeeTabVM { get; set; }
        public EditEmployeeWindowViewModel EditEmployeeVM { get; set; }
        public VehicleTabViewModel VehicleTabVM { get; set; }
        public RelayCommand<object> LoadInitialDataCommand { get; set; }

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
            }
        }

        public MainViewModel()
        {
            EquipmentTabVM = new EquipmentTabViewModel();
            EmployeeTabVM = new EmployeeTabViewModel();
            EditEmployeeVM = new EditEmployeeWindowViewModel();
            VehicleTabVM = new VehicleTabViewModel();
            LoadInitialDataCommand = new RelayCommand<object>(LoadInitialData);
        }

    }
}

