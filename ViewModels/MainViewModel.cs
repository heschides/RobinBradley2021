using GalaSoft.MvvmLight;
using RobinBradley2021.DataAccess;
using RobinBradley2021.Models;
using RobinBradley2021.ViewModels.Vehicles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using static RobinBradley2021.DataAccess.VehicleRepository;
using static RobinBradley2021.DataAccess.EmployeeRepository;
using System.Collections.ObjectModel;
using RobinBradley2021.Models.Equipment;
using RobinBradley2021.Models.Employees;

namespace RobinBradley2021.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        //PROPERTIES
        public EquipmentTabViewModel EquipmentTabVM { get; private set; }
        public EmployeeTabViewModel EmployeeTabVM { get; private set; }
        public EditEmployeeWindowViewModel EditEmployeeVM { get; private set; }
        public VehicleTabViewModel VehicleTabVM { get; private set; }
        public DashboardViewModel DashboardVM { get; private set; }
        public CreateVehicleViewModel CreateVehicleVM { get; private set; }
        public DateTime now = DateTime.Now;
        public List<EquipmentAssignmentRecordModel> LongEquipmentList { get; set; }

        //COMMANDS
        public RelayCommand<object> LoadInitialDataCommand { get; set; }
        //METHODS

        public async void LoadInitialData(object e)
        {
            var _equipmentAssignments = await EquipmentRepository.GetEquipmentAssignments();
            var vehicles = await VehicleQueryAsync();
            var employees = await EmployeeRepository.EmployeeQueryAsync();
            var equipment = await EquipmentRepository.EquipmentQueryAsync();
            var departments = await AdministrationRepository.DepartmentQueryAsync();
            var eIds = new List<int>();
            var vIds = new List<int>();
            //The loops in the LoadInitialData Method allow the asynchronous queries to be completed before the binding takes place.

            foreach (EmployeeModel _employee in employees)
            {
                foreach (CertificationRecordModel _cert in _employee.Certifications)
                {
                    DashboardVM.Certifications.Add(_cert);
                }
                EmployeeTabVM.Employees.Add(_employee);

            }
            foreach (DepartmentModel _department in departments)
            {
                EmployeeTabVM.Departments.Add(_department);
            }
            foreach (EquipmentModel _equipment in EquipmentTabVM.Equipment)
            {
                eIds.Add(_equipment.Id);
            }
            foreach (EquipmentModel _equipment in equipment)
            {
                if (_equipment.CICs != null)
                {
                    foreach (CICRecordModel _cic in _equipment.CICs)
                    {
                        DashboardVM.CIC.Add(_cic);
                    }
                }

                if (eIds.Contains(_equipment.Id))
                { }
                else
                {
                    EquipmentTabVM.Equipment.Add(_equipment);
                }
            }
            foreach (EquipmentAssignmentRecordModel _record in _equipmentAssignments)
            {
                DashboardVM.EquipmentAssignments.Add(_record);
            }
            foreach (VehicleModel _vehicle in VehicleTabVM.Vehicles) { vIds.Add(_vehicle.Id); }
            foreach (VehicleModel _vehicle in vehicles)
            {
                if (vIds.Contains(_vehicle.Id))
                { }
                else { VehicleTabVM.Vehicles.Add(_vehicle); }
            }
            foreach (VehicleModel _vehicle in vehicles)
            {
                foreach (VehicleAssignmentRecordModel _record in _vehicle.Assignments)
                {
                    DashboardVM.VehicleAssignments.Add(_record);
                }
            }

            }

            //CONSTRUCTOR
            public MainViewModel()
            {
                LoadInitialDataCommand = new RelayCommand<object>(LoadInitialData);
                EquipmentTabVM = new EquipmentTabViewModel();
                EmployeeTabVM = new EmployeeTabViewModel();
                EditEmployeeVM = new EditEmployeeWindowViewModel();
                VehicleTabVM = new VehicleTabViewModel();
                DashboardVM = new DashboardViewModel();
            }
        }
    }

