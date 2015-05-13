using System;
using System.Windows.Input;

namespace WPF45_Commanding_Enable_Disable.Commands
{
    /// <summary>
    /// The Command class
    /// </summary>
    public class RelayActionCommand : ICommand
    {
        /// <summary>
        /// The Action Delegate representing a method with input parameter 
        /// </summary>
        public Action<object> ExecuteAction { get; set; }
        /// <summary>
        /// The Delegate, used to represent the method which defines criteria for the execution 
        /// </summary>
        public Predicate<object> CanExecuteAction { get; set; }

        public bool CanExecute(object parameter)
        {
            if (CanExecuteAction != null)
            {
                return CanExecuteAction(parameter);
            }
            return true;
        }

        /// <summary>
        /// The event which will be raised based upon the
        /// value set for the command.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            if (ExecuteAction != null)
            {
                ExecuteAction(parameter);
            }
        }
    }
}
