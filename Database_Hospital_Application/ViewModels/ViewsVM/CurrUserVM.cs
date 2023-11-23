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
using Database_Hospital_Application.ViewModels.Dialogs.Edit;
using Database_Hospital_Application.Views.Lists.Dialogs.Drug;
using Database_Hospital_Application.Views.Lists.Dialogs.CurrentUser;

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

        private ICommand _editCommand;
        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                {
                    _editCommand = new RelayCommand(Edit);
                }
                return _editCommand; 
            }
        }

        private void EditPhoto(object parameter)
        {
            try
            {
                string filename = string.Empty;
                string suffix = string.Empty;
                var selectedFilePath = fileDialogService.OpenFileDialog(out filename, out suffix);
                byte[] imageBytes = File.ReadAllBytes(selectedFilePath);
                CurrentUser.Employee._foto.Image = FotoExtension.ConvertBytesToBitmapImage(imageBytes);
                UserRepo ur = new UserRepo();
                ur.UploadPhotoAsync(CurrentUser.Employee.Id, FotoExtension.BitmapImageToBytes(CurrentUser.Employee._foto.Image),filename,suffix);

                OnPropertyChange(nameof(CurrentUser));
            }
            catch(Exception ex)
            {

            }
        }

        private void Edit(object parameter)
        {

            EditCurrentUserVM editVM = new EditCurrentUserVM(CurrentUser);
            EditCurrentUserDialog editDialog = new EditCurrentUserDialog(editVM);

            editDialog.ShowDialog();

        }

    }
}

