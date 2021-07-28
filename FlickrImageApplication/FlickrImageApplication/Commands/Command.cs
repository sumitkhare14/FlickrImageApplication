using System;
using System.Windows.Input;

namespace FlickrImageApplication.Commands
{
    /// <summary>
    ///     ICommand implementation for ViewModel Commands
    /// </summary>
    public class Command : ICommand
    {
        private readonly Func<bool> _canExecuteParametrizedAction;

        private readonly Action<object> _parametrizedAction;

        public Command(Action<object> parametrizedAction, Func<bool> canExecuteMethod)
        {
            _parametrizedAction = parametrizedAction;
            _canExecuteParametrizedAction = canExecuteMethod;
        }

        public Command(Action<object> parametrizedAction)
        {
            _parametrizedAction = parametrizedAction;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecuteParametrizedAction != null) return _canExecuteParametrizedAction();

            if (_parametrizedAction != null) return true;

            return false;
        }

        public void Execute(object parameter)
        {
            var theParametrizedAction = _parametrizedAction;
            theParametrizedAction?.Invoke(parameter);
        }

        public event EventHandler CanExecuteChanged = delegate { };

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}