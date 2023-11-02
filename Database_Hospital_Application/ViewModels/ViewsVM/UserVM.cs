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
        private ObservableCollection<User> _usersList;


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
            UserRepo repo = new UserRepo();
            UsersList = repo.GetAllUsers();
        }
    }
}
