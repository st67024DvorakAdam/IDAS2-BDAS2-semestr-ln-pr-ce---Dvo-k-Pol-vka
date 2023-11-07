using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Commands
{
    public class DeleteCommand : BaseCommand
    {
        private readonly Action<object?> _execute;
        private readonly Func<object?, bool>? _canExecute;

        public DeleteCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public override bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            if (CanExecute(parameter))
            {
                _execute(parameter);
            }
        }
    }
}
