using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RobinBradley2021.Models.Enums;

namespace RobinBradley2021.Models
{
    public class PhoneModel : ViewModelBase
    {
        public int Id { get; set; }
        private string _number;
        public string Number
        {
            get { return _number; }
            set { Set(ref _number, value); }
        }
        private PhoneType _Type;

        public PhoneType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        public override string ToString()
        {
            return Number + " : " + Type;
        }
    }
}
