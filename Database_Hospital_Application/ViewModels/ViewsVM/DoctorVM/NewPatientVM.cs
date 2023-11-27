using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.Views.Doctor.Patient;

namespace Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM
{
    public class NewPatientVM : BaseViewModel
    {
        private string _firstName;
        private string _lastName;
        private string _identificationNumber;
        private string _gender;
        private string _city;
        private string _street;
        private string _houseNumber;
        private string _postalCode;
        private string _country;
        private string _email;
        private string _phone;
        private string _insuranceCompanyName;
        private string _insuranceCompanyAbbreviation;
        private bool _isSmoker;
        private bool _isAllergic;

        public string FirstName
        {
            get => _firstName;
            set { _firstName = value; OnPropertyChange(nameof(FirstName)); }
        }

        public string LastName
        {
            get => _lastName;
            set { _lastName = value; 
                OnPropertyChange(nameof(LastName)); }
        }

        public string IdentificationNumber
        {
            get => _identificationNumber;
            set { _identificationNumber = value;
                OnPropertyChange(nameof(IdentificationNumber)); }
        }

        public string Gender
        {
            get => _gender;
            set { _gender = value;
                OnPropertyChange(nameof(Gender)); }
        }

        public string City
        {
            get => _city;
            set { _city = value;
                OnPropertyChange(nameof(City)); }
        }

        public string Street
        {
            get => _street;
            set { _street = value;
                OnPropertyChange(nameof(Street)); }
        }

        public string HouseNumber
        {
            get => _houseNumber;
            set { _houseNumber = value;
                OnPropertyChange(nameof(HouseNumber)); }
        }

        public string PostalCode
        {
            get => _postalCode;
            set { _postalCode = value;
                OnPropertyChange(nameof(PostalCode)); }
        }

        public string Country
        {
            get => _country;
            set { _country = value;
                OnPropertyChange(nameof(Country)); }
        }

        public string Email
        {
            get => _email;
            set { _email = value;
                OnPropertyChange(nameof(Email)); }
        }

        public string Phone
        {
            get => _phone;
            set { _phone = value;
                OnPropertyChange(nameof(Phone)); }
        }

        public string InsuranceCompanyName
        {
            get => _insuranceCompanyName;
            set { _insuranceCompanyName = value;
                OnPropertyChange(nameof(InsuranceCompanyName)); }
        }

        public string InsuranceCompanyAbbreviation
        {
            get => _insuranceCompanyAbbreviation;
            set { _insuranceCompanyAbbreviation = value;
                OnPropertyChange(nameof(InsuranceCompanyAbbreviation)); }
        }

        public bool IsSmoker
        {
            get => _isSmoker;
            set { _isSmoker = value;
                OnPropertyChange(nameof(IsSmoker)); }
        }

        public bool IsAllergic
        {
            get => _isAllergic;
            set { _isAllergic = value;
                OnPropertyChange(nameof(IsAllergic)); }
        }

        public ICommand AcceptPatientCommand { get; private set; }

        public NewPatientVM()
        {
            AcceptPatientCommand = new RelayCommand(ExecuteAcceptPatient, CanExecuteAcceptPatient);
        }

        private async void ExecuteAcceptPatient(object parameter)
        {
            AddressRepo ar = new AddressRepo();
            PatientRepo pr = new PatientRepo();
            MedicalCardsRepo mcr = new MedicalCardsRepo();
            HealthInsurancesRepo hir = new HealthInsurancesRepo();
            ContactRepo cr = new ContactRepo();

            var existingPatient = await pr.GetPatientByBirthNumber(IdentificationNumber);
            if (existingPatient != null)
            {
                MessageBox.Show("Pacient s tímto rodným číslem již existuje.", "Existující pacient", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int addressId = await ar.AddAddress(new Models.Entities.Address(Street, City, HouseNumber, Country, PostalCode));
            
            int healthInsuranceId = await hir.AddHealthInsurance(new Models.Entities.HealthInsurance(InsuranceCompanyName, InsuranceCompanyAbbreviation));
            
            int patientId = await pr.AddPatient(new Models.Entities.Patient(FirstName, LastName, IdentificationNumber, Gender, addressId.ToString(), healthInsuranceId.ToString()));

            mcr.AddMedicalCard(new Models.Entities.MedicalCard(IsSmoker, IsAllergic, patientId), null);
            
            cr.AddContact(new Models.Entities.Contact(Email, Phone, patientId, null));


            // TODO
            

            

        }


        private bool CanExecuteAcceptPatient(object parameter)
        {
            return IsFormValid();
        }

        private bool IsFormValid()
        {
            return !string.IsNullOrWhiteSpace(FirstName) &&
                   !string.IsNullOrWhiteSpace(LastName) &&
                   IsValidIdentificationNumber(IdentificationNumber) &&
                   !string.IsNullOrWhiteSpace(Gender) &&
                   !string.IsNullOrWhiteSpace(City) &&
                   !string.IsNullOrWhiteSpace(Street) &&
                   !string.IsNullOrWhiteSpace(HouseNumber) &&
                   IsValidPostalCode(PostalCode) &&
                   !string.IsNullOrWhiteSpace(Country) &&
                   !string.IsNullOrWhiteSpace(Email) &&
                   IsValidPhoneNumber(Phone) &&
                   !string.IsNullOrWhiteSpace(InsuranceCompanyName) &&
                   !string.IsNullOrWhiteSpace(InsuranceCompanyAbbreviation);
        }


        private bool IsValidPhoneNumber(string phone)
        {
            return !string.IsNullOrEmpty(phone) && Regex.IsMatch(phone, "^[0-9]{9,10}$");
        }

        private bool IsValidIdentificationNumber(string number)
        {
            return !string.IsNullOrEmpty(number) && Regex.IsMatch(number, "^[0-9]{10}$");
        }

        private bool IsValidPostalCode(string postalCode)
        {
            return !string.IsNullOrEmpty(postalCode) && Regex.IsMatch(postalCode, "^[0-9]{5}$");
        }

        


        protected override void OnPropertyChange(string propertyName)
        {
            base.OnPropertyChange(propertyName);
            (AcceptPatientCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }
    }
}
