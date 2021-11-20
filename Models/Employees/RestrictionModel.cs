using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobinBradley2021.Models
{
  public class RestrictionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public override string ToString()
        {
            if (EndDate == default) {
                return Name + ": " + BeginDate.ToShortDateString(); }
            else
            {
                return Name + ": " + BeginDate.ToShortDateString() + "until " + EndDate.ToShortDateString();
               
            }
        }
    }
}
