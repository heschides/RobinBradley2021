using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyEmployeeTracker.Models
{
    public class EmployeeModel
    {
        public int ID { get; set; }
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
    }
}

