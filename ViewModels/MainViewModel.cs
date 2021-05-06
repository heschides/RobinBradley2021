using SimplyEmployeeTracker.DataAccess;
using SimplyEmployeeTracker.Models;
using SimplyEmployeeTracker.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimplyEmployeeTracker.ViewModels.EmployeeTabViewModel;
namespace SimplyEmployeeTracker.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public EquipmentTabViewModel EquipmentTabVM { get; set; }
        public EmployeeTabViewModel EmployeeTabVM { get; set; }
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
            VehicleTabVM = new VehicleTabViewModel();
            LoadInitialDataCommand = new RelayCommand<object>(LoadInitialData);
        }

    }
}

