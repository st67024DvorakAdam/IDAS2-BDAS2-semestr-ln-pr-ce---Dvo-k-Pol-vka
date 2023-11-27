using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.Models.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class SystemCatalogVM : BaseViewModel
    {
        private ObservableCollection<DataClass> _systemCatalogList;

        public ObservableCollection<DataClass> SystemCatalogList
        {
            get { return _systemCatalogList; }
            set
            {
                _systemCatalogList = value;
                OnPropertyChange(nameof(SystemCatalogList));
            }
        }

        public ICollectionView SystemCatalogView { get; private set; }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChange(nameof(SearchText));
                SystemCatalogView.Refresh();
            }
        }

        public SystemCatalogVM()
        {
            LoadSystemCatalogAsync();
            SystemCatalogView = CollectionViewSource.GetDefaultView(SystemCatalogList);
            SystemCatalogView.Filter = SystemCatalogFilter;
        }

        private async Task LoadSystemCatalogAsync()
        {
            SystemCatalogRepo repo = new SystemCatalogRepo();
            SystemCatalogList = await repo.GetSystemCatalogAsync();
        }

        private bool SystemCatalogFilter(object item)
        {
            if (string.IsNullOrWhiteSpace(_searchText)) return true;

            var catalogItem = item as DataClass;
            if (catalogItem == null) return false;

            return catalogItem.Owner.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                || catalogItem.ObjectName.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                || catalogItem.ObjectType.Contains(_searchText, StringComparison.OrdinalIgnoreCase);
        }

        
    }
}
