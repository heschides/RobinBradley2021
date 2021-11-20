using FluentValidation.Results;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using RobinBradley2021.DataAccess;
using RobinBradley2021.DataValidation;
using RobinBradley2021.Models;
using RobinBradley2021.Models.Employees;
using RobinBradley2021.Models.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows;
using static RobinBradley2021.Functions.ReferenceAction;
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
            set
            {
                Set(ref _selectedEmployee, value);
                //Passes SelectedEmployee properties to ComboBoxes for reference equality.  Necessary for binding to work.
                ReplaceReferences(EmployeeStatuses, SelectedEmployee);
                ReplaceReferences(JobTitles, SelectedEmployee);
                ReplaceReferences(Departments, SelectedEmployee);
            }
        }
        private PhoneModel _newPhone;
        public PhoneModel NewPhone
        {
            get { return _newPhone; }
            set { Set(ref _newPhone, value); }
        }
        private string _newPhoneNumber;
        public string NewPhoneNumber
        {
            get { return _newPhoneNumber; }
            set { Set(ref _newPhoneNumber, value); }
        }
        private PhoneType _newPhoneType;
        public PhoneType NewPhoneType
        {
            get { return _newPhoneType; }
            set { Set(ref _newPhoneType, value); }
        }
        private EmailModel _newEmail;
        public EmailModel NewEmail
        {
            get { return _newEmail; }
            set { Set(ref _newEmail, value); }
        }
        private string _newEmailAddress;
        public string NewEmailAddress
        {
            get { return _newEmailAddress; }
            set { Set(ref _newEmailAddress, value); }
        }
        private EmailType _newEmailType;
        public EmailType NewEmailType
        {
            get { return _newEmailType; }
            set { Set(ref _newEmailType, value); }
        }
        private RestrictionModel _newRestriction;
        public RestrictionModel NewRestriction
        {
            get { return _newRestriction; }
            set { Set(ref _newRestriction, value); }
        }
        private CertificationRecordModel _newCertification;
        public CertificationRecordModel NewCertification
        {
            get { return _newCertification; }
            set { Set(ref _newCertification, value); }
        }
        private CitationModel _newCitation;
        public CitationModel NewCitation
        {
            get { return _newCitation; }
            set { Set(ref _newCitation, value); }
        }
        private DocumentModel _newDocument;
        public DocumentModel NewDocument
        {
            get { return _newDocument; }
            set { Set(ref _newDocument, value); }
        }
        private Image _newImage;
        public Image NewImage
        {
            get { return _newImage; }
            set { Set(ref _newImage, value); }
        }
        private string _displayedImage;
        public string DisplayedImage
        {
            get { return _displayedImage; }
            set { Set(ref _displayedImage, value); }
        }
        private PhoneModel _selectedPhone;
        public PhoneModel SelectedPhone
        {
            get { return _selectedPhone; }
            set { Set(ref _selectedPhone, value); }
        }
        private EmailModel _emailModel;
        public EmailModel SelectedEmail
        {
            get { return _emailModel; }
            set { Set(ref _emailModel, value); }
        }
        private RestrictionModel _selectedRestriction;
        public RestrictionModel SelectedRestriction
        {
            get { return _selectedRestriction; }
            set { Set(ref _selectedRestriction, value); }
        }
        private CertificationRecordModel _selectedCertification;
        public CertificationRecordModel SelectedCertification
        {
            get { return _selectedCertification; }
            set { Set(ref _selectedCertification, value); }
        }
        private CitationModel _selectedCitation;
        public CitationModel SelectedCitation
        {
            get { return _selectedCitation; }
            set { Set(ref _selectedCitation, value); }
        }
        private DocumentModel _selectedDocument;
        public DocumentModel SelectedDocument
        {
            get { return _selectedDocument; }
            set { Set(ref _selectedDocument, value); }
        }
        public ObservableCollection<DocumentModel> Documents { get; private set; }
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
        public IEnumerable<EmailType> EmailTypeValues
        {
            get
            {
                return Enum.GetValues(typeof(EmailType)).Cast<EmailType>();
            }
        }
        public ObservableCollection<RestrictionModel> Restrictions { get; private set; }
        public ObservableCollection<CertificationRecordModel> Certifications { get; private set; }
        public ObservableCollection<CitationModel> Citations { get; private set; }
        public ObservableCollection<string> ValidationErrors { get; private set; }
        //COMMANDS
        public RelayCommand<object> AddPhoneCommand { get; private set; }
        public RelayCommand<object> AddEmailCommand { get; private set; }
        public RelayCommand<object> AddCertificationCommand { get; private set; }
        public RelayCommand<object> AddCitationCommand { get; private set; }
        public RelayCommand<object> AddRestrictionCommand { get; private set; }
        public RelayCommand<object> AddDocumentCommand { get; private set; }
        public RelayCommand<object> ValidateNewEmployeeCommand { get; private set; }
        public RelayCommand<object> SelectNewImageCommand { get; private set; }
        public TokenCommand<EmployeeToken> LoadInitialDataCommand { get; private set; }
        public RelayCommand<ObservableCollection<PhoneModel>> RemovePhoneCommand { get; private set; }
        public RelayCommand<ObservableCollection<EmailModel>> RemoveEmailCommand { get; private set; }
        public RelayCommand<ObservableCollection<CertificationRecordModel>> RemoveCertificationCommand { get; private set; }
        public RelayCommand<ObservableCollection<CitationModel>> RemoveCitationCommand { get; private set; }
        public RelayCommand<ObservableCollection<RestrictionModel>> RemoveRestrictionCommand { get; private set; }
        public RelayCommand<ObservableCollection<DocumentModel>> RemoveDocumentCommand { get; private set; }
        //METHODS
        public void AddPhone(object e)
        {
            var newPhone = new PhoneModel();
            newPhone.Number = NewPhoneNumber;
            newPhone.Type = NewPhoneType;
            _selectedEmployee.Phones.Add(newPhone);
            NewPhoneNumber = string.Empty;
            NewPhoneType = default;
        }
        public void AddEmail(object e)
        {
            var newEmail = new EmailModel();
            newEmail.Address = NewEmailAddress;
            newEmail.Type = NewEmailType;
            _selectedEmployee.Emails.Add(newEmail);
            NewEmailAddress = string.Empty;
            NewEmailType = default;
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
            var newFileDialog = new OpenFileDialog();
            if (newFileDialog.ShowDialog() == true)
            {
                var selectedFileSafeName = newFileDialog.SafeFileName;
                var selectedFileName = newFileDialog.FileName;
                var newdoc = new DocumentModel();
                newdoc.SafeName = selectedFileSafeName;
                newdoc.Name = selectedFileName;
                _selectedEmployee.Documents.Add(newdoc);
            }
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
            var newList = new List<CertificationRecordModel>();
            foreach (CertificationRecordModel CertificationRecordModel in SelectedEmployee.Certifications)
            {
                if (CertificationRecordModel.Id != SelectedCertification.Id)
                    newList.Add(CertificationRecordModel);
            }
            SelectedEmployee.Certifications.Clear();
            foreach (CertificationRecordModel certification in newList)
            {
                SelectedEmployee.Certifications.Add(certification);
            }
        }
        public void RemoveCitation(object e)
        {
            var newList = new List<CitationModel>();
            if (SelectedEmployee.Citations != null)
            {
                foreach (CitationModel citation in SelectedEmployee.Citations.Where(x => x.Id != SelectedCitation.Id))
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
                for (int i = SelectedEmployee.Documents.Count - 1; i >= 0; i--)
                {
                    var document = SelectedEmployee.Documents[i];
                    if (SelectedDocument != null && SelectedDocument == document)
                        SelectedEmployee.Documents.Remove(document);
                }
            }
        }
        public void SelectImage(object e)
        {
            var selectImageDialog = new OpenFileDialog() { Filter = "Image Files(*.bmp; *.png; *.jpg)|*.bmp; *.png; *.jpg" };
            if (selectImageDialog.ShowDialog() == true)
            {
                Image image = Image.FromFile(selectImageDialog.FileName.ToString());
                if (image.Width != 250 || image.Height != 200)
                {
                    var errorMessage = "Please select an image with dimensions 250x200";
                    MessageBox.Show(errorMessage);
                }
                else
                {
                    DisplayedImage = selectImageDialog.FileName.ToString();
                    NewImage = image;
                    var profilePhoto = new DocumentModel();
                    profilePhoto.Name = DisplayedImage;
                    SelectedEmployee.ProfilePhoto = profilePhoto;
                }
            }
        }
        public async void LoadComboboxes(object e)
        {
            var departments = await AdministrationRepository.DepartmentQueryAsync();
            var jobTitles = await AdministrationRepository.JobTitleQueryAsync();
            var employeeStatuses = await AdministrationRepository.EmployeeStatusQueryAsync();
            var certifications = await AdministrationRepository.CertificationQueryAsync();
            var citations = await AdministrationRepository.CitationQueryAsync();
            var restrictions = await AdministrationRepository.RestrictionQueryAsync();
            foreach (CertificationRecordModel certification in certifications)
            {
                Certifications.Add(certification);
            }
            foreach (CitationModel citation in citations)
            {
                Citations.Add(citation);
            }
            foreach (RestrictionModel restriction in restrictions)
            {
                Restrictions.Add(restriction);
            }
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
        public void ValidateNewEmployee(object e)
        {
            var employeeValidator = new EmployeeValidation();
            if (SelectedEmployee != null)
            {
                var results = employeeValidator.Validate(SelectedEmployee);
                if (results.IsValid == false)
                {
                    foreach (ValidationFailure failure in results.Errors)
                    {
                        ValidationErrors.Add($"{failure.PropertyName }: {failure.ErrorMessage}");
                    }
                    MessageBox.Show("Mistakes were made.");
                }
            }
        }
        private void OnNewEmployeeToken(EmployeeToken token)
        {
            if (token != null)
            {
                SelectedEmployee = token.SelectedEmployee;
            }
        }
        //CONSTRUCTOR
        public EditEmployeeWindowViewModel()
        {
            var now = DateTime.Now;
            var oneYearAhead = now.AddDays(365);
            SelectedEmployee = new EmployeeModel();
            Messenger.Default.Register<EmployeeToken>(this, OnNewEmployeeToken);
            LoadInitialDataCommand = new TokenCommand<EmployeeToken>(OnNewEmployeeToken, LoadComboboxes);
            EmployeeStatuses = new ObservableCollection<EmployeeStatusModel>();
            ValidationErrors = new ObservableCollection<string>();
            Restrictions = new ObservableCollection<RestrictionModel>();
            Citations = new ObservableCollection<CitationModel>();
            Certifications = new ObservableCollection<CertificationRecordModel>();
            Departments = new ObservableCollection<DepartmentModel>();
            Documents = new ObservableCollection<DocumentModel>();
            JobTitles = new ObservableCollection<JobTitleModel>();
            NewCitation = new CitationModel() { Date = DateTime.Now };
            NewPhone = new PhoneModel();
            NewEmail = new EmailModel();
            NewDocument = new DocumentModel();
            NewImage = Image.FromFile(@"C:\Users\jwhit\source\repos\Robin-Bradley2021\Images\defaultUserImage.jpg");
            DisplayedImage = @"C:\Users\jwhit\source\repos\Robin-Bradley2021\Images\defaultUserImage.jpg";
            NewRestriction = new RestrictionModel() { BeginDate = DateTime.Now, EndDate = oneYearAhead };
            NewCertification = new CertificationRecordModel() { DateStart = DateTime.Now, DateEnd = oneYearAhead };
            AddPhoneCommand = new RelayCommand<object>(AddPhone);
            AddEmailCommand = new RelayCommand<object>(AddEmail);
            AddCertificationCommand = new RelayCommand<object>(AddCertification);
            AddCitationCommand = new RelayCommand<object>(AddCitation);
            AddRestrictionCommand = new RelayCommand<object>(AddRestriction);
            AddDocumentCommand = new RelayCommand<object>(AddDocument);
            SelectNewImageCommand = new RelayCommand<object>(SelectImage);
            RemovePhoneCommand = new RelayCommand<ObservableCollection<PhoneModel>>(RemovePhone);
            RemoveEmailCommand = new RelayCommand<ObservableCollection<EmailModel>>(RemoveEmail);
            RemoveCertificationCommand = new RelayCommand<ObservableCollection<CertificationRecordModel>>(RemoveCertification);
            RemoveCitationCommand = new RelayCommand<ObservableCollection<CitationModel>>(RemoveCitation);
            RemoveRestrictionCommand = new RelayCommand<ObservableCollection<RestrictionModel>>(RemoveRestriction);
            RemoveDocumentCommand = new RelayCommand<ObservableCollection<DocumentModel>>(RemoveDocument);
            ValidateNewEmployeeCommand = new RelayCommand<object>(ValidateNewEmployee);
        }
    }
}
