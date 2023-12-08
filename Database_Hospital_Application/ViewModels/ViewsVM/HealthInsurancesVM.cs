using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.ViewModels.Dialogs.Edit;
using Database_Hospital_Application.Views.Lists.Dialogs.Contact;
using Database_Hospital_Application.Views.Lists.Dialogs.HealthInsurance;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class HealthInsurancesVM : BaseViewModel
    {
        private ObservableCollection<HealthInsurance> _healthInsurancesList;

        public ObservableCollection<HealthInsurance> HealthInsurancesList
        {
            get { return _healthInsurancesList; }
            set
            {
                _healthInsurancesList = value;
                OnPropertyChange(nameof(HealthInsurancesList));
            }
        }

        // Buttony
        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand EditCommand { get; private set; }

        private HealthInsurance _selectedInsurance;
        public HealthInsurance SelectedInsurance
        {
            get { return _selectedInsurance; }
            set
            {
                if (_selectedInsurance != value)
                {
                    _selectedInsurance = value;
                    OnPropertyChange(nameof(SelectedInsurance));
                    (EditCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    (DeleteCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        private HealthInsurance _newInsurance = new HealthInsurance();
        public HealthInsurance NewInsurance
        {
            get { return _newInsurance; }
            set
            {
                _newInsurance = value;
                OnPropertyChange(nameof(NewInsurance));
            }
        }

        public HealthInsurancesVM()
        {
            LoadHealthInsurancesAsync();
            InitializeCommands();
        }

        private async Task LoadHealthInsurancesAsync()
        {
            HealthInsurancesRepo repo = new HealthInsurancesRepo();
            HealthInsurancesList = await repo.GetAllHealthInsurancesAsync();
            HealthInsurancesView = CollectionViewSource.GetDefaultView(HealthInsurancesList);
            HealthInsurancesView.Filter = HealthInsuranceFilter;
        }

        private void InitializeCommands()
        {
            AddCommand = new RelayCommand(AddNewInsuranceAction);
            DeleteCommand = new RelayCommand(DeleteInsuranceAction, CanExecuteDelete);
            EditCommand = new RelayCommand(EditInsuranceAction, CanEdit);
        }

        private bool CanExecuteDelete(object parameter)
        {
            return SelectedInsurance != null;
        }

        private async void DeleteInsuranceAction(object parameter)
        {
            if (SelectedInsurance == null) return;

            HealthInsurancesRepo repo = new HealthInsurancesRepo();
            await repo.DeleteHealthInsurance(SelectedInsurance.Id);
            await LoadHealthInsurancesAsync();
        }

        private async void AddNewInsuranceAction(object parameter)
        {
            if(!isValidAndFilled(NewInsurance))
            {
                MessageBox.Show("Vyplňte správně všechna pole!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            HealthInsurancesRepo repo = new HealthInsurancesRepo();
            await repo.AddHealthInsurance(NewInsurance);
            await LoadHealthInsurancesAsync();
            NewInsurance = new HealthInsurance();
        }

        private bool CanEdit(object parameter)
        {
            return SelectedInsurance != null;
        }

        private void EditInsuranceAction(object parameter)
        {
            if (!CanEdit(parameter)) return;


            EditInsuranceVM editVM = new EditInsuranceVM(SelectedInsurance);
            EditInsuranceDialog editDialog = new EditInsuranceDialog(editVM);

            editDialog.ShowDialog();

            
            LoadHealthInsurancesAsync();
            
        }

        //FILTER/////////////////////////////////////////////////////////////////////

        private string _searchText;
        public ICollectionView HealthInsurancesView { get; private set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChange(nameof(SearchText));
                HealthInsurancesView.Refresh();
            }
        }

        private bool HealthInsuranceFilter(object item)
        {
            if (string.IsNullOrWhiteSpace(_searchText)) return true;

            var insurance = item as HealthInsurance;
            if (insurance == null) return false;

            return insurance.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                   || insurance.Code.ToString().Contains(_searchText, StringComparison.OrdinalIgnoreCase);
        }

        private bool isValidAndFilled(HealthInsurance insurance)
        {
            return (!string.IsNullOrEmpty(insurance.Name) && (insurance.Code != 0 || insurance.Code != null) && insurance.Code.ToString().Length == 3);
        }
    }
}
