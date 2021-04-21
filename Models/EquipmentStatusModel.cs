namespace SimplyEmployeeTracker.Models
{
    public class EquipmentStatusModel
    {
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _type;
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

    }
}