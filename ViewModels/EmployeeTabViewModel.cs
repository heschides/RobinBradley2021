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
                RemoveEmployeeCommand = new RelayCommand<object>(RemoveEmployee);
                OpenAddEmployeeWindowCommand = new RelayCommand<object>(OpenAddEmployee);
                RefreshEmployeesCommand = new RelayCommand<object>(RefreshEmployees);
            }
        } 
}
