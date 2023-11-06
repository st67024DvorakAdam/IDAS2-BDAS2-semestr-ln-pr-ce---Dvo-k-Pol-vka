using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.ViewModels.ViewsVM;
using System.Windows;

public class LoginCommand : BaseCommand
{
    private readonly LoginWindowViewModel _loginWindowViewModel;

    public LoginCommand(LoginWindowViewModel mainWindowViewModel)
    {
        _loginWindowViewModel = mainWindowViewModel;
    }

    public override void Execute(object? parameter)
    {
        
        UserRepo userRepo = new UserRepo();
        var _username = _loginWindowViewModel.Username;
        var _password = _loginWindowViewModel.Password;
        
        if (_username == null || _password == null) {
            MessageBox.Show("Vyplňte prosím všechna pole! ", "Info", MessageBoxButton.OK, MessageBoxImage.Warning);
            return; }


        if (_username != null && _password != null)
        {
           User us = userRepo.LoginUser(_username, _password);

        
            if (us == null)
            {
                MessageBox.Show("Zadané přihlašovací údaje se neschodují!", "Info", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (us.Name == _username && us.Password == _password)
            {
                MessageBox.Show("Execute metoda, username: " + _username + " password: " + _password, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                _loginWindowViewModel.OpenProfileWindow(us);
               

            }
        }


    }


}

