using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyEmployeeTracker.Models
{
    public class JobsiteModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string ZIP { get; set; }
        public string PhoneNumber { get; set; }
        public string Contatct { get; set; }
        public string EmailAddress { get; set; }
    }
}
