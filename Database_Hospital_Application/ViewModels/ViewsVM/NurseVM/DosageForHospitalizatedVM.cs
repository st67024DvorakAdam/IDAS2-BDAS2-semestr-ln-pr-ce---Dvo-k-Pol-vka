using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Entities.HelpEntities;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.Views.Doctor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Database_Hospital_Application.ViewModels.ViewsVM.NurseVM
{
    public class DosageForHospitalizatedVM : BaseViewModel
    {
        private User _currUser {  get; set; }   
        private ObservableCollection<DosageForHospitalizated> _dosagesList;

        public ObservableCollection<DosageForHospitalizated> DosagesList
        {
            get { return _dosagesList; }
            set
            {
                _dosagesList = value;
                OnPropertyChange(nameof(DosagesList));
            }
        }

        private async Task LoadDosagesAsync()
        {
            DrugsRepo repo = new DrugsRepo();
            DosagesList = await repo.GetDosageForHospitalizatedPatients(_currUser);
        }

        public DosageForHospitalizatedVM(User user)
        {
            _currUser = user;
            LoadDosagesAsync();
            DosageForHospitalizatedView = CollectionViewSource.GetDefaultView(DosagesList);
            DosageForHospitalizatedView.Filter = DosageForHospitalizatedFilter;
        }

        // Filter
        private string _searchText;
        public ICollectionView DosageForHospitalizatedView { get; private set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChange(nameof(SearchText));
                DosageForHospitalizatedView.Refresh();
            }
        }

        private bool DosageForHospitalizatedFilter(object item)
        {
            if (string.IsNullOrWhiteSpace(_searchText)) return true;

            var dosage = item as DosageForHospitalizated;
            var fullName = $"{dosage._patient.FirstName} {dosage._patient.LastName}";
            var fullName2 = $"{dosage._patient.LastName} {dosage._patient.FirstName}";
            return dosage != null &&
                   (dosage._patient.FirstName.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                   || dosage._patient.LastName.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                   || fullName.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                   || fullName2.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                   || dosage._drug.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                   || dosage._illness.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                    || dosage._drug.Dosage.ToString().Contains(_searchText));
        }
    }
}
