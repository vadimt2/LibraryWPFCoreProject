using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfLibraryProj.Commands
{
    public class DelagteCommands : ICommand 
    {
        public event EventHandler CanExecuteChanged;

        Action<object> ExecuteActionObj { get; }
        Func<object,bool> ValidateActionObj { get; }
        Action ExecuteAction { get; }
        Func<bool> ValidateAction { get; }


        public DelagteCommands(Action<object> executeActionObj, Func<object,bool> validateActionObj)
        {
            ExecuteActionObj = executeActionObj;
            ValidateActionObj = validateActionObj;
        }

        public DelagteCommands(Action executeAction, Func<bool> validateAction)
        {
            ExecuteAction = executeAction;

            ValidateAction = validateAction;
        }

        public bool CanExecute(object parameter)
        {
            return ValidateActionObj(parameter);
        }

        public void Execute(object parameter)
        {
            ExecuteActionObj(parameter);
        }

        public bool CanExecute()
        {
            return ValidateAction();
        }

        public void Execute()
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
