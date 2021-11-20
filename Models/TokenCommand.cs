using RobinBradley2021.Models.Tokens;
using System;
using System.Windows.Input;

namespace RobinBradley2021.Models
{
    public class TokenCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<EmployeeToken> _execute1;
        private Action<object> _execute2;

        public TokenCommand(Action<EmployeeToken> execute1, Action<object> execute2)
        {
            _execute1 = execute1;
            _execute2 = execute2;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute1.Invoke(parameter as EmployeeToken);
            _execute2.Invoke(parameter);
        }
    }
}