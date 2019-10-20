using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfLibraryProj.Commands
{
    public class CommandExecuter : ICommand
    {
        Action ExecuteAction { get; }
        Func<bool> ValidateAction { get; }

        public CommandExecuter(Action executeAction, Func<bool> validateAction)
        {
            ExecuteAction = executeAction;
            ValidateAction = validateAction;
        }
    


        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return ValidateAction();
        }
        public void Execute(object parameter)
        {
            ExecuteAction();
        }

        public void NotifyCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, new EventArgs());
            }
        }

    }
}
