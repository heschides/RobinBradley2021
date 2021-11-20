using RobinBradley2021.Models.Employees;
using RobinBradley2021.Models.Tokens;
using System;
using System.Collections.ObjectModel;
using System.Drawing;

namespace RobinBradley2021.Models
{
    public class EmployeeModel
    {
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
        public DocumentModel ProfilePhoto { get; set; }
        public static explicit operator EmployeeModel(EmployeeToken v)
        {
            var employee = new EmployeeModel();
            employee.FirstName = v.FirstName;
            employee.LastName = v.LastName;
            return employee;
        }
        public ObservableCollection<EmailModel> Emails { get; set; }
        public ObservableCollection<PhoneModel> Phones { get; set; }
        public ObservableCollection<CertificationRecordModel> Certifications { get; set; }
        public ObservableCollection<CitationModel> Citations { get; set; }
        public ObservableCollection<EquipmentAssignmentRecordModel> EquipmentAssignments { get; set; }
        public ObservableCollection<VehicleAssignmentRecordModel> VehicleAssignments { get; set; }
        public ObservableCollection<RestrictionModel> Restrictions { get; set; }
        public ObservableCollection<DocumentModel> Documents { get; set; }
    }
}

