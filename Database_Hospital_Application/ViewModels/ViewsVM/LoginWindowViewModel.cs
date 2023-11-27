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

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class LoginWindowViewModel : BaseViewModel
    {
        public ICommand LoginCommand { get; private set; }
        public ICommand ContinueWithoutLoginCommand { get; private set; }

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
        }

        private bool CanExecuteLogin()
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }

        private async Task ExecuteLogin()
        {
            UserRepo userRepo = new UserRepo();
            if (Username == null || Password == null)
            {
                MessageBox.Show("Vyplňte prosím všechna pole!", "Info", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Pro hashování přímo z passwordBoxu -> to samé musí být hashované při přidávání Employee (hashovat hned z inputu)
            //string salt = await userRepo.GetUserSaltByUsername(Username);
            //string hashedPassword = PasswordHasher.HashPassword(Password, salt);
            //User user = await userRepo.LoginUserAsync(Username, hashedPassword);

            User user = await userRepo.LoginUserAsync(Username, Password);
            if (user == null)
            {
                MessageBox.Show("Zadané přihlašovací údaje se neschodují!", "Info", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (user.Name == Username && user.Password == Password)
            {
                MessageBox.Show($"Úspěšné přihlášení, username: {Username}", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                OpenProfileWindow(user);
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

                    // budoucí window pro sestru
                    //Views.NurseWindow nurseWindow = new Views.NurseWindow();
                    //nurseWindow.DataContext = new NurseNavigateVM(user);
                    //nurseWindow.Show();
                    //CloseAction?.Invoke();
                    break;
                    
                case 4:

                    // budoucí window pro asistenta
                    //Views.AssistantWindow assistantWindow = new Views.AssistantWindow();
                    //assistantWindow.DataContext = new AssistantNavigateVM(user);
                    //assistantWindow.Show();
                    //CloseAction?.Invoke();
                    break;
                case 5:
                //profileWindow.DataContext = new NavigateVM(user);
                //profileWindow.Show();
                //CloseAction?.Invoke();
                //break;
                default:
                    break;
            }
        }

        public Action CloseAction { get; set; }

        
    }
}
