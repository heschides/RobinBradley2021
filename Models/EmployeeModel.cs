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
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

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

        private string _nickName;

        public string NickName
        {
            get { return _nickName; }
            set { _nickName = value; }
        }
        private JobTitleModel _jobTitle;

        public JobTitleModel JobTitle
        {
            get { return _jobTitle; }
            set { _jobTitle = value; }
        }

        private EmployeeStatusModel _status;

        public EmployeeStatusModel Status
        {
            get { return _status; }
            set { _status = value; }
        }

        private DepartmentModel _department;

        public DepartmentModel Department
        {
            get { return _department; }
            set { _department = value; }
        }
        private DateTime _hireDate;

        public DateTime HireDate
        {
            get { return _hireDate; }
            set { _hireDate = value; }
        }

        private ObservableCollection<EmailModel> _emails;

        public ObservableCollection<EmailModel> Emails
        {
            get { return _emails; }
            set { _emails = value; }
        }

        private ObservableCollection<PhoneModel> _phones;

        public ObservableCollection<PhoneModel> Phones
        {
            get { return _phones; }
            set { _phones = value; }
        }

        private ObservableCollection<CertificationModel> _certifications;

        public ObservableCollection<CertificationModel> Certifications
        {
            get { return _certifications; }
            set { _certifications = value; }
        }

        private ObservableCollection<CitationModel> _citations;
        
        public ObservableCollection<CitationModel> Citations
        {
            get { return _citations; }
            set { _citations = value; }
        }
        private ObservableCollection<EquipmentAssignmentRecordModel> _equipmentAssignments;

        public ObservableCollection<EquipmentAssignmentRecordModel> EquipmentAssignments
        {
            get { return _equipmentAssignments; }
            set { _equipmentAssignments = value; }
        }

        private ObservableCollection<RestrictionModel> _restrictions;

        public ObservableCollection<RestrictionModel> Restrictions
        {
            get { return _restrictions; }
            set { _restrictions = value; }
        }

    }
}
