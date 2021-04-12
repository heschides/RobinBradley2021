using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyEmployeeTracker.Models
{
public    class CertificationModel
    {
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        private DateTime _beginDate;

        public DateTime BeginDate
        {
            get { return _beginDate; }
            set { _beginDate = value; }
        }
        private DateTime _expirationDate;

        public DateTime ExpirationDate
        {
            get { return _expirationDate; }
            set { _expirationDate = value; }
        }
    }
}
