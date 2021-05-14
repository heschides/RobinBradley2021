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
        private PhoneModel _newPhone;
        public PhoneModel NewPhone
        {
            get { return _newPhone; }
            set { if (Set(ref _newPhone, value)) ; }
        }
        private EmailModel _newEmail;
        public EmailModel NewEmail
        {
            get { return _newEmail; }
            set { if (Set(ref _newEmail, value)) ; }
        }
        private RestrictionModel _newRestriction;
        public RestrictionModel NewRestriction
        {
            get { return _newRestriction; }
            set { if (Set(ref _newRestriction, value)) ; }
        }
        private CertificationModel _newCertification;
        public CertificationModel NewCertification
        {
            get { return _newCertification; }
            set { if (Set(ref _newCertification, value)) ; }
        }
        private CitationModel _newCitation;
        public CitationModel NewCitation
        {
            get { return _newCitation; }
            set { if (Set(ref _newCitation, value)) ; }
        }
        private DocumentModel _newDocument;
        public DocumentModel NewDocument
        {
            get { return _newDocument; }
            set { if (Set(ref _newDocument, value)) ; }
        }

        private PhoneModel _selectedPhone;
        public PhoneModel SelectedPhone
        {
            get { return _selectedPhone; }
            set { _selectedPhone = value; }
        }
        private EmailModel _emailModel;
        public EmailModel SelectedEmail
        {
            get { return _emailModel; }
            set { if (Set(ref _emailModel, value)) ; }
        }
        private RestrictionModel _selectedRestriction;
        public RestrictionModel SelectedRestriction
        {
            get { return _selectedRestriction; }
            set { if (Set(ref _selectedRestriction, value)) ; }
        }
        private CertificationModel _selectedCertification;
        public CertificationModel SelectedCertification
        {
            get { return _selectedCertification; }
            set { if (Set(ref _selectedCertification, value)) ; }
        }
        private CitationModel _selectedCitation;
        public CitationModel SelectedCitation
        {
            get { return _selectedCitation; }
            set { if (Set(ref _selectedCitation, value)) ; }
        }
        private DocumentModel _selectedDocument;
        public DocumentModel SelectedDocument
        {
            get { return _selectedDocument; }
            set { if (Set(ref _selectedDocument, value)) ; }
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

        //COMMANDS
        public RelayCommand<object> AddPhoneCommand { get; private set; }
        public RelayCommand<object> AddEmailCommand { get; private set; }
        public RelayCommand<object> AddCertificationCommand { get; private set; }
        public RelayCommand<object> AddCitationCommand { get; private set; }
        public RelayCommand<object> AddRestrictionCommand { get; private set; }
        public RelayCommand<object> AddDocumentCommand { get; private set; }
        public RelayCommand<ObservableCollection<PhoneModel>> RemovePhoneCommand { get; private set; }
        public RelayCommand<ObservableCollection<EmailModel>> RemoveEmailCommand { get; private set; }
        public RelayCommand<ObservableCollection<CertificationModel>> RemoveCertificationCommand { get; private set; }
        public RelayCommand<ObservableCollection<CitationModel>> RemoveCitationCommand { get; private set; }
        public RelayCommand<ObservableCollection<RestrictionModel>> RemoveRestrictionCommand { get; private set; }
        public RelayCommand<ObservableCollection<DocumentModel>> RemoveDocumentCommand { get; private set; }

        public RelayCommand<object> LoadComboboxesCommand { get; set; }

        //METHODS
        public void AddPhone(object e)
        {
            _selectedEmployee.Phones.Add(NewPhone);
        }
        public void AddEmail(object e)
        {
            _selectedEmployee.Emails.Add(NewEmail);
        }
        public void AddRestriction(object e)
        {
            SelectedEmployee.Restrictions.Add(_newRestriction);
        }
        public void AddCertification(object e)
        {
            _selectedEmployee.Certifications.Add(NewCertification);
        }
        public void AddCitation(object e)
        {
            var newCitation = new CitationModel()
            {
                Name = NewCitation.Name,
                Date = NewCitation.Date,
            };
            _selectedEmployee.Citations.Add(newCitation);
        }
        public void AddDocument(object e)
        {
            _selectedEmployee.Documents.Add(SelectedDocument);
        }
        public void RemovePhone(object e)
        {
            var newList = new List<PhoneModel>();
            foreach (PhoneModel phoneModel in SelectedEmployee.Phones)
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
        public void RemoveEmail(object e)
        {
            var newList = new List<EmailModel>();
            foreach (EmailModel emailModel in SelectedEmployee.Emails)
            {
                if (emailModel.Address != SelectedEmail.Address)
                {
                    newList.Add(emailModel);
                }
            }
            _selectedEmployee.Emails.Clear();
            foreach (EmailModel email in newList)
                _selectedEmployee.Emails.Add(email);
        }
        public void RemoveCertification(object e)
        {
            var newList = new List<CertificationModel>();
            foreach (CertificationModel certificationModel in SelectedEmployee.Certifications)
            {
                if (certificationModel.Id != SelectedCertification.Id)
                    newList.Add(certificationModel);
            }

        }
        public void RemoveCitation(object e)
        {
            var newList = new List<CitationModel>();
            if (SelectedEmployee.Citations != null)
            {
                foreach (CitationModel citation in SelectedEmployee.Citations)
                {
                    newList.Add(citation);
                }
                SelectedEmployee.Citations.Clear();
                foreach (CitationModel citation in newList)
                {
                    SelectedEmployee.Citations.Add(citation);
                }
            }
        }
        public void RemoveRestriction(object e)
        {
            var newList = new List<RestrictionModel>();
            if (SelectedEmployee.Restrictions != null)
            {
                foreach (RestrictionModel restriction in SelectedEmployee.Restrictions)
                {
                    newList.Add(restriction);
                }
                SelectedEmployee.Restrictions.Clear();
                foreach (RestrictionModel restriction in newList)
                {
                    SelectedEmployee.Restrictions.Add(restriction);
                }
            }
        }
        public void RemoveDocument(object e)
        {
            var newList = new List<DocumentModel>();
            if (SelectedEmployee.Documents != null)
            {
                foreach (DocumentModel document in SelectedEmployee.Documents)
                {
                    newList.Add(document);
                }
                SelectedEmployee.Documents.Clear();
                foreach (DocumentModel document in newList)
                {
                    SelectedEmployee.Documents.Add(document);
                }
            }
        }
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
            LoadComboboxesCommand = new RelayCommand<object>(LoadComboboxes);
            EmployeeStatuses = new ObservableCollection<EmployeeStatusModel>();
            Messenger.Default.Register<EmployeeToken>(this, OnNewEmployeeToken);

            Departments = new ObservableCollection<DepartmentModel>();
            JobTitles = new ObservableCollection<JobTitleModel>();
            NewCitation = new CitationModel();
            
            AddPhoneCommand = new RelayCommand<object>(AddPhone);
            AddEmailCommand = new RelayCommand<object>(AddEmail);
            AddCertificationCommand = new RelayCommand<object>(AddCertification);
            AddCitationCommand = new RelayCommand<object>(AddCitation);
            AddRestrictionCommand = new RelayCommand<object>(AddRestriction);
            AddDocumentCommand = new RelayCommand<object>(AddDocument);
            RemovePhoneCommand = new RelayCommand<ObservableCollection<PhoneModel>>(RemovePhone);
            RemoveEmailCommand = new RelayCommand<ObservableCollection<EmailModel>>(RemoveEmail);
            RemoveCertificationCommand = new RelayCommand<ObservableCollection<CertificationModel>>(RemoveCertification);
            RemoveCitationCommand = new RelayCommand<ObservableCollection<CitationModel>>(RemoveCitation);
            RemoveRestrictionCommand = new RelayCommand<ObservableCollection<RestrictionModel>>(RemoveRestriction);
            RemoveDocumentCommand = new RelayCommand<ObservableCollection<DocumentModel>>(RemoveDocument);
        }
    }
}
