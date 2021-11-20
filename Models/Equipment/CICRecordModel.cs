using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobinBradley2021.Models.Equipment
{
    public class CICRecordModel
    {
        public int ID { get; set; }
        public EquipmentModel Equipment { get; set; }
        public CICRecordModel Class { get; set; }
        public DateTime DateDue { get; set; }
        public DateTime DateStart { get; set; }
    }
}

