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
       public  EquipmentTabViewModel EquipmentTabVM { get; set; }
        public EmployeeTabViewModel EmployeeTabVM { get; set; }
        public VehicleTabViewModel VehicleTabVM { get; set; }

        public RelayCommand <object>LoadInitialDataCommand { get; set; }
        public async void LoadInitialData(object e)
        {
            var employees = await GetData.EmployeeQueryAsync();
            var Ids = new List<int>();
            foreach (EmployeeModel _employee in EmployeeTabVM.Employees) { Ids.Add(_employee.Id); }

            foreach (EmployeeModel _employee in employees)
            {
                if (Ids.Contains(_employee.Id))
                { }
                else { EmployeeTabVM.Employees.Add(_employee); }
            }

            var equipment = await GetData.EquipmentQueryAsync();
            var eIds = new List<int>();
            foreach (EquipmentModel _equipment in EquipmentTabVM.Equipment) { eIds.Add(_equipment.Id); }

            foreach (EquipmentModel _equipment in equipment)
            {
                if (eIds.Contains(_equipment.Id))
                { }
                else { EquipmentTabVM.Equipment.Add(_equipment); }
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

