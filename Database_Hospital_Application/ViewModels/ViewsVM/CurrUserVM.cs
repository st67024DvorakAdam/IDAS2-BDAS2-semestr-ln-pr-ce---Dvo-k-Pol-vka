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
using Database_Hospital_Application.Models.Tools;
using System.CodeDom;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class CurrUserVM : BaseViewModel
    {
        private OpenFileDialogService fileDialogService;

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
            this.fileDialogService = new OpenFileDialogService();
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
            var fileName = fileDialogService.OpenFileDialog();
            MessageBox.Show(fileName);
            //nejprve data aktualizovat
            //v userrepo mít metodu pro zápis do db
        }

    }
}

