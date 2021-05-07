using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobinBradley2021.Models
{
 public   class PhoneModel
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _number;

        public string Number
        {
            get { return _number; }
            set { _number = value; }
        }
        private string _type;

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public override string ToString()
        {
            return Number + " : " + Type;
        }

    }
}
