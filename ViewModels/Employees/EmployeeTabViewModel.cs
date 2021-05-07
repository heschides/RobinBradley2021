using RobinBradley2021.Models;
using RobinBradley2021.Other;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RobinBradley2021.Views;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobinBradley2021.DataAccess;
using System.Windows.Input;
using System.Windows;
using System.Windows.Data;
using System.ComponentModel;
using RobinBradley2021.Views.Employees;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using RobinBradley2021.Models.Tokens;

namespace RobinBradley2021.ViewModels
{
    public class EmployeeTabViewModel : ViewModelBase
    {
        //PROPERTIES
        public ObservableCollection<EmployeeModel> Employees { get; set; }
        private EmployeeModel _selectedEmployee;
        public EmployeeModel SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                if (Set(ref _selectedEmployee, value))
                {
                    Messenger.Default.Send(new EmployeeToken(value));
                    EquipmentAssignments = SelectedEmployee.EquipmentAssignments;
                }
            }
        }
        private ObservableCollection<EquipmentAssignmentRecordModel> _equipmentAssignments;
        public ObservableCollection<EquipmentAssignmentRecordModel> EquipmentAssignments
        {
            get { return _equipmentAssignments; }
            set
            {
                if (value != null)
                {
                    var result = value.Where(x => x.IsStandardIssue == true);
                    var resultCollection = new ObservableCollection<EquipmentAssignmentRecordModel>(result);
                    Set<ObservableCollection<EquipmentAssignmentRecordModel>>(ref _equipmentAssignments, resultCollection);
                }
            }
        }
        public string SelectedEmployeeName { get; set; }
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
            var Ids = new List<int>();
            foreach (EmployeeModel _employee in Employees) { Ids.Add(_employee.Id); }
            foreach (EmployeeModel _employee in employees)
            {
                if (Ids.Contains(_employee.Id))
                { }
                else { Employees.Add(_employee); }
            }
        }
        public RelayCommand<object> OpenAddEmployeeCertificationWindowCommand { get; private set; }
        public static void OpenAddEmployeeCertificationWindow(object e)
        {
            var w = new AddEmployeeCertificationWindow();
            w.Show();
        }
        public RelayCommand<object> OpenEditEmployeeWindowCommand { get; private set; }
        public static void OpenEditEmployeeWindow(object e)
        {
            var window = new EditEmployeeWindow();
            window.Show();
        }
        //CONSTRUCTORS
        public EmployeeTabViewModel()
        {
            Employees = new ObservableCollection<EmployeeModel>();
            SelectedEmployee = new EmployeeModel();
            EquipmentAssignments = new ObservableCollection<EquipmentAssignmentRecordModel>();
            RemoveEmployeeCommand = new RelayCommand<object>(RemoveEmployee);
            OpenAddEmployeeWindowCommand = new RelayCommand<object>(OpenAddEmployee);
            RefreshEmployeesCommand = new RelayCommand<object>(RefreshEmployees);
            OpenAddEmployeeCertificationWindowCommand = new RelayCommand<object>(OpenAddEmployeeCertificationWindow);
            OpenEditEmployeeWindowCommand = new RelayCommand<object>(OpenEditEmployeeWindow);
            SelectedEmployeeName = SelectedEmployee.FirstName;
        }
    }
}
