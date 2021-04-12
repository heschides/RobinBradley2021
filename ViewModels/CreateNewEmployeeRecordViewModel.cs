using SimplyEmployeeTracker.DataAccess;
using static SimplyEmployeeTracker.DataAccess.SendData;
using SimplyEmployeeTracker.Models;
using SimplyEmployeeTracker.Other;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyEmployeeTracker.ViewModels
{
public  class CreateNewEmployeeRecordViewModel : ViewModelBase
    {
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        private string _nickname;
        public string Nickname
        {
            get { return _nickname; }
            set { _nickname = value; }
        }

        private DateTime dateTime;
        public DateTime HireDate
        {
            get { return dateTime; }
            set { dateTime = value; }
        }

        
        public static ObservableCollection<JobTitleModel> JobTitles { get; private set; }
        
        private JobTitleModel _selectedJobTitle;
        public JobTitleModel SelectedJobTitle
        {
            get { return _selectedJobTitle; }
            set { _selectedJobTitle = value; }
        }

        public static ObservableCollection<DepartmentModel> Departments { get; private set; }
        private DepartmentModel _selectedDepartment;
        public DepartmentModel SelectedDepartment
        {
            get { return _selectedDepartment; }
            set { _selectedDepartment = value; }
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
            set { OnPropertyChanged(ref _newPhoneNumber, value); }
        }

        private string _newPhoneType;
        public string NewPhoneType
        {
            get { return _newPhoneType; }
            set { OnPropertyChanged(ref _newPhoneType, value); }
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
            set { OnPropertyChanged(ref _newEmailAddress, value); }
        }

        private string _newEmailType;
        public string NewEmailType
        {
            get { return _newEmailType; }
            set { OnPropertyChanged(ref _newEmailType, value); }
        }

        public static ObservableCollection<CertificationModel> Certifications { get; private set; }
        public static ObservableCollection<CertificationModel> CertificationTypes {get; private set;}
        
        private string _newCertificationName;
        public string NewCertificationName
        {
            get { return _newCertificationName; }
            set { OnPropertyChanged(ref _newCertificationName, value); }
        }

        private DateTime _newCertificationExpirationDate;
        public DateTime NewCertificationExpirationDate
        {
            get { return _newCertificationExpirationDate; }
            set { OnPropertyChanged(ref _newCertificationExpirationDate, value); }
        }

        public static ObservableCollection<RestrictionModel> Restrictions { get; private set; }
        public static ObservableCollection<RestrictionModel> RestrictionTypes { get; private set; }

        private string _newRestrictionName;

        public string NewRestrictionType
        {
            get { return _newRestrictionName; }
            set { OnPropertyChanged(ref _newRestrictionName, value); }
        }
        private DateTime _newRestrictionEndDate;

        public DateTime NewRestrictionEndDate
        {
            get { return _newRestrictionEndDate; }
            set { OnPropertyChanged(ref _newRestrictionEndDate, value); }
        }


        //COMMANDS

        public RelayCommand AddPhoneCommand { get; private set; }
        public void AddEmployeeForm_AddPhone(object e)
        {
            PhoneModel newPhone = new PhoneModel();
            newPhone.Number = _newPhoneNumber;
            newPhone.Type = _newPhoneType;
            Phones.Add(newPhone);
        }

        public RelayCommand AddEmailCommand { get; private set; }
        public void AddEmployeeForm_AddEmail(object e)
        {
            var newEmail = new EmailModel();
            newEmail.Address = _newEmailAddress;
            newEmail.Type = _newEmailType;
            Emails.Add(newEmail);
        }

        public RelayCommand AddCertificationCommand { get; private set; }
        public void AddEmployeeForm_AddCertification(object e)
        {
            var newCertification = new CertificationModel();
            newCertification.Name = _newCertificationName;
            newCertification.ExpirationDate = _newCertificationExpirationDate;
            Certifications.Add(newCertification);
        }

        public RelayCommand AddRestrictionCommand { get; private set; }
        public void AddEmployeeForm_AddRestriction(object e)
        {
            var newRestriction = new RestrictionModel();
            newRestriction.Name = _newRestrictionName;
            newRestriction.EndDate = _newRestrictionEndDate;
            Restrictions.Add(newRestriction);
        }

        public RelayCommand CreateNewEmployeeCommand { get; private set; }
        public void CreateNewEmployee(object e)
        {
            var newEmployee = new EmployeeModel();
            newEmployee.FirstName = FirstName;
            newEmployee.LastName = LastName;
            newEmployee.NickName = Nickname;
            newEmployee.HireDate = HireDate;
            newEmployee.Emails = Emails;
            newEmployee.Phones = Phones;
            newEmployee.Certifications = Certifications;
            newEmployee.Restrictions = Restrictions;
            newEmployee.Department = _selectedDepartment;
            newEmployee.JobTitle = _selectedJobTitle;
            EmployeeStatusModel newEmployeeStatus = new EmployeeStatusModel();
            newEmployeeStatus.ID = 1;
            newEmployee.Status = newEmployeeStatus;
            SendData.CreateEmployee(newEmployee);
       //     EmployeeViewModel = await GetData.EmployeeQueryAsync();
        }

//CONSTRUCTORS
        public CreateNewEmployeeRecordViewModel()
        {
            AddPhoneCommand = new RelayCommand(AddEmployeeForm_AddPhone);
            AddEmailCommand = new RelayCommand(AddEmployeeForm_AddEmail);
            AddCertificationCommand = new RelayCommand(AddEmployeeForm_AddCertification);
            AddRestrictionCommand = new RelayCommand(AddEmployeeForm_AddRestriction);
            Phones = new ObservableCollection<PhoneModel>();
            Emails = new ObservableCollection<EmailModel>();
            Certifications = new ObservableCollection<CertificationModel>();
            Restrictions = new ObservableCollection<RestrictionModel>();
            CreateNewEmployeeCommand = new RelayCommand(CreateNewEmployee);
        }

        public static async Task<CreateNewEmployeeRecordViewModel>CreateNewEmployeeRecordViewModelAsync()
        {
            var createNewEmployeeRecordViewModel = new CreateNewEmployeeRecordViewModel();
            CertificationTypes = await GetData.CertificationQueryAsync();
            RestrictionTypes = await GetData.RestrictionQueryAsync();
            Departments = await GetData.DepartmentQueryAsync();
            JobTitles = await GetData.JobTitleQueryAsync();          
            return createNewEmployeeRecordViewModel;
        }
    }
}
