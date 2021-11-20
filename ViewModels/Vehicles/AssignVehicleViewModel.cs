using RobinBradley2021.DataAccess;
using RobinBradley2021.Models;
using RobinBradley2021.Models.Administration;
using RobinBradley2021.Models.BindingLists;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RobinBradley2021.ViewModels.Vehicles
{
    class AssignVehicleViewModel
    {

        //PROPERTIES
        public ObservableCollection<VehicleModel> VehiclesAvailable { get; set; }
        public ObservableCollection<EmployeeModel> Employees_VehicleEligible { get; set; }
        public ObservableCollection<JobsiteModel> Jobsites { get; set; }
        public ConditionsModel Conditions { get; set; }
        public FuelLevelModel FuelLevels { get; set; }
        public DateTime DateOut { get; set; }
        public DateTime DateDue { get; set; }
        public VehicleModel SelectedVehicle { get; set; }
        public EmployeeModel SelectedEmployee { get; set; }
        public JobsiteModel SelectedJobsite { get; set; }
        public bool IsStandardIssue { get; set; }
        public string SelectedConditionOut { get; set; }
        public string SelectedFuelOut { get; set; }
        public string SelectedCondition { get; set; }


        //COMMANDs
        public RelayCommand<object> LoadIntialDataCommand { get; set; }
        public RelayCommand<object> CreateVehicleAssignmentCommand { get; set; }

        //METHODS
        public async void LoadInitialData(object e)
        {
            var vehiclesAvailable = await VehicleRepository.VehiclesAvailableQueryAsync();
            foreach (VehicleModel _vehicle in vehiclesAvailable)
                VehiclesAvailable.Add(_vehicle);
            var employees_VehiclesEligible = await EmployeeRepository.Employees_VehiclesEligibleQueryAsync();
            foreach (EmployeeModel _employee in employees_VehiclesEligible)
                Employees_VehicleEligible.Add(_employee);
            var jobsites = await AdministrationRepository.JobsiteQueryAsync();
            foreach(JobsiteModel _jobsite in jobsites)
                Jobsites.Add(_jobsite);
            Conditions = new ConditionsModel();
            var _assignments = await VehicleRepository.VehicleAssignmentQueryAsync();

        } 
        public void CreateVehicleAssignment(object e)
        {
            var _vehicleAssignment = new VehicleAssignmentRecordModel();
            _vehicleAssignment.Assignee = SelectedEmployee;
            _vehicleAssignment.AssignedVehicle = SelectedVehicle;
            _vehicleAssignment.ConditionOut = SelectedCondition;
            _vehicleAssignment.FuelLevelOut = SelectedFuelOut;
            _vehicleAssignment.DateOut = DateOut;
            _vehicleAssignment.DueDate = DateDue;
            if ( IsStandardIssue == false)
            { 
                _vehicleAssignment.Jobsite = SelectedJobsite;
                _vehicleAssignment.DueDate = DateDue;
            }
            _vehicleAssignment.IsStandardIssue = true;
            _vehicleAssignment.IsResolved = false;
            VehicleRepository.CreateVehicleAssignmentRecord(_vehicleAssignment);
            
        }


        public AssignVehicleViewModel()
        {
            VehiclesAvailable = new ObservableCollection<VehicleModel>();
            Employees_VehicleEligible = new ObservableCollection<EmployeeModel>();
            Jobsites = new ObservableCollection<JobsiteModel>();
            FuelLevels = new FuelLevelModel();
            LoadIntialDataCommand = new RelayCommand<object>(LoadInitialData);
            CreateVehicleAssignmentCommand = new RelayCommand<object>(CreateVehicleAssignment);
            SelectedEmployee = new EmployeeModel();
            Conditions = new ConditionsModel();
            DateOut = new DateTime();
            DateDue = new DateTime();
            
        }

    }
}
