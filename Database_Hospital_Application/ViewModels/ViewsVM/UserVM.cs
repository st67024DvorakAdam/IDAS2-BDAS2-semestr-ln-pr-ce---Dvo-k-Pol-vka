using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
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

        // Hledaný řetezec v TextBoxu pro vyhledávání
        private string _searchText;
        public ICollectionView UsersView { get; private set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChange(nameof(SearchText));
                UsersView.Refresh();
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
            var users = await _userRepo.GetAllUsersAsync();
            UsersList = new ObservableCollection<User>(users);
            UsersView = CollectionViewSource.GetDefaultView(UsersList);
            UsersView.Filter = UserFilter;
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
            
            //var dialog = new AddUserDialog(); 

           
            //var addUserVM = new AddUserVM();
            //dialog.DataContext = addUserVM;

            //if (dialog.ShowDialog() == true)
            //{
                
            //    var newUser = addUserVM.NewUser;

                
            //    _userRepo.RegisterUserAsync(newUser);

                
            //    LoadUsersAsync();
            //}
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

        private bool UserFilter(object item)
        {
            if (string.IsNullOrWhiteSpace(_searchText)) return true;

            var user = item as User;
            if (user == null) return false;

            return user.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                || user.RoleID.ToString().Contains(_searchText);
        }
    }
}
