using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobinBradley2021.Models
{
public    class JobTitleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal BaseSalary { get; set; }

        public override string ToString()
        {
            return Name; 
        }

    }
}
