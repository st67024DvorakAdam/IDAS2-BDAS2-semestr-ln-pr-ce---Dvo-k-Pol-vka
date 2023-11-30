using Database_Hospital_Application.Models.Entities.HelpEntities;
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

namespace Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM
{
    public class PreceptedPillsVM : BaseViewModel
    {
        private User _currUser { get; set; }

        private ObservableCollection<DrugsPreceptedByDoctor> _drugsPreceptedByDoctorList;

        public ObservableCollection<DrugsPreceptedByDoctor> DrugsPreceptedByDoctorList
        {
            get { return _drugsPreceptedByDoctorList; }
            set
            {
                _drugsPreceptedByDoctorList = value;
                OnPropertyChange(nameof(DrugsPreceptedByDoctorList));
            }
        }


        public PreceptedPillsVM(User user)
        {
            _currUser = user;
            LoadPreceptedDrugsAsync();
            PreceptedPillsView = CollectionViewSource.GetDefaultView(DrugsPreceptedByDoctorList);
            PreceptedPillsView.Filter = PreceptedPillsFilter;
        }
        
        private async Task LoadPreceptedDrugsAsync()
        {
            DrugsRepo repo = new DrugsRepo();
            DrugsPreceptedByDoctorList = await repo.GetPreceptedDrugByDoctor(_currUser);
        }





        // Filter
        private string _searchText;
        public ICollectionView PreceptedPillsView { get; private set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChange(nameof(SearchText));
                PreceptedPillsView.Refresh();
            }
        }

        private bool PreceptedPillsFilter(object item)
        {
            if (string.IsNullOrWhiteSpace(_searchText)) return true;

            var drugs = item as DrugsPreceptedByDoctor;
            var fullName = $"{drugs._patient.FirstName} {drugs._patient.LastName}";
            var fullName2 = $"{drugs._patient.LastName} {drugs._patient.FirstName}";
            return drugs != null &&
                   (drugs._patient.FirstName.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                   || drugs._patient.LastName.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                   || fullName.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                   || fullName2.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                   || drugs._drug.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                    || drugs._drug.Dosage.ToString().Contains(_searchText));
        }
    }

}

