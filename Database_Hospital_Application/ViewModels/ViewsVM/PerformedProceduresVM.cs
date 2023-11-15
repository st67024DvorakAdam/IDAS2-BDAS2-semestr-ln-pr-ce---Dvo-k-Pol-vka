using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Enums;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.ViewModels.Dialogs.Edit;
using Database_Hospital_Application.Views.Lists.Dialogs.PerformedProcedure;
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
    public class PerformedProceduresVM : BaseViewModel
    {
        public ObservableCollection<PerformedProcedure> _performedProceduresList;

        public ObservableCollection<PerformedProcedure> PerformedProceduresList
        {
            get { return _performedProceduresList; }
            set { _performedProceduresList = value; OnPropertyChange(nameof(PerformedProceduresList)); }
        }

        // BUTTONS
        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        private void InitializeCommands()
        {
            DeleteCommand = new RelayCommand(DeleteAction);
            AddCommand = new RelayCommand(AddNewPerformedProcedrureAction);
            EditCommand = new RelayCommand(EditAction);
        }

        private async void DeleteAction(object parameter)
        {
            if (SelectedPerformedProcedure == null) return;

            PerformedProceduresRepo performedProceduresRepo = new PerformedProceduresRepo();
            await performedProceduresRepo.DeletePerformedProcedure(SelectedPerformedProcedure.Id);
            await LoadPerformedProceduresAsync();
        }

        private PerformedProcedure _selectedPerformedProcedure;
        public PerformedProcedure SelectedPerformedProcedure
        {
            get { return _selectedPerformedProcedure; }
            set
            {
                _selectedPerformedProcedure = value;
                OnPropertyChange(nameof(SelectedPerformedProcedure));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private PerformedProcedure _newPerformedProcedure;
        public PerformedProcedure NewPerformedProcedure
        {
            get { return _newPerformedProcedure; }
            set
            {
                _newPerformedProcedure = value;
                OnPropertyChange(nameof(NewPerformedProcedure));
            }
        }
        private void AddNewPerformedProcedrureAction(object parameter)
        {

            PerformedProceduresRepo performedProcedureRepo = new PerformedProceduresRepo();
            performedProcedureRepo.AddPerformedProcedure(NewPerformedProcedure);
            LoadPerformedProceduresAsync();
            PerformedProceduresView = CollectionViewSource.GetDefaultView(PerformedProceduresList);
            PerformedProceduresView.Filter = PerformedProceduresFilter;
            NewPerformedProcedure = new PerformedProcedure();
        }

        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////
        public PerformedProceduresVM()
        {
            LoadPerformedProceduresAsync();
            PerformedProceduresView = CollectionViewSource.GetDefaultView(PerformedProceduresList);
            PerformedProceduresView.Filter = PerformedProceduresFilter;
            NewPerformedProcedure = new PerformedProcedure();
            InitializeCommands();
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
                || performedProcedure.Id.ToString().Contains(_searchText)
                || performedProcedure.IdOfPatient.ToString().Contains(_searchText)
                //|| performedProcedure.BirthNumberOfPatient.ToString().Contains(_searchText)
                || performedProcedure.Price.ToString().Contains(_searchText)
                || performedProcedure.IsCoveredByInsurence.ToString().Contains(_searchText, StringComparison.OrdinalIgnoreCase);
        }
        //FILTER/////////////////////////////////////////////////////////////////////

        

        //EDIT///////////////////////////////////////////////////////////////////////

        


        public ICommand EditCommand { get; private set; }
        private bool CanEdit()
        {
            return SelectedPerformedProcedure != null;
        }
        private void EditAction(object parametr)
        {
            if (!CanEdit()) return;


            EditPerformedProcedureVM editVM = new EditPerformedProcedureVM(SelectedPerformedProcedure);

            EditPerformedProcedureDialog editDialog = new EditPerformedProcedureDialog(editVM);


            editDialog.ShowDialog();


            if (editDialog.DialogResult == true)
            {
                LoadPerformedProceduresAsync();


            }

        }

    }
}
