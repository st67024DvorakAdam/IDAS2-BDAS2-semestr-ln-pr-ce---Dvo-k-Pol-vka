using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Database_Hospital_Application.Commands
{
    public abstract class BaseCommand : ICommand
    {
        public event EventHandler BoolExecChanged;
        public event EventHandler? CanExecuteChanged;

        //public virtual bool BoolExec(object parameter)
        //{
        //    return true;
        //}

        public virtual bool CanExecute(object? parameter)
        {
            return true;
        }

        

        public abstract void Execute(object? parameter);
        

        protected void OnBoolExecChanged() {
            BoolExecChanged?.Invoke(this, new EventArgs());
        }
        
    }
}
