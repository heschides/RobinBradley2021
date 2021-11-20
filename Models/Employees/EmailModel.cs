using GalaSoft.MvvmLight;
using static RobinBradley2021.Models.Enums;

namespace RobinBradley2021.Models
{
    public class EmailModel : ViewModelBase
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set { Set(ref _address, value); }
        }

        private EmailType _type;
        public EmailType Type
        {
            get { return _type; }
            set { Set(ref _type, value); }
        }

        public override string ToString()
        {
            return Address + " : " + Type;
        }
    }
}
