using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM
{
    public class DoctorNavigateVM : BaseViewModel
    {

        //TODO celá třída
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

        
        public ICommand PatientCommand { get; }
        public ICommand DepartmentCommand { get; }
        public ICommand PrescriptedPillsCommand { get; }
        public ICommand HospitalizaceCommand { get; }
        public ICommand NewPatientCommand { get; }
        public ICommand ProfileCommand { get; }
        public ICommand StatisticsCommand { get; }
        public ICommand PatientsListCommand { get; }

        // TODO udělat VMs 
        private void Profile(object obj) => CurrentView = new CurrUserVM(CurrentUser);
        private void Patient(object obj) => CurrentView = new DoctorPatientVM(CurrentUser);
        private void Department(object obj) => CurrentView = new SubordinatesVM(CurrentUser);
        private void PrescriptedPills(object obj) => CurrentView = new PreceptedPillsVM(CurrentUser);
        private void Hospitalizace(object obj) => CurrentView = new HospitalizationVM();
        private void NewPatient(object obj) => CurrentView = new NewPatientVM();
        private void Statisctics(object obj) => CurrentView = new StatisticsVM();
        private void Patients(object obj) => CurrentView = new PatientVM();

        public DoctorNavigateVM(User user)
        {
            //defaultní pohled
            CurrentUser = user;
            CurrentView = new CurrUserVM(CurrentUser);


            //Inicializace příkazů
            ProfileCommand = new RelayCommand(Profile);
            PatientCommand = new RelayCommand(Patient);
            PrescriptedPillsCommand = new RelayCommand(PrescriptedPills);
            HospitalizaceCommand = new RelayCommand(Hospitalizace);
            NewPatientCommand = new RelayCommand(NewPatient);
            DepartmentCommand = new RelayCommand(Department);
            StatisticsCommand = new RelayCommand(Statisctics);
            PatientsListCommand = new RelayCommand(Patients);
        }
    }
}
