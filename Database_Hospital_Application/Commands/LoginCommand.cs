using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.ViewModels.ViewsVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Database_Hospital_Application.Commands
{
    public class LoginCommand : BaseCommand
    {
        private readonly MainWindowViewModel _mainWindowView;
        private readonly string _password;
        private readonly string _username;

        public LoginCommand(MainWindowViewModel mainWindowViewModel, string password, string username)
        {
            _mainWindowView = mainWindowViewModel;
            _password = password;
            _username = username;
        }

        public override void Execute(object? parameter)
        {
            UserRepo userRepo = new UserRepo();
            if (userRepo.LoginUser(_password, _username))
            {
                //_mainWindowView.CurrentVM = 
                MessageBox.Show("jsme tu", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


    }
}
