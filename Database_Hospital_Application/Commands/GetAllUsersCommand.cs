using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.ViewModels.ViewsVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Database_Hospital_Application.Commands
{
    public class GetAllUsersCommand : BaseCommand
    {

        private readonly UserListPageViewModel _userListPageViewModel;

        public GetAllUsersCommand(UserListPageViewModel userListPageViewModel)
        {
            _userListPageViewModel = userListPageViewModel;
        }

        public override void Execute(object? parameter)
        {
            UserRepo userRepo = new UserRepo();
            ObservableCollection<User> users = userRepo.GetAllUsers();
        }
    }
}
