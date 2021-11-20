﻿using System;
using System.Windows.Input;

namespace RobinBradley2021.ViewModels
{
    public class RelayCommand<T> : ICommand
    {
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new NullReferenceException("Something is null that shouldn't be.");
            _execute = execute;
            _canExecute = canExecute;
        }
        public RelayCommand(Action<object> execute) : this(execute, null)
        {
        }
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }
        public void Execute(object parameter)
        {
            _execute.Invoke(parameter);
        }
    }
}
