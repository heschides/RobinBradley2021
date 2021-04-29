using SimplyEmployeeTracker.Models;
using SimplyEmployeeTracker.Other;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SimplyEmployeeTracker.Views;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyEmployeeTracker.DataAccess;
using System.Windows.Input;
using System.Windows;

namespace SimplyEmployeeTracker.ViewModels
{
    public class EmployeeTabViewModel : ViewModelBase
    {
        //PROPERTIES

        public ObservableCollection<EmployeeModel> Employees { get; set; }
        public ObservableCollection<EquipmentAssignmentRecordModel> EquipmentAssignments_StandardIssue { get; set; }


        private EmployeeModel _selectedEmployee;
        public EmployeeModel SelectedEmployee
        {
            get { return _selectedEmployee; }
            set { OnPropertyChanged(ref _selectedEmployee, value); }
        }



        //COMMANDS

        public RelayCommand<object> OpenAddEmployeeWindowCommand { get; private set; }
        public static void OpenAddEmployee(object e)
        {
            var w = new CreateNewEmployeeRecord();
            w.Show();
        }
        public RelayCommand<object> RemoveEmployeeCommand { get; private set; }
        public void RemoveEmployee(object employee)
        {
            Employees.Remove(employee as EmployeeModel);
            DeleteData.DeleteEmployee(employee as EmployeeModel);
        }
        public RelayCommand<object> RefreshEmployeesCommand { get; private set; }
        public async void RefreshEmployees(object e)
        {
            var employees = await GetData.EmployeeQueryAsync();
            var IDs = new List<int>();
            foreach (EmployeeModel _employee in Employees) { IDs.Add(_employee.ID); }

            foreach (EmployeeModel _employee in employees)
            {
                if (IDs.Contains(_employee.ID))
                { }
                else { Employees.Add(_employee); }
            }
        }

        //CONSTRUCTORS
        public EmployeeTabViewModel()
        {
            Employees = new ObservableCollection<EmployeeModel>();
            SelectedEmployee = new EmployeeModel();
            RemoveEmployeeCommand = new RelayCommand<object>(RemoveEmployee);
            OpenAddEmployeeWindowCommand = new RelayCommand<object>(OpenAddEmployee);
            RefreshEmployeesCommand = new RelayCommand<object>(RefreshEmployees);
        }
    }
}
