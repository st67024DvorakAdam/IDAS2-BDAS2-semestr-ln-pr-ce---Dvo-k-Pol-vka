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
    public class PatientVM : BaseViewModel
    {
        private ObservableCollection<Patient> _patientsList;

        public ObservableCollection<Patient> PatientsList
        {
            get { return _patientsList; }
            set
            {
                _patientsList = value;
                OnPropertyChange(nameof(PatientsList));
            }
        }

        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////
        public PatientVM()
        {
            LoadPatientsAsync();
            PatientsView = CollectionViewSource.GetDefaultView(PatientsList);
            PatientsView.Filter = PatientsFilter;
        }
        private async Task LoadPatientsAsync()
        {
            PatientRepo repo = new PatientRepo();
            PatientsList = await repo.GetPatientsAsync();
        }
        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////
        
        //FILTER/////////////////////////////////////////////////////////////////////

        // Hledaný řetezec v TextBoxu pro vyhledávání
        private string _searchText;
        public ICollectionView PatientsView { get; private set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChange(nameof(SearchText));
                PatientsView.Refresh();
            }
        }

        private bool PatientsFilter(object item)
        {

            if (string.IsNullOrWhiteSpace(_searchText)) return true;

            var patient = item as Patient;
            if (patient == null) return false;



            return patient.FirstName.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                || patient.LastName.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                || patient.BirthNumber.ToString().Contains(_searchText)
                || SexEnumParser.GetStringFromEnumEnglish(patient.Sex).StartsWith(_searchText, StringComparison.OrdinalIgnoreCase);
        }
        //FILTER/////////////////////////////////////////////////////////////////////


    }
}
