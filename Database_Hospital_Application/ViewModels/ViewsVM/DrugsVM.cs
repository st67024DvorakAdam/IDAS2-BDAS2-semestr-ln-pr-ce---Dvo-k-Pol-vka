using Database_Hospital_Application.Models.Entities;
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
    public class DrugsVM : BaseViewModel
    {
        private ObservableCollection<Drug> _drugsList;

        public ObservableCollection<Drug> DrugsList
        {
            get { return _drugsList; }
            set
            {
                _drugsList = value;
                OnPropertyChange(nameof(DrugsList));
            }
        }

        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////
        public DrugsVM()
        {
            LoadDrugsAsync();
            DrugsView = CollectionViewSource.GetDefaultView(DrugsList);
            DrugsView.Filter = DrugsFilter;
        }
        private async Task LoadDrugsAsync()
        {
            DrugsRepo repo = new DrugsRepo();
            DrugsList = await repo.GetAllDrugsAsync();
        }
        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////
        ///

        //FILTER/////////////////////////////////////////////////////////////////////

        private string _searchText;
        public ICollectionView DrugsView { get; private set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChange(nameof(SearchText));
                DrugsView.Refresh();
            }
        }

        private bool DrugsFilter(object item)
        {

            if (string.IsNullOrWhiteSpace(_searchText)) return true;

            var drug = item as Drug;
            if (drug == null) return false;

            return drug.Id.ToString().Contains(_searchText)
                || drug.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                || drug.Dosage.ToString().Contains(_searchText)
                || drug.Doctor_id.ToString().Contains(_searchText);
        }
        //FILTER/////////////////////////////////////////////////////////////////////
    }
}
