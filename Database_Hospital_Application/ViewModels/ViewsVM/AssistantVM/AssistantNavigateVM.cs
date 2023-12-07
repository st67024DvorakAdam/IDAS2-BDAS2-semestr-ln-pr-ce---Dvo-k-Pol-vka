using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM;
using Database_Hospital_Application.ViewModels.ViewsVM.NurseVM;
using Database_Hospital_Application.Views.Assistant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.ViewsVM.AssistantVM
{
    public class AssistantNavigateVM:BaseViewModel
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

        public ICommand ProfileCommand { get; }
        public ICommand GeneralInfoCommand { get; }
        public ICommand NewPatientCommand {  get; }
        public ICommand AllPreceptedPillsCommand { get; }
        public ICommand HospitalizaceCommand { get; }
        public ICommand PatientCommand { get; }
        public ICommand SubordinatesCommand { get; }

        private void Profile(object obj) => CurrentView = new CurrUserVM(CurrentUser);
        private void GeneralInfo(object obj) => CurrentView = new GeneralInfoVM();
        private void NewPatient(object obj) => CurrentView = new NewPatientVM();
        private void AllPreceptedPills(object obj) => CurrentView = new AllPreceptedPillsVM();
        private void Hospitalizace(object obj) => CurrentView = new HospitalizationVM();
        private void Patient(object obj) => CurrentView = new DoctorPatientVM(CurrentUser);
        private void Subordinates(object obj) => CurrentView = new SubordinatesVM(CurrentUser);

        public AssistantNavigateVM(User user)
        {
            //defaultní pohled
            CurrentUser = user;
            CurrentView = new CurrUserVM(CurrentUser);


            //Inicializace příkazů
            ProfileCommand = new RelayCommand(Profile);
            GeneralInfoCommand = new RelayCommand(GeneralInfo);
            NewPatientCommand = new RelayCommand(NewPatient);
            AllPreceptedPillsCommand = new RelayCommand(AllPreceptedPills);
            HospitalizaceCommand = new RelayCommand(Hospitalizace);
            PatientCommand = new RelayCommand(Patient);
            SubordinatesCommand = new RelayCommand(Subordinates);
        }
    }
}
