using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Views.Doctor.Patient;
using System;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM.PatientViewVM
{
    public class PersonalDetailsVM : BaseViewModel
    {
        private Patient _patient;
        private string _firstName;
        private string _lastName;
        private string _identificationNumber;
        private string _gender;
        private string _city;
        private string _street;
        private int _houseNumber;
        private int _postalCode;
        private string _country;
        private string _email;
        private int _phone;
        private string _insuranceCompanyName;
        private int _insuranceCompanyAbbreviation;
        public Patient Patient
        {
            get => _patient;
            set
            {
                _patient = value;
                OnPropertyChange(nameof(Patient));
                UpdateProperties();
            }
        }

        public string FirstName
        {
            get => _firstName;
            set { _firstName = value; OnPropertyChange(nameof(FirstName)); }
        }

        public string LastName
        {
            get => _lastName;
            set { _lastName = value; OnPropertyChange(nameof(LastName)); }
        }

        public string IdentificationNumber
        {
            get => _identificationNumber;
            set { _identificationNumber = value; OnPropertyChange(nameof(IdentificationNumber)); }
        }

        public string Gender
        {
            get => _gender;
            set { _gender = value; OnPropertyChange(nameof(Gender)); }
        }

        public string City
        {
            get => _city;
            set { _city = value; OnPropertyChange(nameof(City)); }
        }

        public string Street
        {
            get => _street;
            set { _street = value; OnPropertyChange(nameof(Street)); }
        }

        public int HouseNumber
        {
            get => _houseNumber;
            set { _houseNumber = value; OnPropertyChange(nameof(HouseNumber)); }
        }

        public int PostalCode
        {
            get => _postalCode;
            set { _postalCode = value; OnPropertyChange(nameof(PostalCode)); }
        }

        public string Country
        {
            get => _country;
            set { _country = value; OnPropertyChange(nameof(Country)); }
        }

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChange(nameof(Email)); }
        }

        public int Phone
        {
            get => _phone;
            set { _phone = value; OnPropertyChange(nameof(Phone)); }
        }

        public string InsuranceCompanyName
        {
            get => _insuranceCompanyName;
            set { _insuranceCompanyName = value; OnPropertyChange(nameof(InsuranceCompanyName)); }
        }

        public int InsuranceCompanyAbbreviation
        {
            get => _insuranceCompanyAbbreviation;
            set { _insuranceCompanyAbbreviation = value; OnPropertyChange(nameof(InsuranceCompanyAbbreviation)); }
        }
        
        //public bool IsSmoker => _patient.IsSmoker ?? false;
        //public bool IsAllergic => _patient.IsAllergic ?? false;

        public ICommand EditPatientDetailsCommand { get; private set; }

        public ICommand SaveChangesCommand { get; private set; }

        public PersonalDetailsVM(Patient patient)
        {
            _patient = patient;

            
            _firstName = _patient.FirstName;
            _lastName = _patient.LastName;
            _identificationNumber = _patient.BirthNumber;
            _gender = _patient.Sex.ToString();
            _city = _patient.Address.City;
            _street = _patient.Address.Street;
            _houseNumber = _patient.Address.HouseNumber;
            _postalCode = _patient.Address.ZipCode;
            _country = _patient.Address.Country;
            _email = _patient.Contact.Email;
            _phone = _patient.Contact.PhoneNumber;
            _insuranceCompanyName = _patient.HealthInsurance.Name;
            _insuranceCompanyAbbreviation = _patient.HealthInsurance.Code;

            EditPatientDetailsCommand = new RelayCommand(ShowEditDialog);
            SaveChangesCommand = new RelayCommand(SaveChanges);
        }

        private void UpdateProperties()
        {
            OnPropertyChange(nameof(FirstName));
            OnPropertyChange(nameof(LastName));
            OnPropertyChange(nameof(IdentificationNumber));
            OnPropertyChange(nameof(Gender));
            OnPropertyChange(nameof(City));
            OnPropertyChange(nameof(Street));
            OnPropertyChange(nameof(HouseNumber));
            OnPropertyChange(nameof(PostalCode));
            OnPropertyChange(nameof(Country));
            OnPropertyChange(nameof(Email));
            OnPropertyChange(nameof(Phone));
            OnPropertyChange(nameof(InsuranceCompanyName));
            OnPropertyChange(nameof(InsuranceCompanyAbbreviation));
            //OnPropertyChange(nameof(IsSmoker));
            //OnPropertyChange(nameof(IsAllergic));
        }

        public PersonalDetailsVM() { }

        private void ShowEditDialog(object? parameter)
        {
            EditPersonalDetails dialog = new EditPersonalDetails(this);
            
            dialog.ShowDialog();
        }

        private void SaveChanges(object? parameter)
        {
            //TODO
        }
    }
}
