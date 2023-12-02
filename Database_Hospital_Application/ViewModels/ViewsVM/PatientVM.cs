using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Enums;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.ViewModels.Dialogs.Edit;
using Database_Hospital_Application.Views.Lists.Dialogs.Patient;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

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

        private ObservableCollection<Address> _addressesList;

        public ObservableCollection<Address> AddressesList
        {
            get { return _addressesList; }
            set
            {
                _addressesList = value;
                OnPropertyChange(nameof(AddressesList));
            }
        }

        private void LoadAddressesFromAddressesVM()
        {
            AddressesVM addressesVM = new AddressesVM();
            AddressesList = addressesVM.AddressesList;
            var addresses = AddressesList.OrderBy(a => a.Street).ToList();
            AddressesList.Clear();
            foreach(var address in addresses)
            {
                AddressesList.Add(address);
            }
        }


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

        private void LoadHealthInsurancesFromHealthInsurancesVM()
        {
            HealthInsurancesVM healthInsurancesVM = new HealthInsurancesVM();
            HealthInsurancesList = healthInsurancesVM.HealthInsurancesList;
        }

        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand EditCommand { get; private set; }

        private Patient _selectedPatient;
        public Patient SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                if (_selectedPatient != value)
                {
                    _selectedPatient = value;
                    OnPropertyChange(nameof(SelectedPatient));
                    (EditCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    (DeleteCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        private Patient _newPatient = new Patient();
        public Patient NewPatient
        {
            get { return _newPatient; }
            set
            {
                _newPatient = value;
                OnPropertyChange(nameof(NewPatient));
            }
        }

        public PatientVM()
        {
            LoadAddressesFromAddressesVM();
            LoadHealthInsurancesFromHealthInsurancesVM();

            LoadPatientsAsync();
            InitializeCommands();
        }

        private async Task LoadPatientsAsync()
        {
            PatientRepo repo = new PatientRepo();
            PatientsList = await repo.GetAllPatientsAsync();
            PatientsView = CollectionViewSource.GetDefaultView(PatientsList);
            PatientsView.Filter = PatientsFilter;
        }

        private void InitializeCommands()
        {
            AddCommand = new RelayCommand(AddNewPatientAction);
            DeleteCommand = new RelayCommand(DeletePatientAction, CanExecuteDelete);
            EditCommand = new RelayCommand(EditAction, CanEdit);
        }

        private bool CanExecuteDelete(object parameter)
        {
            return SelectedPatient != null;
        }

        private async void DeletePatientAction(object parameter)
        {
            if (SelectedPatient == null) return;

            PatientRepo patientRepo = new PatientRepo();
            await patientRepo.DeletePatient(SelectedPatient.Id);
            await LoadPatientsAsync();
        }

        private async void AddNewPatientAction(object parameter)
        {
            if(NewPatient.BirthNumber != null && NewPatient.BirthNumber.Length < 10)
            {
                MessageBox.Show("Rodné číslo kratší než 10!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            PatientRepo patientRepo = new PatientRepo();
            await patientRepo.AddPatient(NewPatient);
            await LoadPatientsAsync();
            NewPatient = new Patient();
        }

        private bool CanEdit(object parameter)
        {
            return SelectedPatient != null;
        }

        private void EditAction(object parameter)
        {
            if (!CanEdit(parameter)) return;

            EditPatientVM editVM = new EditPatientVM(SelectedPatient);
            EditPatientDialog editDialog = new EditPatientDialog(editVM);

            editDialog.ShowDialog();

            LoadPatientsAsync();
        }

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


    }
}
