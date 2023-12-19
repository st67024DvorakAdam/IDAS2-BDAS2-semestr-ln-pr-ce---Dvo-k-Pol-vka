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

        private bool _isVerified;
        public bool IsVerified
        {
            get { return _isVerified; }
            set
            {
                _isVerified = value;
                OnPropertyChange(nameof(IsVerified));
                
            }
        }
        public VerifyVM(int code)
        {
            _verificationCode = code;
            InitializeCommands();
        }

        public event Action CloseRequested;
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

        private void Cancel()
        {
            CloseRequested?.Invoke();

        }

        private void VerifyCodeAction(object? obj)
        {
            if (_code.ToString().Equals(_verificationCode.ToString()))
            {
                _isVerified = true;
                CloseRequested?.Invoke();
            }
            else
            {
                _isVerified = false;
                CloseRequested?.Invoke();
            }
        }
    }
}
