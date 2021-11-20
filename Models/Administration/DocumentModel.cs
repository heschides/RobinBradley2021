using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobinBradley2021.Models
{
   public class DocumentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SafeName { get; set; }
        public override string ToString()
        {
            return SafeName;
        }

    }
}
