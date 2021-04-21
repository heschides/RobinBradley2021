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

        public RelayCommand <object>LoadInitialDataCommand { get; set; }
        public async void LoadInitialData(object e)
        {
            var employees = await GetData.EmployeeQueryAsync();
            var IDs = new List<int>();
            foreach (EmployeeModel _employee in EmployeeTabVM.Employees) { IDs.Add(_employee.ID); }

            foreach (EmployeeModel _employee in employees)
            {
                if (IDs.Contains(_employee.ID))
                { }
                else { EmployeeTabVM.Employees.Add(_employee); }
            }

            var equipment = await GetData.EquipmentQueryAsync();
            var eIDs = new List<int>();
            foreach (EquipmentModel _equipment in EquipmentTabVM.Equipment) { eIDs.Add(_equipment.ID); }

            foreach (EquipmentModel _equipment in equipment)
            {
                if (eIDs.Contains(_equipment.ID))
                { }
                else { EquipmentTabVM.Equipment.Add(_equipment); }
            }

        }
       
        public MainViewModel()
        {
            EquipmentTabVM = new EquipmentTabViewModel();
            EmployeeTabVM = new EmployeeTabViewModel();
            LoadInitialDataCommand = new RelayCommand<object>(LoadInitialData);
        }
    
    } 

}

