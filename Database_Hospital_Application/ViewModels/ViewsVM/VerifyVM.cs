using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Tools;
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
        private DateTime _codeCreationTime;
        private const int CodeValiditySeconds = 180;

        private int _verificationCode;
        public int VerificationCode
        {
            get { return _verificationCode; }
            set
                {
                    _verificationCode = value;
                    OnPropertyChange(nameof(VerificationCode));
                    }
                }
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

        
        

        public bool IsCodeStillValid()
        {
            TimeSpan elapsedTime = DateTime.Now - _codeCreationTime;
            return elapsedTime.TotalSeconds <= CodeValiditySeconds;
        }
        public VerifyVM()
        {
            _codeCreationTime = DateTime.Now;
            _verificationCode = CodeGenerator.Generate4DigitCode();
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
