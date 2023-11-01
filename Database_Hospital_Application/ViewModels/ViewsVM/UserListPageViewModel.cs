using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Views;
using Database_Hospital_Application.Views.Lists;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class UserListPageViewModel : BaseViewModel
    {
        private ObservableCollection<User> _users;

        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChange(nameof(Users));
            }
        }

        public UserListPageViewModel(ObservableCollection<User> users)
        {
            Users = users;
        }

        public ICommand GetAllUsersCommand { get; }

        public UserListPageViewModel(ICommand getAllUsersCommand)
        {
            GetAllUsersCommand = new GetAllUsersCommand(this);
        }

        public void OpenProfileWindow(ObservableCollection<User> users)
        {
            UserListPage profileWindow = new UserListPage(users);
        }

    }
}
