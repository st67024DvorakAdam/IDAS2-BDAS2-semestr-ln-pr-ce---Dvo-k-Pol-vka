using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.ViewModels.Dialogs.Edit.Doctor;
using Database_Hospital_Application.Views.Doctor.Patient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM.PatientViewVM
{
    public class HospitalizationVM : BaseViewModel
    {
        private ObservableCollection<Hospitalization> _hospitalizationList;
        private Hospitalization _selectedHospitalization;
        private HospitalizationRepo _hospitalizationRepo;
        private Hospitalization _currentHospitalization;

        public ObservableCollection<Hospitalization> HospitalizationList
        {
            get { return _hospitalizationList; }
            set
            {
                _hospitalizationList = value;
                OnPropertyChange(nameof(HospitalizationList));
            }
        }

        public Hospitalization SelectedHospitalization
        {
            get => _selectedHospitalization;
            set
            {
                _selectedHospitalization = value;
                OnPropertyChange(nameof(SelectedHospitalization));
                UpdateCommandStates();
            }
        }

        public Hospitalization CurrentHospitalization
        {
            get => _currentHospitalization;
            set
            {
                _currentHospitalization = value;
                OnPropertyChange(nameof(CurrentHospitalization));
                UpdateCommandStates();
            }
        }

        public ICommand PatientHospitalizationCommand { get; private set; }
        public ICommand UpdateOldDetailsCommand { get; private set; }
        public ICommand UpdateActualDetailsCommand { get; private set; }
        public ICommand MoveToDepartmentCommand { get; private set; }
        public ICommand DischargePatientCommand { get; private set; }

        private User _currentUser;
        private Patient _currentPatient;
       
        public HospitalizationVM(Patient currentPatient, User user)
        {
            _currentUser = user;
            _currentPatient = currentPatient;
            _hospitalizationRepo = new HospitalizationRepo();
            LoadDataAsync();
            FindCurrentHospitalization();

            PatientHospitalizationCommand = new RelayCommand(_ => HospitalizePatient(), _ => CurrentHospitalization == null);
            UpdateOldDetailsCommand = new RelayCommand(_ => UpdateOldDetails(), _ => SelectedHospitalization != null);
            UpdateActualDetailsCommand = new RelayCommand(_ => UpdateActualDetails(), _ => CurrentHospitalization != null);
            MoveToDepartmentCommand = new RelayCommand(_ => MoveToDepartment(), _ => CurrentHospitalization != null);
            DischargePatientCommand = new RelayCommand(_ => DischargePatient(), _ => CurrentHospitalization != null);
        }

        private async void LoadDataAsync()
        {
            var hospitalizationData = await _hospitalizationRepo.GetAllHospitalizationsAsync(_currentPatient.Id);
            HospitalizationList = new ObservableCollection<Hospitalization>(hospitalizationData);
        }

        private void HospitalizePatient()
        {
            HospitalizePatientView dialog = new HospitalizePatientView(new HospitalizeCurrentPatientVM(_currentPatient));

            dialog.ShowDialog();
            LoadDataAsync();
            FindCurrentHospitalization();
        }

        private void UpdateOldDetails()
        {
            EditOldDetailsView dialog = new EditOldDetailsView(new EditOldDetailsVM(_selectedHospitalization));

            dialog.ShowDialog();
            LoadDataAsync();
            FindCurrentHospitalization();
        }

        private void UpdateActualDetails()
        {
            EditOldDetailsView dialog = new EditOldDetailsView(new EditOldDetailsVM(_currentHospitalization));

            dialog.ShowDialog();
            LoadDataAsync();
            FindCurrentHospitalization();
        }

        private void MoveToDepartment()
        {
            //TODO
        }

        private void DischargePatient()
        {
            if(_currentHospitalization != null)
            {
                _currentHospitalization.DateOut = DateTime.Now;
                _hospitalizationRepo.UpdateHospitalization(_currentHospitalization);
            }
            LoadDataAsync();
            FindCurrentHospitalization();
        }

        private void UpdateCommandStates()
        {
            (PatientHospitalizationCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (UpdateOldDetailsCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (UpdateActualDetailsCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (MoveToDepartmentCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (DischargePatientCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }

        private void FindCurrentHospitalization()
        {
            CurrentHospitalization = HospitalizationList.FirstOrDefault(h => h.DateOut == null);
        }
    }
}
