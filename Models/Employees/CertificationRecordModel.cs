using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobinBradley2021.Models.Employees
{
   public class CertificationRecordModel
    {
        public int Id { get; set; }
        public CertificationClassModel Class { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
