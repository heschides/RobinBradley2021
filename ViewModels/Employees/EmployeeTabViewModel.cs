using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using RobinBradley2021.DataAccess;
using RobinBradley2021.Models;
using RobinBradley2021.Models.Tokens;
using RobinBradley2021.Views;
using RobinBradley2021.Views.Employees;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace RobinBradley2021.ViewModels
{
    public class EmployeeTabViewModel : ViewModelBase
    {
        
        //PROPERTIES
        public ObservableCollection<EmployeeModel> Employees { get; set; }
        private EmployeeModel _selectedEmployee;
        public EmployeeModel SelectedEmployee
        {
            get => 
                _selectedEmployee;
            set
            {
                {
                    if (Set(ref _selectedEmployee, value))
                    {
                        Messenger.Default.Send(new EmployeeToken(value));
                        RaisePropertyChanged(nameof(EquipmentAssignments_StandardIssue));
                        RaisePropertyChanged(nameof(EquipmentAssignments_AdHoc));
                    }
                }
            }
        }
        public ObservableCollection<VehicleAssignmentRecordModel> VehicleAssignments { get; set; }
        public ObservableCollection<DepartmentModel> Departments { get; set; }
        private DepartmentModel _selectedDepartment;
        public DepartmentModel SelectedDepartment
        {
            get { return _selectedDepartment; }
            set
            {
                _selectedDepartment = value;
                RaisePropertyChanged(nameof(EmployeeCollection));
            }
        }

        //COLLECTIONVIEWS
        private ICollectionView _employeeCollection;
        public ICollectionView EmployeeCollection 
        { 
            get
            {
                var _employeeCollectionView = new CollectionViewSource { Source = Employees }.View;

                if (SelectedDepartment == null)
                {
                    return _employeeCollection;
                }
               _employeeCollectionView.Filter = item => item is EmployeeModel model && model.Department.ToString() == SelectedDepartment.ToString();
                return _employeeCollectionView;
            }
            set { _employeeCollection= value; }
        }
        public ICollectionView EquipmentAssignments_StandardIssue
        {
            get
            {
                if (SelectedEmployee?.EquipmentAssignments == null)
                    return null;
                var view = new CollectionViewSource { Source = SelectedEmployee.EquipmentAssignments }.View;
                view.Filter = item => item is EquipmentAssignmentRecordModel model && model.IsStandardIssue;
                return view;
            }
        }
        public ICollectionView EquipmentAssignments_AdHoc
        {
            get
            {
                if (SelectedEmployee?.EquipmentAssignments == null)
                    return null;
                var view = new CollectionViewSource { Source = SelectedEmployee.EquipmentAssignments }.View;
                view.Filter = item => item is EquipmentAssignmentRecordModel model && model.IsStandardIssue == false;
                return view;
            }
        }

        //COMMANDS
        public RelayCommand<object> OpenAddEmployeeWindowCommand { get; private set; }
        public RelayCommand<object> RemoveEmployeeCommand { get; private set; }
        public RelayCommand<object> RefreshEmployeesCommand { get; private set; }
        public RelayCommand<object> OpenEditEmployeeWindowCommand { get; private set; }
        public RelayCommand<object> OpenAddEmployeeCertificationWindowCommand { get; private set; }
        //METHODS
        public void OpenAddEmployee(object e)
        {
            var w = new CreateNewEmployeeRecord();
            w.Show();
        }
        public void RemoveEmployee(object employee)
        {
            Employees.Remove(employee as EmployeeModel);
            EmployeeRepository.DeleteEmployee(employee as EmployeeModel);
        }
        public async void RefreshEmployees(object e)
        {
            var employees = await EmployeeRepository.EmployeeQueryAsync();
            var Ids = new List<int>();
            foreach (EmployeeModel _employee in Employees) { Ids.Add(_employee.Id); }
            foreach (EmployeeModel _employee in employees)
            {
                if (Ids.Contains(_employee.Id))
                { }
                else { Employees.Add(_employee); }
            }
        }
        public static void OpenAddEmployeeCertificationWindow(object e)
        {
            var w = new AddEmployeeCertificationWindow();
            w.Show();
        }
        public static void OpenEditEmployeeWindow(object e)
        {
            var window = new EditEmployeeWindow();
            window.Show();
        }
  

        //CONSTRUCTORS
        public EmployeeTabViewModel()
        {
            //properties
            Employees = new ObservableCollection<EmployeeModel>();
            VehicleAssignments = new ObservableCollection<VehicleAssignmentRecordModel>();
            Departments = new ObservableCollection<DepartmentModel>();
            //commands
            RemoveEmployeeCommand = new RelayCommand<object>(RemoveEmployee);
            RefreshEmployeesCommand = new RelayCommand<object>(RefreshEmployees);
            OpenAddEmployeeWindowCommand = new RelayCommand<object>(OpenAddEmployee);
            OpenEditEmployeeWindowCommand = new RelayCommand<object>(OpenEditEmployeeWindow);
            OpenAddEmployeeCertificationWindowCommand = new RelayCommand<object>(OpenAddEmployeeCertificationWindow);
            EmployeeCollection = CollectionViewSource.GetDefaultView(Employees);
           
        }
    }
}
