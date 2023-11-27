using Database_Hospital_Application.Models.Entities;
using System;

namespace Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM.PatientViewVM
{
    public class PersonalDetailsVM : BaseViewModel
    {
        private Patient _patient;

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

        public string FirstName => _patient.FirstName;
        public string LastName => _patient.LastName;
        public string IdentificationNumber => _patient.BirthNumber;
        public string Gender => _patient.Sex.ToString();
        public string City => _patient.Address.City;
        public string Street => _patient.Address.Street;
        public int HouseNumber => _patient.Address.HouseNumber;
        public int PostalCode => _patient.Address.ZipCode;
        public string Country => _patient.Address.Country;
        public string Email => _patient.Contact.Email;
        public int Phone => _patient.Contact.PhoneNumber;
        public string InsuranceCompanyName => _patient.HealthInsurance.Name;
        public int InsuranceCompanyAbbreviation => _patient.HealthInsurance.Code;
        //public bool IsSmoker => _patient.IsSmoker ?? false;
        //public bool IsAllergic => _patient.IsAllergic ?? false;

        public PersonalDetailsVM(Patient patient)
        {
            _patient = patient;
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
    }
}
