using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RobinBradley2021.Models.Enums;

namespace RobinBradley2021.Models
{
    public class PhoneModel
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public PhoneType Type { get; set; }
        public override string ToString()
        {
            return Number + " : " + Type;
        }
    }
}
