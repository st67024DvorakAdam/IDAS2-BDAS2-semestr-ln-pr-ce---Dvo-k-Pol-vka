using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.ViewModels.Dialogs.Edit;
using Database_Hospital_Application.Views.Lists.Dialogs.Illness;
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
    public class IllnessDrugConnectionVM : BaseViewModel
    {
        private ObservableCollection<IllnessDrugConnection> _illnessDrugConnectionsList;

        public ObservableCollection<IllnessDrugConnection> IllnessDrugConnectionsList
        {
            get { return _illnessDrugConnectionsList; }
            set
            {
                _illnessDrugConnectionsList = value;
                OnPropertyChange(nameof(IllnessDrugConnectionsList));
            }
        }

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

        private void LoadIllnessesFromIllnessesVM()
        {
            IllnessesVM illnessesVM = new IllnessesVM();
            _illnessesList = illnessesVM.IllnessesList;
        }


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

        private void LoadDrugssFromDrugsVM()
        {
            DrugsVM drugsVM = new DrugsVM();
            _drugsList = drugsVM.DrugsList;
        }

        // BUTTONS
        public ICommand AddCommand { get; private set; }
        public ICommand DeleteConCommand { get; private set; }
        public ICommand EditCommand { get; private set; }

        private IllnessDrugConnection _selectedIllnessDrugConnection;
        public IllnessDrugConnection SelectedIllnessDrugConnection
        {
            get { return _selectedIllnessDrugConnection; }
            set
            {
                if (_selectedIllnessDrugConnection != value)
                {
                    _selectedIllnessDrugConnection = value;
                    OnPropertyChange(nameof(SelectedIllnessDrugConnection));
                    (EditCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    (DeleteConCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        private IllnessDrugConnection _newIllnessDrugConnection = new IllnessDrugConnection();
        public IllnessDrugConnection NewIllnessDrugConnection
        {
            get { return _newIllnessDrugConnection; }
            set
            {
                _newIllnessDrugConnection = value;
                OnPropertyChange(nameof(NewIllnessDrugConnection));
            }
        }

        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////

        public IllnessDrugConnectionVM()
        {
            LoadIllnessDrugConnectionAsync();
            IllnessDrugConnectionView = CollectionViewSource.GetDefaultView(IllnessDrugConnectionsList);
            IllnessDrugConnectionView.Filter = IllnessDrugConnectionsFilter;
            InitializeCommands();

            LoadIllnessesFromIllnessesVM();
            LoadDrugssFromDrugsVM();
        }
        private async Task LoadIllnessDrugConnectionAsync()
        {
            IllnessDrugConnectionRepo repo = new IllnessDrugConnectionRepo();
            IllnessDrugConnectionsList = await repo.GetAllIllness_drugConnectionsAsync();
        }

        private void InitializeCommands()
        {
            AddCommand = new RelayCommand(AddNewAction);
            DeleteConCommand = new RelayCommand(DeleteAction, CanExecuteDelete);
            EditCommand = new RelayCommand(EditAction, CanEdit);
        }

        private bool CanExecuteDelete(object parameter)
        {
            return SelectedIllnessDrugConnection != null;
        }

        private async void DeleteAction(object parameter)
        {
            if (SelectedIllnessDrugConnection == null) return;

            IllnessDrugConnectionRepo repo = new IllnessDrugConnectionRepo();
            await repo.DeleteIllnessDrugConnection(SelectedIllnessDrugConnection);
            await LoadIllnessDrugConnectionAsync();
        }

        private async void AddNewAction(object parameter)
        {
            IllnessDrugConnectionRepo repo = new IllnessDrugConnectionRepo();
            await repo.AddIllnessDrugConnection(NewIllnessDrugConnection);
            await LoadIllnessDrugConnectionAsync();
            NewIllnessDrugConnection = new IllnessDrugConnection();
        }

        private bool CanEdit(object parameter)
        {
            return SelectedIllnessDrugConnection != null;
        }

        private void EditAction(object parameter)
        {
            //if (!CanEdit(parameter)) return;


            //EditIllnessVM editVM = new EditIllnessVM(SelectedIllnessDrugConnection);
            //EditIllnessDialog editDialog = new EditIllnessDialog(editVM);

            //editDialog.ShowDialog();

            //if (editDialog.DialogResult == true)
            //{
            //    LoadIllnessDrugConnectionAsync();
            //}
        }

        //FILTER/////////////////////////////////////////////////////////////////////

        private string _searchTextConnection;
        public ICollectionView IllnessDrugConnectionView { get; private set; }

        public string SearchTextConnection
        {
            get { return _searchTextConnection; }
            set
            {
                _searchTextConnection = value;
                OnPropertyChange(nameof(SearchTextConnection));
                IllnessDrugConnectionView.Refresh();
            }
        }

        private bool IllnessDrugConnectionsFilter(object item)
        {

            if (string.IsNullOrWhiteSpace(_searchTextConnection)) return true;

            var illnessDrugConnection = item as IllnessDrugConnection;
            if (illnessDrugConnection == null) return false;

            return illnessDrugConnection._illness.Id.ToString().Contains(_searchTextConnection)
                || illnessDrugConnection._drug.Id.ToString().Contains(_searchTextConnection);
        }
        //FILTER/////////////////////////////////////////////////////////////////////
    }
}
