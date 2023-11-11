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
    public class PerformedProceduresVM : BaseViewModel
    {
        public ObservableCollection<PerformedProcedure> _performedProceduresList;

        public ObservableCollection<PerformedProcedure> PerformedProceduresList
        {
            get { return _performedProceduresList; }
            set { _performedProceduresList = value; OnPropertyChange(nameof(PerformedProceduresList)); }
        }

        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////
        public PerformedProceduresVM()
        {
            LoadPerformedProceduresAsync();
            PerformedProceduresView = CollectionViewSource.GetDefaultView(PerformedProceduresList);
            PerformedProceduresView.Filter = PerformedProceduresFilter;
        }

        private async Task LoadPerformedProceduresAsync()
        {
            PerformedProceduresRepo repo = new PerformedProceduresRepo();
            PerformedProceduresList = await repo.GetAllPerformedProceduresAsync();
        }
        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////
        ///

        //FILTER/////////////////////////////////////////////////////////////////////

        // Hledaný řetezec v TextBoxu pro vyhledávání
        private string _searchText;
        public ICollectionView PerformedProceduresView { get; private set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChange(nameof(SearchText));
                PerformedProceduresView.Refresh();
            }
        }

        private bool PerformedProceduresFilter(object item)
        {

            if (string.IsNullOrWhiteSpace(_searchText)) return true;

            var performedProcedure = item as PerformedProcedure;
            if (performedProcedure == null) return false;



            return performedProcedure.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                //|| performedProcedure.Id.ToString().Contains(_searchText)
                //|| performedProcedure.IdOfPatient.ToString().Contains(_searchText)
                || performedProcedure.BirthNumberOfPatient.ToString().Contains(_searchText)
                || performedProcedure.Price.ToString().Contains(_searchText)
                || performedProcedure.IsCoveredByInsurence.ToString().Contains(_searchText, StringComparison.OrdinalIgnoreCase);
        }
        //FILTER/////////////////////////////////////////////////////////////////////
    }
}
