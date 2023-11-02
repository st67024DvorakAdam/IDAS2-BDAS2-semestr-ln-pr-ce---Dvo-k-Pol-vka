using Database_Hospital_Application.Commands;
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

        private void Profile(object obj) => CurrentView = new UserVM();
        private void Database(object obj) => CurrentView = new UserVM();
        private void Settings(object obj) => CurrentView = new ProfileWindowViewModel(new Models.Entities.User("david", "heslo"));

        public NavigateVM()
        {
            // Nastavit defaultní pohled
            CurrentView = new UserVM(); // Měli byste mít vytvořený tento ViewModel

            // Inicializace příkazů
            ProfileCommand = new RelayCommand(Profile);
            DatabaseCommand = new RelayCommand(Database);
            SettingsCommand = new RelayCommand(Settings);

            // Další inicializace...
        }
    }
}
