using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class UserVM : BaseViewModel
    {
        private User _selectedUser;
        private UserRepo _userRepo;

        public ObservableCollection<User> UsersList { get; set; }
        public ObservableCollection<Patient> PatientList { get; set; }

        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChange(nameof(SelectedUser));
            }
        }

        // Příkazy pro tlačítka
        public ICommand EditUserCommand { get; private set; }
        public ICommand AddUserCommand { get; private set; }
        public ICommand DeleteUserCommand { get; private set; }

        public UserVM()
        {
            _userRepo = new UserRepo();
            LoadUsersAsync();
            InitializeCommands();
        }

        private async Task LoadUsersAsync()
        {
            UsersList = await _userRepo.GetAllUsersAsync();
            // PatientList = await repo.GetAllPatientsAsync(); 
        }

        private void InitializeCommands()
        {
            EditUserCommand = new RelayCommand(EditUserAction);
            AddUserCommand = new RelayCommand(AddUserAction);
            DeleteUserCommand = new RelayCommand(DeleteUserAction);
        }

        private void EditUserAction(object parameter)
        {
            
        }

        private void AddUserAction(object parameter)
        {
            
        }

        private void DeleteUserAction(object parameter)
        {
            
        }

        private bool CanEditUser(object parameter)
        {
            return SelectedUser != null;
        }
        private bool CanAddUser(object parameter)
        {
            return true; 
        }
        private bool CanDeleteUser(object parameter)
        {
            return SelectedUser != null;
        }
    }
}
