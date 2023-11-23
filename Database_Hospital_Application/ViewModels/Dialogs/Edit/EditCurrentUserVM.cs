using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.Dialogs.Edit
{
    public class EditCurrentUserVM : BaseViewModel
    {

        private User _user;
        public User User
        {
            get { return _user; }
            set { _user = value; OnPropertyChange(nameof(User)); }
        }

        private string _oldPassword;
        public string OldPassword
        {
            get { return _oldPassword; }
            set { _oldPassword = value; OnPropertyChange(nameof(OldPassword)); }
        }

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public EditCurrentUserVM(User user)
        {
            User = user;
            SaveCommand = new AsyncRelayCommand(async (o) => await SaveActionAsync());
            CancelCommand = new RelayCommand(CancelAction);
        }

        private bool CanSaveExecute(object parameter)
        {

            return User != null;
        }

        private async Task SaveActionAsync()
        {
            try
            {
                UserRepo userRepo = new UserRepo();
                int affectedRows = await userRepo.UpdateUser(User, OldPassword);
                if (affectedRows == 0)
                {
                    MessageBox.Show("Váš účet se nepodařilo změnit", "Not Ok", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else if(affectedRows == -123456789)
                {
                    MessageBox.Show("Špatně zadané původní heslo. Váš účet se nepodařilo změnit", "Not Ok", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Váš účet byl úspšně aktualizován", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
                    OnClosingRequest();
                }


            }
            catch (Exception ex)
            {
                OnClosingRequest();
            }
        }

        public void CancelAction(object parameter)
        {
            OnClosingRequest();
        }

        public event EventHandler ClosingRequest;

        public void OnClosingRequest()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ClosingRequest?.Invoke(this, EventArgs.Empty);
            });
        }
    }
}
