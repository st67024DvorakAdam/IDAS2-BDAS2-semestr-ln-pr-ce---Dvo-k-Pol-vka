using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.ViewModels.ViewsVM;
using Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.Dialogs.Edit
{
    public class EditPatientVM : BaseViewModel
    {

        private Patient _editablePatient;
        public Patient EditablePatient
        {
            get { return _editablePatient; }
            set { _editablePatient = value; OnPropertyChange(nameof(EditablePatient)); }
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
            _addressesList = addressesVM.AddressesList;
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
            _healthInsurancesList = healthInsurancesVM.HealthInsurancesList;
        }

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public EditPatientVM(Patient patient)
        {
            EditablePatient = patient;
            SaveCommand = new AsyncRelayCommand(async (o) => await SaveActionAsync());
            CancelCommand = new RelayCommand(CancelAction);

            LoadHealthInsurancesFromHealthInsurancesVM();
            LoadAddressesFromAddressesVM();
        }
        private bool CanSaveExecute(object parameter)
        {

            return EditablePatient != null;
        }

        private async Task SaveActionAsync()
        {
            if (!IsFormValid())
            {
                MessageBox.Show("Vyplňte všechna pole!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (EditablePatient.BirthNumber != null && EditablePatient.BirthNumber.Length < 10)
            {
                MessageBox.Show("Rodné číslo kratší než 10!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                PatientRepo patientRepo = new PatientRepo();
                int affectedRows = await patientRepo.UpdatePatient(EditablePatient);
                if (affectedRows == 0)
                {
                    MessageBox.Show("Pacienta se nepodařilo změnit", "Not Ok", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    MessageBox.Show("Pacient byl úspšně aktualizován", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
                    OnClosingRequest();
                }


            }
            catch (Exception ex)
            {
                OnClosingRequest();
            }
        }

        public void CancelAction(object parameter)
        {
            OnClosingRequest();
        }

        public event EventHandler ClosingRequest;

        public void OnClosingRequest()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ClosingRequest?.Invoke(this, EventArgs.Empty);
            });
        }

        private bool IsFormValid()
        {
            return !string.IsNullOrWhiteSpace(EditablePatient.FirstName) &&
            !string.IsNullOrWhiteSpace(EditablePatient.LastName) &&
            !string.IsNullOrWhiteSpace(EditablePatient.Sex.ToString()) &&
                   !string.IsNullOrWhiteSpace(EditablePatient.BirthNumber) &&
            !(EditablePatient.IdAddress == null) && (EditablePatient.IdAddress != 0) &&
            !(EditablePatient.IdHealthInsurance == null) && (EditablePatient.IdHealthInsurance != 0);
        }
    }

    
}
