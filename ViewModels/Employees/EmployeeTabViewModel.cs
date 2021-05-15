﻿using RobinBradley2021.Models;
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
using System.Runtime.CompilerServices;

namespace RobinBradley2021.ViewModels
{
    public class EmployeeTabViewModel : ViewModelBase
    {
        //PROPERTIES
        public ObservableCollection<EmployeeModel> Employees { get; set; }
        private EmployeeModel _selectedEmployee;
        public EmployeeModel SelectedEmployee
        {
            get => _selectedEmployee;
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
                view.Filter = item => item is EquipmentAssignmentRecordModel model && model.IsStandardIssue;
                return view;

            }
        }

        public ObservableCollection<VehicleAssignmentRecordModel> VehicleAssignments { get; set; }

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
            DeleteData.DeleteEmployee(employee as EmployeeModel);
        }
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
            SelectedEmployee = new EmployeeModel();
            VehicleAssignments = new ObservableCollection<VehicleAssignmentRecordModel>();
            //commands
            RemoveEmployeeCommand = new RelayCommand<object>(RemoveEmployee);
            RefreshEmployeesCommand = new RelayCommand<object>(RefreshEmployees);
            OpenAddEmployeeWindowCommand = new RelayCommand<object>(OpenAddEmployee);
            OpenEditEmployeeWindowCommand = new RelayCommand<object>(OpenEditEmployeeWindow);
            OpenAddEmployeeCertificationWindowCommand = new RelayCommand<object>(OpenAddEmployeeCertificationWindow);
        }
    }
}
