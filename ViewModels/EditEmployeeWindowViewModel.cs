using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using RobinBradley2021.Models;
using RobinBradley2021.Models.Tokens;
using System;

namespace RobinBradley2021.ViewModels
{
    public class EditEmployeeWindowViewModel : ViewModelBase
    {
        private EmployeeModel _selectedEmployee;
        public EmployeeModel SelectedEmployee
        {
            get { return _selectedEmployee; }
            set { Set(ref _selectedEmployee, value); }
        }

        public EditEmployeeWindowViewModel()
        {
            Messenger.Default.Register<EmployeeToken>(this, OnNewEmployeeToken);
        }

        private void OnNewEmployeeToken(EmployeeToken token)
        {
            SelectedEmployee = token.SelectedEmployee;
        }
    }
}
