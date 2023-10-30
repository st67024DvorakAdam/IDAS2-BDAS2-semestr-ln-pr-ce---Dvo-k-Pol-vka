using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.ViewModels.ReposVM
{
    public class UserRepoViewModel : BaseViewModel
    {
        private readonly UserRepo _user;
        public ObservableCollection<User> users => _user.users;
        public UserRepoViewModel(UserRepo user) { _user = user; }

        

    }
}
