using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class NavigateVM : BaseViewModel
    {
        private object _currentView;
        public User _currentUser;

        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                if (_currentUser != value)
                {
                    _currentUser = value;
                    OnPropertyChange(nameof(CurrentUser));
                }
            }
        }

        public object CurrentView
        {
            get => _currentView;
            set
            {
                if (_currentView != value)
                {
                    _currentView = value;
                    OnPropertyChange(nameof(CurrentView));
                }
            }
        }

        public ICommand UsersListCommand { get; }
        public ICommand ProfileCommand { get; }
        public ICommand PatientsListCommand { get; }
        public ICommand AddressesListCommand { get; }
        public ICommand HealthInsurancesListCommand { get; }
        public ICommand PerformedProceduresListCommand { get; }
        public ICommand PersonalMedicalHistoriesListCommand { get; }
        public ICommand MedicalCardsListCommand { get; }
        public ICommand IllnessesListCommand { get; }
        public ICommand DrugsListCommand { get; }
        public ICommand ContactsListCommand { get; }
        public ICommand EmployeesListCommand { get; }
        public ICommand PhotosListCommand { get; }
        public ICommand RolesListCommand { get; }
        public ICommand AddNewUserEasyWayCommand { get; }
        public ICommand DepartmentsListCommand { get; }
        public ICommand LogsListCommand { get; }

        private void Profile(object obj) => CurrentView = new CurrUserVM(CurrentUser);
        private void Users(object obj) => CurrentView = new UserVM();
        private void Patients(object obj) => CurrentView = new PatientVM();
        private void Addresses(object obj) => CurrentView = new AddressesVM();
        private void HealthInsurances(object obj) => CurrentView = new HealthInsurancesVM();
        private void PerformedProcedures(object obj) => CurrentView = new PerformedProceduresVM();
        private void PersonalMedicalHistories(object obj) => CurrentView = new PersonalMedicalHistoriesVM();
        private void MedicalCards(object obj) => CurrentView = new MedicalCardsVM();
        private void Illnesses(object obj) => CurrentView = new IllnessesVM();
        private void Drugs(object obj) => CurrentView = new DrugsVM();
        private void Contacts(object obj) => CurrentView = new ContactsVM();
        private void Employees(object obj) => CurrentView = new EmployeeVM();
        private void Photos(object obj) => CurrentView = new PhotoVM();
        private void AddNewUserEasyWay(object obj) => CurrentView = new AddNewUserEasyWayVM();
        private void Roles(object obj) => CurrentView = new RolesVM();
        private void Departments(object obj) => CurrentView = new DepartmentVM();
        private void Logs(object obj) => CurrentView = new LogsVM();

        public NavigateVM(User user)
        {
            //defaultní pohled
            CurrentUser = user;
            CurrentView = new CurrUserVM(CurrentUser); 


            //Inicializace příkazů
            ProfileCommand = new RelayCommand(Profile);
            UsersListCommand = new RelayCommand(Users);
            PatientsListCommand = new RelayCommand(Patients);
            AddressesListCommand = new RelayCommand(Addresses);
            HealthInsurancesListCommand = new RelayCommand(HealthInsurances);
            PerformedProceduresListCommand = new RelayCommand(PerformedProcedures);
            PersonalMedicalHistoriesListCommand = new RelayCommand(PersonalMedicalHistories);
            MedicalCardsListCommand = new RelayCommand(MedicalCards);
            IllnessesListCommand = new RelayCommand(Illnesses);
            DrugsListCommand = new RelayCommand(Drugs);
            EmployeesListCommand = new RelayCommand(Employees);
            ContactsListCommand = new RelayCommand(Contacts);
            PhotosListCommand = new RelayCommand(Photos);
            RolesListCommand = new RelayCommand(Roles);
            AddNewUserEasyWayCommand = new RelayCommand(AddNewUserEasyWay);
            DepartmentsListCommand = new RelayCommand(Departments);
            LogsListCommand = new RelayCommand(Logs);

        }
    }
}
