using GalaSoft.MvvmLight;
using RobinBradley2021.DataAccess;
using RobinBradley2021.Models;
using RobinBradley2021.Other;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace RobinBradley2021.ViewModels
{
public  class NewEmployeeRecordWindowViewModel : ViewModelBase
    {
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { Set(ref _firstName, value); }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { Set(ref _lastName, value); }
        }

        private string _nickname;
        public string Nickname
        {
            get { return _nickname; }
            set { Set(ref _nickname, value); }
        }

        private DateTime _hireDate;
        public DateTime HireDate
        {
            get { return _hireDate; }
            set { Set(ref _hireDate, value); }
        }

        public static ObservableCollection<JobTitleModel> JobTitles { get; private set; }
        private JobTitleModel _selectedJobTitle;
        public JobTitleModel SelectedJobTitle
        {
            get { return _selectedJobTitle; }
            set { Set(ref _selectedJobTitle, value); }
        }

        public static ObservableCollection<DepartmentModel> Departments { get; private set; }
        private DepartmentModel _selectedDepartment;
        public DepartmentModel SelectedDepartment
        {
            get { return _selectedDepartment; }
            set { Set(ref _selectedDepartment, value); }
        }

        public ObservableCollection<PhoneModel> Phones { get; private set; }
        public static List<string> PhoneTypes
        {
            get 
            {
                return new List<string>() { "Home", "Work", "Cell" };
            }
        }
        private string _newPhoneNumber;
        public string NewPhoneNumber
        {
            get { return _newPhoneNumber; }
            set { Set(ref _newPhoneNumber, value); }
        }

        private string _newPhoneType;
        public string NewPhoneType
        {
            get { return _newPhoneType; }
            set { Set(ref _newPhoneType, value); }
        }

        
        public ObservableCollection<EmailModel> Emails { get; private set; }
        public List<string> EmailTypes
        {
            get { return new List<string>() { "Personal", "Work" }; }
        }
        private string _newEmailAddress;

        public string NewEmailAddress
        {
            get { return _newEmailAddress; }
            set { Set(ref _newEmailAddress, value); }
        }

        private string _newEmailType;
        public string NewEmailType
        {
            get { return _newEmailType; }
            set { Set(ref _newEmailType, value); }
        }

        public static ObservableCollection<CertificationModel> Certifications { get; private set; }
        public static ObservableCollection<CertificationModel> CertificationTypes {get; private set;}
        
        private CertificationModel _selectedCertification;
        public CertificationModel SelectedCertification
        {
            get { return _selectedCertification; }
            set { Set(ref _selectedCertification, value); }
        }

        private DateTime _newCertificationExpirationDate;
        public DateTime NewCertificationExpirationDate
        {
            get { return _newCertificationExpirationDate; }
            set { _newCertificationExpirationDate = value; }
        }


        public static ObservableCollection<RestrictionModel> Restrictions { get; private set; }
        public static ObservableCollection<RestrictionModel> RestrictionTypes { get; private set; }

        private RestrictionModel _selectedRestriction;
        public RestrictionModel SelectedRestriction
        {
            get { return _selectedRestriction; }
            set { Set(ref _selectedRestriction, value); }
        }

        private DateTime _newRestrictionEndDate;
        public DateTime NewRestrictionEndDate
        {
            get { return _newRestrictionEndDate; }
            set { _newRestrictionEndDate = value; }
        }


        //COMMANDS

        public RelayCommand<object> AddPhoneCommand { get; private set; }
        public void AddEmployeeForm_AddPhone(object e)
        {
            PhoneModel newPhone = new PhoneModel();
            newPhone.Number = _newPhoneNumber;
            newPhone.Type = _newPhoneType;
            Phones.Add(newPhone);
        }

        public RelayCommand<object> AddEmailCommand { get; private set; }
        public void AddEmployeeForm_AddEmail(object e)
        {
            var newEmail = new EmailModel();
            newEmail.Address = _newEmailAddress;
            newEmail.Type = _newEmailType;
            Emails.Add(newEmail);
        }

        public RelayCommand<object> AddCertificationCommand { get; private set; }
        public void AddEmployeeForm_AddCertification(object e)
        {
            var newCertification = new CertificationModel();
            newCertification.Name = SelectedCertification.Name;
                newCertification.ExpirationDate = _newCertificationExpirationDate;
            newCertification.Id = SelectedCertification.Id;
            Certifications.Add(newCertification);
        }

        public RelayCommand<object> AddRestrictionCommand { get; private set; }
        public void AddEmployeeForm_AddRestriction(object e)
        {
            DateTime today = DateTime.Today;
            var newRestriction = new RestrictionModel();
            newRestriction.Name =  _selectedRestriction.Name;
            newRestriction.EndDate = _newRestrictionEndDate;
            newRestriction.Id = _selectedRestriction.Id ;
            newRestriction.BeginDate = today;
            Restrictions.Add(newRestriction);
        }

        public RelayCommand<object> CreateNewEmployeeCommand { get; private set; }
        public void CreateNewEmployee(object e)
        {
            var newEmployee = new EmployeeModel();
            newEmployee.FirstName = FirstName;
            newEmployee.LastName = LastName;
            newEmployee.Nickname = Nickname;
            newEmployee.HireDate = HireDate;
            newEmployee.Emails = Emails;
            newEmployee.Phones = Phones;
            newEmployee.Certifications = Certifications;
            newEmployee.Restrictions = Restrictions;
            newEmployee.Department = _selectedDepartment;
            newEmployee.JobTitle = _selectedJobTitle;
            EmployeeStatusModel newEmployeeStatus = new EmployeeStatusModel();
            newEmployeeStatus.Id = 1;
            newEmployee.Status = newEmployeeStatus;
            SendData.CreateEmployee(newEmployee);
        }

        public RelayCommand<object> LoadComboBoxesCommand { get; private set; }
        public async void LoadComboBoxes(object e)
        {
            var certificationTypes = await GetData.CertificationQueryAsync();
            foreach (CertificationModel certification in certificationTypes) { CertificationTypes.Add(certification); }
            var restrictionTypes = await GetData.RestrictionQueryAsync();
            foreach (RestrictionModel restrictionType in restrictionTypes) { RestrictionTypes.Add(restrictionType); }
            var departments = await GetData.DepartmentQueryAsync();
            foreach (DepartmentModel department in departments) { Departments.Add(department); }
            var jobTitles = await GetData.JobTitleQueryAsync();
            foreach(JobTitleModel jobTitle in jobTitles) { JobTitles.Add(jobTitle); }
        }


        //CONSTRUCTORS
        public NewEmployeeRecordWindowViewModel()
        {
            Phones = new ObservableCollection<PhoneModel>();
            Emails = new ObservableCollection<EmailModel>();
            Certifications = new ObservableCollection<CertificationModel>();
            CertificationTypes = new ObservableCollection<CertificationModel>();
            Restrictions = new ObservableCollection<RestrictionModel>();
            RestrictionTypes = new ObservableCollection<RestrictionModel>();
            JobTitles = new ObservableCollection<JobTitleModel>();
            Departments = new ObservableCollection<DepartmentModel>();
            AddPhoneCommand = new RelayCommand<object>(AddEmployeeForm_AddPhone);
            AddEmailCommand = new RelayCommand<object>(AddEmployeeForm_AddEmail);
            AddCertificationCommand = new RelayCommand<object>(AddEmployeeForm_AddCertification);
            AddRestrictionCommand = new RelayCommand<object>(AddEmployeeForm_AddRestriction);
            CreateNewEmployeeCommand = new RelayCommand<object>(CreateNewEmployee);
            LoadComboBoxesCommand = new RelayCommand<object>(LoadComboBoxes);

        }

        //public static async Task<NewEmployeeRecordWindowViewModel>CreateNewEmployeeRecordViewModelAsync()
        //{
        //    var createNewEmployeeRecordViewModel = new NewEmployeeRecordWindowViewModel();

        //    return createNewEmployeeRecordViewModel;
        //}
    }
}
