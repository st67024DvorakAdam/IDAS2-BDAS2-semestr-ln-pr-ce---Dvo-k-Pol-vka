using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;
using System.Windows;

namespace Database_Hospital_Application.ViewModels.Dialogs.Edit.Doctor
{
    public class MakeProcedureVM : BaseViewModel
    {
        public event Action CloseRequested;

        public ICommand AddCommand { get; }
        public ICommand AddProcedureCommand { get; }
        public ICommand CancelCommand { get; }
        
        private PerformedProcedure _newPerformedProcedure = new PerformedProcedure();
        public PerformedProcedure NewPerformedProcedure
        {
            get => _newPerformedProcedure;
            set
            {
                _newPerformedProcedure = value;
                OnPropertyChange(nameof(NewPerformedProcedure));
                UpdateCommandStates();
            }
        }

        private ObservableCollection<Department> _departmentList;
        public ObservableCollection<Department> DepartmentList
        {
            get => _departmentList;
            set
            {
                _departmentList = value;
                OnPropertyChange(nameof(DepartmentList));
            }
        }

        private Department _selectedDepartment;
        public Department SelectedDepartment
        {
            get => _selectedDepartment;
            set
            {
                _selectedDepartment = value;
                OnPropertyChange(nameof(SelectedDepartment));
                UpdateCommandStates();
            }
        }

        private readonly PerformedProceduresRepo _proceduresRepo;
        private readonly DepartmentRepo _departmentRepo;
        private Patient _patient;
        public MakeProcedureVM(Patient patient)
        {
            _patient = patient;
            AddCommand = new RelayCommand(_ => AddProcedureAndHospitalize(),_ => CanAddProcedureAndHospitalize());
            CancelCommand = new RelayCommand(_ => Cancel());
            AddProcedureCommand = new RelayCommand(_ => AddProcedure() );

            _proceduresRepo = new PerformedProceduresRepo();
            _departmentRepo = new DepartmentRepo();

            LoadDepartments();
        }

        private void LoadDepartments()
        {
            
            DepartmentList = _departmentRepo.GetAllDepartmentsAsync().Result;
        }

        
        private async void AddProcedureAndHospitalize()
        {
            if(!IsProcedureHospitalizeValidAndFilled(NewPerformedProcedure))
            {
                MessageBox.Show("Vyplňte všechna pole!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _newPerformedProcedure.IdOfPatient = _patient.Id;
            _proceduresRepo.AddPerformedProcedure(NewPerformedProcedure);
            var hospitalizationRepo = new HospitalizationRepo();
            
            var ongoingHospitalization = await GetOngoingHospitalizationByPatientIdAsync(_patient.Id);

            if (ongoingHospitalization == null)
            {
                hospitalizationRepo.AddHospitalization(new Hospitalization(DateTime.Now, NewPerformedProcedure.Name, _patient.Id, _selectedDepartment.Id));
                PersonalMedicalHistoriesRepo anamnesisRepo = new PersonalMedicalHistoriesRepo();
                anamnesisRepo.AddPersonalMedicalHistory(new PersonalMedicalHistory("Byl proveden zákrok " + NewPerformedProcedure.Name + " dne " + DateTime.Now.Date.ToString("dd/MM/yyyy"), _patient.Id));
            }
            else
            {
                ongoingHospitalization.DateOut = DateTime.Now;
                hospitalizationRepo.UpdateHospitalization(ongoingHospitalization);
                hospitalizationRepo.AddHospitalization(new Hospitalization(DateTime.Now, NewPerformedProcedure.Name, _patient.Id, _selectedDepartment.Id));
                PersonalMedicalHistoriesRepo anamnesisRepo = new PersonalMedicalHistoriesRepo();
                anamnesisRepo.AddPersonalMedicalHistory(new PersonalMedicalHistory("Byl proveden zákrok " + NewPerformedProcedure.Name + " dne " + DateTime.Now.ToString("dd/MM/yyyy"), _patient.Id));
            }


            CloseRequested?.Invoke();
        }

        public async Task<Hospitalization> GetOngoingHospitalizationByPatientIdAsync(int patientId)
        {
            var hospitalizationRepo = new HospitalizationRepo();
            ObservableCollection<Hospitalization> allHospitalizations = await hospitalizationRepo.GetAllHospitalizationsAsync(_patient.Id);
            return allHospitalizations.FirstOrDefault(h => h.DateOut == null);
            
        }

        
        private void AddProcedure()
        {
            if (!IsProcedureValidAndFilled(NewPerformedProcedure))
            {
                MessageBox.Show("Vyplňte všechna pole!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _newPerformedProcedure.IdOfPatient = _patient.Id;
            _proceduresRepo.AddPerformedProcedure(NewPerformedProcedure);
            PersonalMedicalHistoriesRepo anamnesisRepo = new PersonalMedicalHistoriesRepo();
            anamnesisRepo.AddPersonalMedicalHistory(new PersonalMedicalHistory("Byl proveden zákrok " + NewPerformedProcedure.Name + " dne " + DateTime.Now.Date.ToString("dd/MM/yyyy"), _patient.Id));
            CloseRequested?.Invoke();

        }

        private bool CanAddProcedure()
        {
            return !string.IsNullOrWhiteSpace(NewPerformedProcedure.Name) &&
                    NewPerformedProcedure.Price > 0;
        }

        private bool CanAddProcedureAndHospitalize()
        {
           
            return !string.IsNullOrWhiteSpace(NewPerformedProcedure.Name) &&
                    NewPerformedProcedure.Price > 0 &&
                    SelectedDepartment != null;
        }

        public void UpdateCommandStates()
        {
            (AddCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (AddProcedureCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }

        private void Cancel()
        {
            CloseRequested?.Invoke();
        }

        private bool IsProcedureValidAndFilled(PerformedProcedure procedure)
        {
            return (!string.IsNullOrEmpty(procedure.Name) && (procedure.Price != 0 && procedure.Price != null));
        }

        private bool IsProcedureHospitalizeValidAndFilled(PerformedProcedure procedure)
        {
            return (!string.IsNullOrEmpty(procedure.Name) && (procedure.Price != 0 && procedure.Price != null) && SelectedDepartment != null);
        }
    }
}

