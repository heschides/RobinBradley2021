using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyEmployeeTracker.Models
{
public    class JobTitleModel
    {
        private int _id;

        public int Id
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

        private int _baseSalary;

        public int BaseSalary
        {
            get { return _baseSalary; }
            set { _baseSalary = value; }
        }

        public override string ToString()
        {
            return Name; 
        }

    }
}
