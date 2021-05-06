using SimplyEmployeeTracker.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyEmployeeTracker.ViewModels
{
    public class EditEmployeeWindowViewModel : ViewModelBase
    {
        private int _selectedEmployee;
        public int SelectedEmployee
        {
            get { return _selectedEmployee; }
            set { OnPropertyChanged(ref _selectedEmployee, value); }
        }



    }
}
