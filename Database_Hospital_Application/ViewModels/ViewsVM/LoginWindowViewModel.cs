using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class LoginWindowViewModel : BaseViewModel
    {
        //public BaseViewModel CurrentVM { get; }

        public ICommand LoginCommand { get; }
        public ICommand ContinueWithoutLoginCommand { get; }

        public LoginWindowViewModel()
        {
            LoginCommand = new LoginCommand(this);
        }

        
        private string _username;

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChange(nameof(Username)); 
            }

        }

        private string _password;

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChange(nameof(Password));
            }

        }

        public void OpenProfileWindow(User user)
        {
            Views.MainWindow profileWindow = new Views.MainWindow();
            switch (user.RoleID)
            {
                case 1:
                    profileWindow.DataContext = new NavigateVM(user);
                    profileWindow.Show();
                    CloseAction?.Invoke();
                    break;
                case 2:
                    
                    // budoucí window pro doctora
                    
                    break;
                case 3:
                    
                    // budoucí window pro sestru
                    
                    break;
                case 4:
                    
                    // budoucí window pro asistenta
                    
                    break;
                case 6:
                    profileWindow.DataContext = new NavigateVM(user);
                    profileWindow.Show();
                    CloseAction?.Invoke();
                    break;
                default:
                    break;
            }
            
        }

        public Action CloseAction { get; set; }




    }
}
