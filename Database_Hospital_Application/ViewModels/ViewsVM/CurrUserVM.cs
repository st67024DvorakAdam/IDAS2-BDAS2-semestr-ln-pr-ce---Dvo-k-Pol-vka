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
using System.IO;

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
            try
            {
                var selectedFilePath = fileDialogService.OpenFileDialog();
                byte[] imageBytes = File.ReadAllBytes(selectedFilePath);
                CurrentUser.Employee._foto.Image = FotoExtension.ConvertBytesToBitmapImage(imageBytes);
                UserRepo ur = new UserRepo();
                //MessageBox.Show(CurrentUser.Employee.Id + "\n" + FotoExtension.BitmapImageToBytes(CurrentUser.Employee._foto.Image).ToString());
                ur.UploadPhotoAsync(CurrentUser.Employee.Id, FotoExtension.BitmapImageToBytes(CurrentUser.Employee._foto.Image));

                OnPropertyChange(nameof(CurrentUser));
            }
            catch(Exception ex)
            {

            }
        }

    }
}

