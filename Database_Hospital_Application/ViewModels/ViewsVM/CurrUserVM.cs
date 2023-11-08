using Database_Hospital_Application.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Database_Hospital_Application.Models.Enums;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.Commands;
using System.Windows.Input;
using System.Windows;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class CurrUserVM : BaseViewModel
    {
        private User _currentUser;
        public User CurrentUser {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChange(nameof(CurrentUser));
            }
        }



        public CurrUserVM(User currentUser)
        {
            CurrentUser = currentUser;
           
        }

        private ICommand _editPhotoCommand;
        public ICommand EditPhotoCommand
        {
            get
            {
                if (_editPhotoCommand == null)
                {
                    _editPhotoCommand = new RelayCommand(EditPhoto);
                }
                return _editPhotoCommand;
            }
        }

        private void EditPhoto(object parameter)
        {
            // Zde implementujte logiku pro editaci fotky, včetně zobrazení dialogu pro výběr souboru.
            // Po úspěšném výběru souboru aktualizujte CurrentUser.Employee._foto.Image
            // a provedete další operace, např. uložení do databáze.
            MessageBox.Show("jsem tu");

            //tohle pak dodělám
        }


    }
}

