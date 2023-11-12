using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Enums;
using Database_Hospital_Application.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class RolesVM : BaseViewModel
    {
        public ObservableCollection<string> _rolesList;

        public ObservableCollection<string> RolesList
        {
            get { return _rolesList; }
            set { _rolesList = value; OnPropertyChange(nameof(RolesList)); }
        }

        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////
        public RolesVM()
        {
            LoadRolesAsync();
            RolesView = CollectionViewSource.GetDefaultView(RolesList);
            RolesView.Filter = RolesFilter;
        }

        private async Task LoadRolesAsync()
        {
            RolesRepo  repo = new RolesRepo();
            RolesList = await repo.GetAllRoleDescriptionsAsync();
        }
        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////
        ///

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

            var role = RoleExtensions.GetRoleEnumFromString(item.ToString());
            if (role == null) return false;

            return RoleExtensions.GetEnumDescription(role).Contains(_searchText, StringComparison.OrdinalIgnoreCase);
        }
        //FILTER/////////////////////////////////////////////////////////////////////
    }
}
