using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM.PatientViewVM;
using Database_Hospital_Application.Views.Doctor.Patient;

namespace Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM
{
    public class DoctorPatientVM : BaseViewModel
    {
        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChange(nameof(SearchText));
                UpdateCommandStates();
            }
        }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChange(nameof(CurrentView));
            }
        }

        private Patient _currentPatient;
        public Patient CurrentPatient
        {
            get { return _currentPatient; }
            set
            {
                _currentPatient = value;
                OnPropertyChange(nameof(CurrentPatient));
                UpdateCommandStates();
            }
        }

        private UserControl _currentContent;
        public UserControl CurrentContent
        {
            get => _currentContent;
            set
            {
                _currentContent = value;
                OnPropertyChange(nameof(CurrentContent));
            }
        }


        public ICommand SearchPatientCommand { get; private set; }
        public ICommand PersonalDetailsCommand { get; private set; }
        public ICommand AnamnesisCommand { get; private set; }
        public ICommand ProceduresCommand { get; private set; }
        public ICommand ActualIllnessCommand { get; private set; }
        public ICommand HospitalizationCommand { get; private set; }

        public DoctorPatientVM()
        {
            SearchPatientCommand = new RelayCommand(ExecuteSearchPatient, CanExecuteSearchPatient);
            PersonalDetailsCommand = new RelayCommand(ExecutePersonalDetails, CanExecutePatientRelatedCommands);
            AnamnesisCommand = new RelayCommand(ExecuteAnamnesis, CanExecutePatientRelatedCommands);
            ProceduresCommand = new RelayCommand(ExecuteProcedures, CanExecutePatientRelatedCommands);
            ActualIllnessCommand = new RelayCommand(ExecuteActualIllness, CanExecutePatientRelatedCommands);
            HospitalizationCommand = new RelayCommand(ExecuteHospitalization, CanExecutePatientRelatedCommands);
        }

        private async void ExecuteSearchPatient(object parameter)
        {
            if(_searchText == "" || _searchText == null)
            {
                MessageBox.Show("Vyplňte prosím pole s rodným číslem", "Pacient nenalezen", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            PatientRepo patientRepo = new PatientRepo();
            CurrentPatient = await patientRepo.GetPatientByBirthNumber(SearchText);

            if (CurrentPatient == null)
            {
                MessageBox.Show("Pacient s tímto rodným číslem nebyl nalezen.", "Pacient nenalezen", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            PersonalDetailsVM personalDetailsVM = new PersonalDetailsVM(CurrentPatient);
            var personalDetailsView = new PersonalDetailsView
            {
                DataContext = personalDetailsVM
            };

            CurrentContent = personalDetailsView;
        }




        private bool CanExecuteSearchPatient(object parameter)
        {
            return true;
        }

        private void ExecutePersonalDetails(object parameter)
        {
            
            var personalDetailsVM = new PersonalDetailsVM(CurrentPatient);
            var personalDetailsView = new PersonalDetailsView
            {
                DataContext = personalDetailsVM
            };

            CurrentContent = personalDetailsView;
        }

        private void ExecuteAnamnesis(object parameter)
        {
            MessageBox.Show("Pacient s tímto rodným číslem nebyl nalezen.", "Pacient nenalezen", MessageBoxButton.OK, MessageBoxImage.Warning);

            // CurrentView = new AnamnesisView();
        }

        private void ExecuteProcedures(object parameter)
        {
            // CurrentView = new ProceduresView();
        }

        private void ExecuteActualIllness(object parameter)
        {
            // CurrentView = new ActualIllnessView();
        }

        private void ExecuteHospitalization(object parameter)
        {
            // CurrentView = new HospitalizationView();
        }

        private void UpdateCommandStates()
        {
            (PersonalDetailsCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (AnamnesisCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (ProceduresCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (ActualIllnessCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (HospitalizationCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }

        private bool CanExecutePatientRelatedCommands(object parameter)
        {
            return CurrentPatient != null;
        }
    }
}
