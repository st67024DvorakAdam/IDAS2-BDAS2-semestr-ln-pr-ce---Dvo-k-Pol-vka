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

    }
}

