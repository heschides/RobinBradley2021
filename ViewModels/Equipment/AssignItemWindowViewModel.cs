using GalaSoft.MvvmLight;
using RobinBradley2021.DataAccess;
using RobinBradley2021.Models;
using RobinBradley2021.Other;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RobinBradley2021.ViewModels
{
    public class AssignItemWindowViewModel : ViewModelBase
    {

        //PROPERTIES
        public EquipmentAssignmentRecordModel NewEquipmentAssignmentRecord { get; private set; }
        public ObservableCollection<EquipmentModel> SelectedItems { get; set; }
        public ObservableCollection<EmployeeModel> AuthorizedEmployees { get; set; }
        public ObservableCollection<EquipmentModel> Equipment { get; set; }
        public ObservableCollection<DepartmentModel> Departments { get; set; }
        public ObservableCollection<EquipmentModel> AssignedItems { get; set; }
        public ObservableCollection<JobsiteModel>Jobsites { get; set; }

        private bool _isStandardIssue;
        public bool IsStandardIssue
        {
            get { return _isStandardIssue; }
            set { Set(ref _isStandardIssue, value); }
        }

        private bool _isIndividual;
        public bool IsIndividual
        {
            get { return _isIndividual; }
            set { Set(ref _isIndividual, value); }
        }

        private bool _isDepartment;
        public bool IsDepartment
        {
            get { return _isDepartment; }
            set { Set(ref _isDepartment, value); }
        }

        private DateTime _assignedDate;
        public DateTime AssignedDate
        {
            get { return _assignedDate; }
            set { Set(ref _assignedDate, value); }
        }

        private DateTime _dueDate;
        public DateTime DueDate
        {
            get { return _dueDate; }
            set { Set(ref _dueDate, value); }
        }

        private string _addedItem;
        public string AddedItem
        {
            get { return _addedItem; }
            set { Set(ref _addedItem, value); }
        }

        private JobsiteModel _destination;
        public JobsiteModel Destination
        {
            get { return _destination; }
            set { Set(ref _destination, value); }
        }


        private EquipmentModel _selectedItem;
        public EquipmentModel SelectedItem
        {
            get { return _selectedItem; }
            set { Set(ref _selectedItem, value); }
        }

        private EmployeeModel _selectedEmployee;
        public EmployeeModel SelectedEmployee
        {
            get { return _selectedEmployee; }
            set { Set(ref _selectedEmployee, value); }
        }

        private DepartmentModel _selectedDepartment;
        public DepartmentModel SelectedDepartment
        {
            get { return _selectedDepartment; }
            set { Set(ref _selectedDepartment, value); }
        }

        


        //COMMANDS
        public RelayCommand<object> LoadDataSourcesCommand { get; private set; }
        public async void LoadDataSources(object e)
        {
            var equipment = await EquipmentRepository.EquipmentQueryAsync();
            var employees = await EmployeeRepository.EmployeeQueryAsync();
            var departments = await AdministrationRepository.DepartmentQueryAsync();
            var jobsites = await AdministrationRepository.JobsiteQueryAsync();

            foreach (EquipmentModel item in equipment)
            {
                Equipment.Add(item);
            }
            foreach (DepartmentModel department in departments)
            {
                Departments.Add(department);
            }
            foreach (JobsiteModel jobsite in jobsites)
            { 
                Jobsites.Add(jobsite); 
            }

            var today = DateTime.Now;
            var authorizedEmployees = employees.Where(x => !x.Restrictions.Any(x => x.Name == "Equipment Assignments" && x.EndDate >= today));
            foreach (EmployeeModel employee in authorizedEmployees)
            {
                AuthorizedEmployees.Add(employee);
            }
        }

        public RelayCommand<object> AddItemToListCommand { get; private set; }
        public void AddItemToList(object e)
        {
            if (AddedItem != null)
            {
                var nextItem = Equipment.First(x => x.InventoryId == AddedItem);
                if (!SelectedItems.Contains(nextItem))
                {
                    SelectedItems.Add(nextItem);
                }

            }
            if (SelectedItem != null && !SelectedItems.Contains(SelectedItem))
            {
                SelectedItems.Add(SelectedItem);
            }
            SelectedItem = null;
            AddedItem = null;
        }

        public RelayCommand<object> CreateAssignmentRecordCommand { get; private set; }
        public void CreateAssignmentRecord(object e)
        {
            AssembleData(NewEquipmentAssignmentRecord);
            ValidateData(NewEquipmentAssignmentRecord);
            EquipmentRepository.CreateEquipmentAssignmentRecord(NewEquipmentAssignmentRecord);
        }

        //ASSEMBLE DATA
        public void AssembleData(EquipmentAssignmentRecordModel record)
        {
            //record.DateOut = AssignedDate;
            //record.SelectedItems = SelectedItems.ToList();
            //record.IsDepartment = IsDepartment;
            //record.IsStandardIssue = IsStandardIssue;

            //if (record.IsStandardIssue == false)
            //{
            //    record.DueDate = DueDate;
            //    record.Jobsite = Destination;
            //}
            //if(record.IsDepartment)
            //{
            //    record.AssigneeDepartment.Id = SelectedDepartment.Id;
            //}
            //if (IsDepartment == false)
            //{
            //    record.AssigneePerson.Id = SelectedEmployee.Id;
            //} 
        }
        
        //DATA VALIDATION
        public void ValidateData(EquipmentAssignmentRecordModel record)
        {
            StringBuilder message = new StringBuilder();
            if (record.IsDepartment == false && SelectedEmployee == null)
            {
                string userMessage = "Please select an employee to receive the assignment.";
                message.Append(userMessage);
                message.AppendLine(); 
                message.AppendLine();
            }
            if (record.IsDepartment && SelectedDepartment == null)
            {
                string userMessage = "Please select a department to receive the assignment.";
                message.Append(userMessage);
                message.AppendLine();
                message.AppendLine();
            }

            if (record.IsStandardIssue == false && DueDate == default)
            {
                string dueDateMessage = "You have selected NO to Standard Issue.  Therefore, please select a due date for the selected items.";
                message.Append(dueDateMessage);
                message.AppendLine();
                message.AppendLine();
            }
            if (record.DateOut == default)
            {
                string assignedDateMessage = "Please select an assignment date.";
                message.Append(assignedDateMessage);
                message.AppendLine();
                message.AppendLine();
            }

            //if (record.SelectedItems == null)
            //{
            //    string selectedItemsMessage = "Please select one or more items to create an assignment.";
            //    message.AppendLine(selectedItemsMessage);
            //    message.AppendLine();
            //    message.AppendLine();
            //}

            if (message.Length == 0)
            {
                string successMessage = "The items have been assigned successfully.";
                message.AppendLine(successMessage);
                message.AppendLine();
            }

            MessageBox.Show(message.ToString(), "Adjustment Needed");
            message.Clear();
        }

        //CONSTRUCTOR
        public AssignItemWindowViewModel()
        {
            NewEquipmentAssignmentRecord = new EquipmentAssignmentRecordModel();
            LoadDataSourcesCommand = new RelayCommand<object>(LoadDataSources);
            AddItemToListCommand = new RelayCommand<object>(AddItemToList);
            CreateAssignmentRecordCommand = new RelayCommand<object>(CreateAssignmentRecord);
            Equipment = new ObservableCollection<EquipmentModel>();
            Departments = new ObservableCollection<DepartmentModel>();
            AuthorizedEmployees = new ObservableCollection<EmployeeModel>();
            SelectedItems = new ObservableCollection<EquipmentModel>();
            Jobsites = new ObservableCollection<JobsiteModel>();
        }
    }
}
