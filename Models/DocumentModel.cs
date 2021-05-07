using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobinBradley2021.Models
{
   public class DocumentModel
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

        private string _safeName;
        public string SafeName
        {
            get { return _safeName; }
            set { _safeName = value; }
        }

    }
}
