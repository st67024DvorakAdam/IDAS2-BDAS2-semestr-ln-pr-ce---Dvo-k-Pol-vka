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
    public class MedicalCardsVM : BaseViewModel
    {
        private ObservableCollection<MedicalCard> _medicalCardsList;

        public ObservableCollection<MedicalCard> MedicalCardsList
        {
            get { return _medicalCardsList; }
            set { _medicalCardsList = value; OnPropertyChange(nameof(MedicalCardsList)); }
        }

        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////

        public MedicalCardsVM()
        {
            LoadMedicalCardsAsync();
            MedicalCardsView = CollectionViewSource.GetDefaultView(MedicalCardsList);
            MedicalCardsView.Filter = MedicalCardsFilter;    
        }

        private async Task LoadMedicalCardsAsync()
        {
            MedicalCardsRepo repo = new MedicalCardsRepo();
            MedicalCardsList = await repo.GetAllMedicalCardsAsync();
        }
        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////
        ///

        //FILTER/////////////////////////////////////////////////////////////////////

        private string _searchText;
        public ICollectionView MedicalCardsView { get; private set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChange(nameof(SearchText));
                MedicalCardsView.Refresh();
            }
        }

        private bool MedicalCardsFilter(object item)
        {

            if (string.IsNullOrWhiteSpace(_searchText)) return true;

            var medicalCard = item as MedicalCard;
            if (medicalCard == null) return false;

            return medicalCard.IdOfPatient.ToString().Contains(_searchText)
                || medicalCard.BirthNumberOfPatient.ToString().Contains(_searchText)
                || medicalCard.StringVersionOfIllnesses.Contains(_searchText, StringComparison.OrdinalIgnoreCase);
        }
        //FILTER/////////////////////////////////////////////////////////////////////

    }
}
