﻿using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;


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

        //TODO pridat do anamnez 
        private async void AddProcedureAndHospitalize()
        {
            _newPerformedProcedure.IdOfPatient = _patient.Id;
            _proceduresRepo.AddPerformedProcedure(NewPerformedProcedure);
            var hospitalizationRepo = new HospitalizationRepo();
            
            var ongoingHospitalization = await GetOngoingHospitalizationByPatientIdAsync(_patient.Id);

            if (ongoingHospitalization == null)
            {
                hospitalizationRepo.AddHospitalization(new Hospitalization(DateTime.Now, NewPerformedProcedure.Name, _patient.Id, _selectedDepartment.Id));

            }
            else
            {
                ongoingHospitalization.DateOut = DateTime.Now;
                hospitalizationRepo.UpdateHospitalization(ongoingHospitalization);
                hospitalizationRepo.AddHospitalization(new Hospitalization(DateTime.Now, NewPerformedProcedure.Name, _patient.Id, _selectedDepartment.Id));
            }


            CloseRequested?.Invoke();
        }

        public async Task<Hospitalization> GetOngoingHospitalizationByPatientIdAsync(int patientId)
        {
            var hospitalizationRepo = new HospitalizationRepo();
            ObservableCollection<Hospitalization> allHospitalizations = await hospitalizationRepo.GetAllHospitalizationsAsync(_patient.Id);
            return allHospitalizations.FirstOrDefault(h => h.DateOut == null);
            
        }

        //TODO pridat do anamnez
        private void AddProcedure()
        {
            _newPerformedProcedure.IdOfPatient = _patient.Id;
            _proceduresRepo.AddPerformedProcedure(NewPerformedProcedure);
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
    }
}

