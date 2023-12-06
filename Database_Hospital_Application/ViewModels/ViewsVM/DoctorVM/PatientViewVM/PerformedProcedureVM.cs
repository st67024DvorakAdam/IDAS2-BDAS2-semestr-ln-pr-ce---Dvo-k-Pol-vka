using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.Views.Doctor.Patient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM.PatientViewVM
{
    public class PerformedProcedureVM : BaseViewModel
    {

        private ObservableCollection<PerformedProcedure> _performedProcedures;
        private PerformedProcedure _selectedProcedure;
        private PerformedProceduresRepo _performedProceduresRepo;
        

        public ObservableCollection<PerformedProcedure> ProcedureList
        {
            get { return _performedProcedures; }
            set
            {
                _performedProcedures = value;
                OnPropertyChange(nameof(ProcedureList));
                UpdateCommandStates();
            }
        }

        public PerformedProcedure SelectedProcedure
        {
            get { return _selectedProcedure; }
            set
            {
                _selectedProcedure = value;
                OnPropertyChange(nameof(SelectedProcedure));
                UpdateCommandStates();
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
            }
        }

        public ICommand MakeProcedureCommand { get; private set; }
        public ICommand MakeProcedureHospitalizeCommand { get; private set; }
        public ICommand UpdateProcedureCommand { get; private set; }

        private void UpdateCommandStates()
        {
            (MakeProcedureCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (MakeProcedureHospitalizeCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (UpdateProcedureCommand as RelayCommand)?.RaiseCanExecuteChanged();
            
        }

        public PerformedProcedureVM(Patient currentPatient) 
        {   
            _performedProceduresRepo = new PerformedProceduresRepo();
            _currentPatient = currentPatient;
            LoadDataAsync();
            
            MakeProcedureCommand = new RelayCommand(_ => ExecuteMakeProcedure());
            MakeProcedureHospitalizeCommand = new RelayCommand(_ => ExecuteMakeProcedureHospitalize());
            UpdateProcedureCommand = new RelayCommand(_ => ExecuteUpdateProcedure(), _ => _selectedProcedure != null);
        }

        private async void LoadDataAsync()
        {
            var procedures = await _performedProceduresRepo.GetAllPerformedProceduresAsync(_currentPatient.Id);
            _performedProcedures = new ObservableCollection<PerformedProcedure>(procedures);
        }

        private async void ExecuteMakeProcedure()
        {
            //TODO VM
            MakeProcedureView dialog = new MakeProcedureView(new Dialogs.Edit.Doctor.MakeProcedureVM());
            dialog.ShowDialog();
            LoadDataAsync();
        }

        private async void ExecuteMakeProcedureHospitalize()
        {
            //TODO VM
            MakeProcedureHospitalize dialog = new MakeProcedureHospitalize(new Dialogs.Edit.Doctor.MakeProcedureVM());
            dialog.ShowDialog();
            LoadDataAsync();
        }

        private async void ExecuteUpdateProcedure()
        {
            //TODO VM
            EditProcedureView dialog = new EditProcedureView(new Dialogs.Edit.Doctor.EditPerformedProcedureVM());
            dialog.ShowDialog();
            LoadDataAsync();
        }
    }
}
