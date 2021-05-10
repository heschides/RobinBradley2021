using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using RobinBradley2021.DataAccess;
using RobinBradley2021.Models;
using RobinBradley2021.Models.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using static RobinBradley2021.Models.Enums;
namespace RobinBradley2021.ViewModels
{
    public class EditEmployeeWindowViewModel : ViewModelBase
    {
        //PROPERTIES
        private EmployeeModel _selectedEmployee;
        public EmployeeModel SelectedEmployee
        {
            get { return _selectedEmployee; }
            set { Set(ref _selectedEmployee, value); }
        }
        private string _newPhoneNumber;
        public string NewPhoneNumber
        {
            get { return _newPhoneNumber; }
            set { Set(ref _newPhoneNumber, value); }
        }
        public PhoneType NewPhoneType { get; set; }
        private PhoneModel _selectedPhone;
        public PhoneModel SelectedPhone
        {
            get { return _selectedPhone; }
            set { _selectedPhone = value; }
        }
        public ObservableCollection<DepartmentModel> Departments { get; private set; }
        public ObservableCollection<JobTitleModel> JobTitles { get; private set; }
        public ObservableCollection<EmployeeStatusModel> EmployeeStatuses { get; private set; }
        public IEnumerable<PhoneType> PhoneTypeValues
        {
            get
            {
                return Enum.GetValues(typeof(PhoneType)).Cast<PhoneType>();
            }
        }

        //COMMANDS AND METHODS
        public RelayCommand<object> AddPhoneCommand { get; private set; }
        public void AddPhone(object e)
        {
            var newPhone = new PhoneModel
            {
                Number = NewPhoneNumber
            };
            _selectedEmployee.Phones.Add(newPhone);
        }
        public RelayCommand<ObservableCollection<PhoneModel>> RemovePhoneCommand { get; private set; }
        public void RemovePhone(object e)
        {
            var newList = new ObservableCollection<PhoneModel>();
            foreach (PhoneModel phoneModel in _selectedEmployee.Phones)
            {
                if (phoneModel.Number != SelectedPhone.Number)
                {
                    newList.Add(phoneModel);
                }
            }
            _selectedEmployee.Phones.Clear();
            foreach (PhoneModel phone in newList)
            {
                _selectedEmployee.Phones.Add(phone);
            }
        }
        public RelayCommand<object> LoadComboboxesCommand { get; set; }
        public async void LoadComboboxes(object e)
        {
            var departments = await GetData.DepartmentQueryAsync();
            var jobTitles = await GetData.JobTitleQueryAsync();
            var employeeStatuses = await GetData.EmployeeStatusQueryAsync();
            foreach (DepartmentModel department in departments)
            {
                Departments.Add(department);
            }
            foreach (JobTitleModel jobTitle in jobTitles)
            {
                JobTitles.Add(jobTitle);
            }
            foreach (EmployeeStatusModel employeeStatus in employeeStatuses)
            {
                EmployeeStatuses.Add(employeeStatus);
            }
        }
        private void OnNewEmployeeToken(EmployeeToken token)
        {
            SelectedEmployee = token.SelectedEmployee;
        }
        //CONSTRUCTOR
        public EditEmployeeWindowViewModel()
        {
            Messenger.Default.Register<EmployeeToken>(this, OnNewEmployeeToken);
            LoadComboboxesCommand = new RelayCommand<object>(LoadComboboxes);
            Departments = new ObservableCollection<DepartmentModel>();
            JobTitles = new ObservableCollection<JobTitleModel>();
            EmployeeStatuses = new ObservableCollection<EmployeeStatusModel>();
            AddPhoneCommand = new RelayCommand<object>(AddPhone);
            RemovePhoneCommand = new RelayCommand<ObservableCollection<PhoneModel>>(RemovePhone);
        }
    }
}
