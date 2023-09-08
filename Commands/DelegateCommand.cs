using System;
using System.Windows.Input;

namespace Tic_tac_Toe_WPF.Commands
{
    public class DelegateCommand : ICommand
    {
        private Action<object> _execute;
        Predicate<object>? _canExecute;

        public event EventHandler? CanExecuteChanged;

        public DelegateCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
         {
            return _canExecute?.Invoke(parameter) != false;
        }

        public void Execute(object? parameter)
        {
            _execute?.Invoke(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}

