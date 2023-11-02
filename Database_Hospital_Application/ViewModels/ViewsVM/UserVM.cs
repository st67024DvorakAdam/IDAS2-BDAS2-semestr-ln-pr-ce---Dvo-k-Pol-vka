using Database_Hospital_Application.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class UserVM : BaseViewModel
    {
        private readonly User _user;
        public string UserName
        {
            get { return _user.Name; }
            set
            {
                _user.Name = value;
                OnPropertyChange(UserName);
            }
        }

        public UserVM()
        {
            _user = new User();
            UserName = "David";
        }
    }
}
