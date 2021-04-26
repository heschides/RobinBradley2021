using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyEmployeeTracker.Models
{
   public class EquipmentAssignmentRecordModel
    {
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _inventoryID;
        public string InventoryID
        {
            get { return _inventoryID; }
            set { _inventoryID = value; }
        }

        private int _employeeID;
        public int EmployeeID
        {
            get { return _employeeID; }
            set { _employeeID = value; }
        }

        private DateTime _dateOut;
        public DateTime DateOut
        {
            get { return _dateOut; }
            set { _dateOut = value; }
        }
       
        private DateTime _dateIn;
        public DateTime DateIn
        {
            get { return _dateIn; }
            set { _dateIn = value; }
        }
       
        private DateTime _dueDate;
        public DateTime DueDate
        {
            get { return _dueDate; }
            set { _dueDate = value; }
        }

        private string _conditionOut;
        public string ConditionOut
        {
            get { return _conditionOut; }
            set { _conditionOut = value; }
            //TODO: Create an enum to populate a dropdown box and include data validation.
        }
   
        private string _destination;
        public string Destination
        {
            get { return _destination; }
            set { _destination = value; }
        }
    }
}
