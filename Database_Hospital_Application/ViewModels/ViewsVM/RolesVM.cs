using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Enums;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.Models.Tools;
using Database_Hospital_Application.ViewModels.Dialogs.Edit;
using Database_Hospital_Application.Views.Lists.Dialogs.Employee;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class RolesVM : BaseViewModel
    {
        public ObservableCollection<Role> _rolesList;

        public ObservableCollection<Role> RolesList
        {
            get { return _rolesList; }
            set { _rolesList = value; OnPropertyChange(nameof(RolesList)); }
        }


        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////
        public RolesVM()
        {
            LoadRolesAsync();
            
        }

        private async Task LoadRolesAsync()
        {
            RolesRepo repo = new RolesRepo();
            RolesList = await repo.GetAllRoleDescriptionsAsync();
            if(RolesList != null)
            {
                RolesView = CollectionViewSource.GetDefaultView(RolesList);
                RolesView.Filter = RolesFilter;
            }
        }


        //FILTER/////////////////////////////////////////////////////////////////////

        private string _searchText;
        public ICollectionView RolesView { get; private set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChange(nameof(SearchText));
                RolesView.Refresh();
            }
        }

        private bool RolesFilter(object item)
        {

            if (string.IsNullOrWhiteSpace(_searchText)) return true;

            var role = item as Role;
            if (role == null) return false;

            return role.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
            || role.Id.ToString().Contains(_searchText);
        }
        //FILTER/////////////////////////////////////////////////////////////////////
    }
}
