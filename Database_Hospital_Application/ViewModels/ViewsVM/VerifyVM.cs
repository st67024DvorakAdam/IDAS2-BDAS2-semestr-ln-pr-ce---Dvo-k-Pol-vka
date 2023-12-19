using Database_Hospital_Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class VerifyVM : BaseViewModel
    {
        private int _verificationCode;

        private int _code;
        public int Code
        {
            get { return _code; }
            set
            {
                _code = value;
                OnPropertyChange(nameof(Code));
                UpdateCommandStates();
            }
        }
        public VerifyVM(int code)
        {
            _verificationCode = code;
            InitializeCommands();
        }

        public ICommand VerifyCommand { get; private set; }

        private void InitializeCommands()
        {
            VerifyCommand = new RelayCommand(VerifyCodeAction, CanExecuteVerifyCommand);
        }

        private void UpdateCommandStates()
        {
            (VerifyCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }

        private bool CanExecuteVerifyCommand(object parameter)
        {
            return _code.ToString().Length == 4;
        }

        private void VerifyCodeAction(object? obj)
        {
            throw new NotImplementedException();
        }
    }
}
