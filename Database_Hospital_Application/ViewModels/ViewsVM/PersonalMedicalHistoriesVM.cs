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
    public class PersonalMedicalHistoriesVM : BaseViewModel
    {
        public ObservableCollection<PersonalMedicalHistory> _personalMedicalHistoriesList;

        public ObservableCollection<PersonalMedicalHistory> PersonalMedicalHistoriesList
        {
            get { return _personalMedicalHistoriesList; }
            set { _personalMedicalHistoriesList = value; OnPropertyChange(nameof(PersonalMedicalHistoriesList)); }
        }

        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////
        public PersonalMedicalHistoriesVM()
        {
            LoadPersonalMedicalHistoriesAsync();
            PersonalMedicalHistoriesView = CollectionViewSource.GetDefaultView(PersonalMedicalHistoriesList);
            PersonalMedicalHistoriesView.Filter = PersonalMedicalHistoriesFilter;
        }

        private async Task LoadPersonalMedicalHistoriesAsync()
        {
            PersonalMedicalHistoriesRepo repo = new PersonalMedicalHistoriesRepo();
            PersonalMedicalHistoriesList = await repo.GetAllPersonalMedicalHistoriesAsync();
        }
        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////
        ///
        //FILTER/////////////////////////////////////////////////////////////////////

        // Hledaný řetezec v TextBoxu pro vyhledávání
        private string _searchText;
        public ICollectionView PersonalMedicalHistoriesView { get; private set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChange(nameof(SearchText));
                PersonalMedicalHistoriesView.Refresh();
            }
        }

        private bool PersonalMedicalHistoriesFilter(object item)
        {

            if (string.IsNullOrWhiteSpace(_searchText)) return true;

            var personalMedicalHistory = item as PersonalMedicalHistory;
            if (personalMedicalHistory == null) return false;



            return personalMedicalHistory.Id.ToString().Contains(_searchText)
                || personalMedicalHistory.BirthNumberOfPatient.ToString().Contains(_searchText)
                || personalMedicalHistory.IdOfPatient.ToString().Contains(_searchText)
                || personalMedicalHistory.Description.Contains(_searchText, StringComparison.OrdinalIgnoreCase);
        }
        //FILTER/////////////////////////////////////////////////////////////////////
    }
}
