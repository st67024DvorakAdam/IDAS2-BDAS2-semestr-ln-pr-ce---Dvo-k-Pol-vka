using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class UserVM : BaseViewModel
    {
        private readonly User _user;
        private ObservableCollection<User> _usersList;

        public string UserName
        {
            get { return _user.Name; }
            set
            {
                _user.Name = value;
                OnPropertyChange(nameof(UserName));
            }
        }

        public ObservableCollection<User> UsersList
        {
            get { return _usersList; }
            set
            {
                _usersList = value;
                OnPropertyChange(nameof(UsersList));
            }
        }

        public UserVM()
        {
            _user = new User();
            UserName = "David";


            UserRepo repo = new UserRepo();
            UsersList = repo.GetAllUsers();
        }
    }
}
