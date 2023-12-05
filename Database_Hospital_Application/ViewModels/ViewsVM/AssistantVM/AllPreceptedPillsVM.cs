using Database_Hospital_Application.Commands;
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
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.ViewsVM.AssistantVM
{
    public class AllPreceptedPillsVM : BaseViewModel
    {

        private ObservableCollection<DrugsPreceptedByDoctor> _allPreceptedDrugsList;

        public ObservableCollection<DrugsPreceptedByDoctor> AllPreceptedDrugsList
        {
            get { return _allPreceptedDrugsList; }
            set
            {
                _allPreceptedDrugsList = value;
                OnPropertyChange(nameof(AllPreceptedDrugsList));
            }
        }

        public AllPreceptedPillsVM()
        {
            LoadPreceptedDrugsAsync();
            PreceptedPillsView = CollectionViewSource.GetDefaultView(AllPreceptedDrugsList);
            PreceptedPillsView.Filter = PreceptedPillsFilter;
        }

        private async Task LoadPreceptedDrugsAsync()
        {
            DrugsRepo repo = new DrugsRepo();
            AllPreceptedDrugsList = await repo.GetAllPreceptedDrugs();
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
                    || drugs._drug.Dosage.ToString().Contains(_searchText))
                    || drugs._nameOfDoctor.Contains(_searchText, StringComparison.OrdinalIgnoreCase);
        }
    }
}
