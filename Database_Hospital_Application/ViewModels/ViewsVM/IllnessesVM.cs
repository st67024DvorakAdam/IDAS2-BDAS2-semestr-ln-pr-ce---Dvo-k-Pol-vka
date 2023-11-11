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
    public class IllnessesVM : BaseViewModel
    {
        private ObservableCollection<Illness> _illnessesList;

        public ObservableCollection<Illness> IllnessesList
        {
            get { return _illnessesList; }
            set
            {
                _illnessesList = value;
                OnPropertyChange(nameof(IllnessesList));
            }
        }

        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////

        public IllnessesVM()
        {
            LoadIllnessesAsync();
            IllnessesView = CollectionViewSource.GetDefaultView(IllnessesList);
            IllnessesView.Filter = IllnessesFilter;
        }
        private async Task LoadIllnessesAsync()
        {
            IllnessesRepo repo = new IllnessesRepo();
            IllnessesList = await repo.GetIllnessesAsync();
        }
        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////
        ///

        //FILTER/////////////////////////////////////////////////////////////////////

        private string _searchText;
        public ICollectionView IllnessesView { get; private set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChange(nameof(SearchText));
                IllnessesView.Refresh();
            }
        }

        private bool IllnessesFilter(object item)
        {

            if (string.IsNullOrWhiteSpace(_searchText)) return true;

            var illness = item as Illness;
            if (illness == null) return false;

            return illness.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                || illness.Id.ToString().Contains(_searchText);
        }
        //FILTER/////////////////////////////////////////////////////////////////////
    }
}
