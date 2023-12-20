using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Views;
using System;
using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.Models.Tools;
using Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM;
using Database_Hospital_Application.ViewModels.ViewsVM.GuestVM;
using Database_Hospital_Application.ViewModels.ViewsVM.NurseVM;
using Database_Hospital_Application.ViewModels.ViewsVM.AssistantVM;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class LoginWindowViewModel : BaseViewModel
    {
        public ICommand LoginCommand { get; private set; }
        public ICommand LoginAsGuestCommand { get; private set; }

        private bool _isLoggingIn;
        public bool IsLoggingIn
        {
            get { return _isLoggingIn; }
            set
            {
                _isLoggingIn = value;
                OnPropertyChange(nameof(IsLoggingIn));
                (LoginCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChange(nameof(Username));
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChange(nameof(Password));
            }
        }

        protected override void OnPropertyChange(string propertyName)
        {
            base.OnPropertyChange(propertyName);
            (LoginCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }

        public LoginWindowViewModel()
        {
            LoginCommand = new RelayCommand(async (o) => await ExecuteLogin(), (o) => CanExecuteLogin());
            LoginAsGuestCommand = new RelayCommand(OpenGuestWindow);
        }

        private void OpenGuestWindow(object? obj)
        {
            User user = new User
            {
                Name = "Host",
                Employee = new Employee
                {
                    FirstName = "Host",
                    UserName = "Username - host",
                    LastName = "" 
                },
                RoleID = 5 //Guest
            };
            OpenProfileWindow(user);
        }

        private bool CanExecuteLogin()
        {
            return !IsLoggingIn && !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }

        private async Task ExecuteLogin()
        {
            UserRepo userRepo = new UserRepo();
            if (IsLoggingIn)
                return;

            if (Username == null || Password == null)
            {
                MessageBox.Show("Vyplňte prosím všechna pole!", "Info", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            
            
            User user;
            try
            {
                IsLoggingIn = true;
                user = await userRepo.LoginUserAsync(Username, Password);
                IsLoggingIn = false;
            } catch
            {
                return;
            }
            if (user == null)
            {
                MessageBox.Show("Zadané přihlašovací údaje se neschodují!", "Info", MessageBoxButton.OK, MessageBoxImage.Error);
                IsLoggingIn = false;
                return;
            }

            if (user.Name == Username && user.Password == Password)
            {
                OpenProfileWindow(user);
                IsLoggingIn = false;
            }
        }

        public void OpenProfileWindow(User user)
        {
            switch (user.RoleID)
            {
                case 1:
                    Views.MainWindow profileWindow = new Views.MainWindow();
                    profileWindow.DataContext = new NavigateVM(user);
                    profileWindow.Show();
                    CloseAction?.Invoke();
                    break;
                case 2:
                    Views.DoctorWindow doctorWindow = new Views.DoctorWindow();
                    doctorWindow.DataContext = new DoctorNavigateVM(user);
                    doctorWindow.Show();
                    CloseAction?.Invoke();
                    break;
                case 3:
                    Views.NurseWindow nurseWindow = new Views.NurseWindow();
                    nurseWindow.DataContext = new NurseNavigateVM(user);
                    nurseWindow.Show();
                    CloseAction?.Invoke();
                    break;

                case 4:
                    Views.AssistantWindow assistantWindow = new Views.AssistantWindow();
                    assistantWindow.DataContext = new AssistantNavigateVM(user);
                    assistantWindow.Show();
                    CloseAction?.Invoke();
                    break;
                case 5:
                    Views.GuestWindows guestWindow = new Views.GuestWindows();
                    guestWindow.DataContext = new GuestNavigateVM();
                    guestWindow.Show();
                    CloseAction?.Invoke();
                    break;
                default:
                    break;
            }
        }

        public Action CloseAction { get; set; }

        
    }
}
