using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class NavigateVM : BaseViewModel
    {
        private object _currentView;
        private User _currentUser;

        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                if(_currentUser!= value)
                {
                    _currentUser = value;
                    OnPropertyChange(nameof(CurrentUser));
                }
            }
        }

        public object CurrentView
        {
            get => _currentView;
            set
            {
                if (_currentView != value)
                {
                    _currentView = value;
                    OnPropertyChange(nameof(CurrentView));
                }
            }
        }

        public ICommand DatabaseCommand { get; }
        public ICommand ProfileCommand { get; }
        public ICommand SettingsCommand { get; }

        private void Profile(object obj) => CurrentView = new CurrUserVM(CurrentUser);
        private void Database(object obj) => CurrentView = new UserVM();
        private void Settings(object obj) => CurrentView = new CurrUserVM(new Models.Entities.User("david", "heslo"));

        public NavigateVM(User user)
        {
            // Nastavit defaultní pohled
            CurrentView = new UserVM(); // Měli byste mít vytvořený tento ViewModel
            CurrentUser = user;

            // Inicializace příkazů
            ProfileCommand = new RelayCommand(Profile);
            DatabaseCommand = new RelayCommand(Database);
            SettingsCommand = new RelayCommand(Settings);

            // Další inicializace...
        }
    }
}
