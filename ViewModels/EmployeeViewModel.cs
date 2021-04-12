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

namespace SimplyEmployeeTracker.ViewModels
{
   public class EmployeeViewModel : ViewModelBase
    {
        //PROPERTIES

        public static ObservableCollection<EmployeeModel> Employees { get; set; }

        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set { OnPropertyChanged(ref _firstName, value); }
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set { OnPropertyChanged(ref _lastName, value); }
        }
        private DateTime _hireDate;

        public DateTime HireDate
        {
            get { return _hireDate; }
            set { OnPropertyChanged(ref _hireDate, value); }
        }

        //COMMANDS

        public RelayCommand AddEmployeeCommand { get; private set; }
        public void AddEmployee(object employee)
        {
            EmployeeModel newEmployee = new EmployeeModel();
            newEmployee.FirstName = FirstName;
            newEmployee.LastName = LastName;
            newEmployee.HireDate = HireDate;
            Employees.Add(newEmployee);
            DataAccess.SendData.CreateEmployee(newEmployee);
        }

        public RelayCommand OpenAddEmployeeWindowCommand { get; private set; }
        public void OpenAddEmployee(object e)
        {
            var w = new CreateNewEmployeeRecord();
            w.Show();
        }

        public RelayCommand RemoveEmployeeCommand { get; private set; }
        public void RemoveEmployee(object employee)
        {
            Employees.Remove(employee as EmployeeModel);
            DeleteData.DeleteEmployee(employee as EmployeeModel);
        }


        //CONSTRUCTORS

        public EmployeeViewModel()
        {
            AddEmployeeCommand = new RelayCommand(AddEmployee);
            RemoveEmployeeCommand = new RelayCommand(RemoveEmployee);
            OpenAddEmployeeWindowCommand = new RelayCommand(OpenAddEmployee);
        }

        public static async Task<EmployeeViewModel> CreateEmployeeViewModelAsync()
        {
            var employeeViewModel = new EmployeeViewModel();
            Employees = await GetData.EmployeeQueryAsync();
            return employeeViewModel;
        }
    }
}
