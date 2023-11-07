using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Enums;
using Database_Hospital_Application.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class RolesVM : BaseViewModel
    {
        public ObservableCollection<RoleEnum> _rolesList;

        public ObservableCollection<RoleEnum> RolesList
        {
            get { return _rolesList; }
            set { _rolesList = value; OnPropertyChange(nameof(RolesList)); }
        }

        public RolesVM()
        {
            LoadPhotosAsync();
        }

        private async Task LoadPhotosAsync()
        {
            RolesRepo  repo = new RolesRepo();
            RolesList = await repo.GetAllRoleEnumsAsync();
        }
    }
}
