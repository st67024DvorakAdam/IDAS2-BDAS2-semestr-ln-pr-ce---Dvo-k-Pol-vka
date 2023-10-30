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

        public virtual bool BoolExec(object parameter)
        {
            return true;
        }

        public bool CanExecute(object? parameter)
        {
            throw new NotImplementedException();
        }

        public abstract void Exec(object parameter);

        public void Execute(object? parameter)
        {
            throw new NotImplementedException();
        }

        protected void OnBoolExecChanged() {
            BoolExecChanged?.Invoke(this, new EventArgs());
        }
        
    }
}
