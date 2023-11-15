using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.ViewModels.Dialogs.Edit;
using Database_Hospital_Application.Views.Lists.Dialogs.Contact;
using Database_Hospital_Application.Views.Lists.Dialogs.Drug;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class DrugsVM : BaseViewModel
    {
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


        // BUTTONS
        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand EditCommand { get; private set; }

        private Drug _selectedDrug;
        public Drug SelectedDrug
        {
            get { return _selectedDrug; }
            set
            {
                if (_selectedDrug != value)
                {
                    _selectedDrug = value;
                    OnPropertyChange(nameof(SelectedDrug));
                    (EditCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    (DeleteCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        private Drug _newDrug = new Drug();
        public Drug NewDrug
        {
            get { return _newDrug; }
            set
            {
                _newDrug = value;
                OnPropertyChange(nameof(NewDrug));
            }
        }

        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////
        public DrugsVM()
        {
            LoadDrugsAsync();
            DrugsView = CollectionViewSource.GetDefaultView(DrugsList);
            DrugsView.Filter = DrugsFilter;
            InitializeCommands();
        }
        private async Task LoadDrugsAsync()
        {
            DrugsRepo repo = new DrugsRepo();
            DrugsList = await repo.GetAllDrugsAsync();
        }


        private void InitializeCommands()
        {
            AddCommand = new RelayCommand(AddNewAction);
            DeleteCommand = new RelayCommand(DeleteAction, CanExecuteDelete);
            EditCommand = new RelayCommand(EditAction, CanEdit);
        }

        private bool CanExecuteDelete(object parameter)
        {
            return SelectedDrug != null;
        }

        private async void DeleteAction(object parameter)
        {
            if (SelectedDrug == null) return;

            DrugsRepo drugsRepo = new DrugsRepo();
            await drugsRepo.DeleteDrug(SelectedDrug.Id);
            await LoadDrugsAsync();
        }

        private async void AddNewAction(object parameter)
        {
            DrugsRepo drugsRepo = new DrugsRepo();
            await drugsRepo.AddDrug(NewDrug);
            await LoadDrugsAsync();
            NewDrug = new Drug();
        }

        private bool CanEdit(object parameter)
        {
            return SelectedDrug != null;
        }

        private void EditAction(object parameter)
        {
            if (!CanEdit(parameter)) return;


            EditDrugVM editVM = new EditDrugVM(SelectedDrug);
            EditDrugDialog editDialog = new EditDrugDialog(editVM);

            editDialog.ShowDialog();

            if (editDialog.DialogResult == true)
            {
                LoadDrugsAsync();
            }
        }

        //FILTER/////////////////////////////////////////////////////////////////////

        private string _searchText;
        public ICollectionView DrugsView { get; private set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChange(nameof(SearchText));
                DrugsView.Refresh();
            }
        }

        private bool DrugsFilter(object item)
        {

            if (string.IsNullOrWhiteSpace(_searchText)) return true;

            var drug = item as Drug;
            if (drug == null) return false;

            return drug.Id.ToString().Contains(_searchText)
                || drug.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                || drug.Dosage.ToString().Contains(_searchText)
                || drug.Employee_id.ToString().Contains(_searchText);
        }
        //FILTER/////////////////////////////////////////////////////////////////////
    }
}
