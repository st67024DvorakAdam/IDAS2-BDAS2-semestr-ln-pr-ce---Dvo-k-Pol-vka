using Database_Hospital_Application.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Database_Hospital_Application.Models.Enums;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class ProfileWindowViewModel : BaseViewModel
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

        public string CurrentUserRole
        {
            get
            {
                // Zde byste měli implementovat logiku pro získání role uživatele
                // na základě RoleID z CurrentUser

                return RoleExtensions.GetRoleDescription(_currentUser.RoleID); //takto nenačítám popis role z db, nechat to tak?
                //return "Nějaká role";
            }
        }

        //public ImageSource CurrentUserImage
        //{
        //    get
        //    {
        //        // Zde byste měli implementovat logiku pro získání ImageSource
        //        // pro obrázek uživatele
        //       // return new BitmapImage(new Uri("cesta_k_obrázku"));
        //    }
        //}

        private User _currentZamestnanec;
        public User CurrentZamestnanec
        {
            get => _currentZamestnanec;
            set
            {
                _currentZamestnanec = value;
                OnPropertyChange(nameof(CurrentZamestnanec));
            }
        }

        public ProfileWindowViewModel(User currentUser)
        {
            CurrentUser = currentUser;
        }
    }
}

