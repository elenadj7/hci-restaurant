using System;
using System.Windows.Input;

namespace WPF_LoginForm.ViewModels
{
    public class ViewModelCommand : ICommand
    {
        
        private readonly Action<object> executeAction;
        private readonly Predicate<object> canExecuteAction;

        public ViewModelCommand(Action<object> executeAction)
        {
            this.executeAction = executeAction;
            canExecuteAction = null;
        }

        public ViewModelCommand(Action<object> executeAction, Predicate<object> canExecuteAction)
        {
            this.executeAction = executeAction;
            this.canExecuteAction = canExecuteAction;
        }

        
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        
        public bool CanExecute(object parameter)
        {
            return canExecuteAction == null ? true : canExecuteAction(parameter);
        }

        public void Execute(object parameter)
        {
            executeAction(parameter);
        }
    }
}