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

        public ObservableCollection<User> UsersList { get; set; }

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
        public ICommand EmulateUserCommand { get; private set; }

        public UserVM()
        {
            LoadUsersAsync();
            InitializeCommands();
        }

        private async Task LoadUsersAsync()
        {
            UserRepo repo = new UserRepo();
            UsersList = await repo.GetAllUsersAsync();
            UsersView = CollectionViewSource.GetDefaultView(UsersList);
            UsersView.Filter = UserFilter;
        }

        private void InitializeCommands()
        {
            EmulateUserCommand = new RelayCommand(EmulateUserAction);
        }

        private void EmulateUserAction(object parametr)
        {
            AddAnotherInfoToUser();
            LoginWindowViewModel lwvm = new LoginWindowViewModel();
            lwvm.OpenProfileWindow(SelectedUser);
        }

        private async Task AddAnotherInfoToUser()
        {
            UserRepo ur = new UserRepo();
            SelectedUser.Employee._department = await ur.GetUsersDepartment(SelectedUser);
            SelectedUser.Employee._foto = await ur.GetUsersPhoto(SelectedUser);
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
