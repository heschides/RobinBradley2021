using System;
using System.Collections.ObjectModel;

namespace RobinBradley2021.Models.Tokens
{
    public class EmployeeToken
    {
        public EmployeeModel SelectedEmployee { get; private set; }
        public EmployeeToken(EmployeeModel employee)
        {
            SelectedEmployee = employee;
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
        public JobTitleModel JobTitle { get; set; }
        public EmployeeStatusModel Status { get; set; }
        public DepartmentModel Department { get; set; }
        public DateTime HireDate { get; set; }
        public ObservableCollection<EmailModel> Emails { get; set; }
        public ObservableCollection<PhoneModel> Phones { get; set; }
        public ObservableCollection<CertificationModel> Certifications { get; set; }
        public ObservableCollection<CitationModel> Citations { get; set; }
        public ObservableCollection<EquipmentAssignmentRecordModel> EquipmentAssignments { get; set; }
        public ObservableCollection<RestrictionModel> Restrictions { get; set; }
        public ObservableCollection<DocumentModel> Documents { get; set; }
    }
}
