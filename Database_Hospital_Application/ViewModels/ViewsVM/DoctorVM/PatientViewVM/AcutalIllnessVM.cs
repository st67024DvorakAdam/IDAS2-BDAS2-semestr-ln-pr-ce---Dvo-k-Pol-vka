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
    public class ActualIllnessVM : BaseViewModel
    {
        private ObservableCollection<DataActualIllness> _illnessList;
        private PatientRepo _patientRepo;
        private DataActualIllness? _selectedIllness;

        public ObservableCollection<DataActualIllness> IllnessList
        {
            get { return _illnessList; }
            set
            {
                _illnessList = value;
                OnPropertyChange(nameof(IllnessList));
            }
        }

        public DataActualIllness SelectedIllness
        {
            get => _selectedIllness;
            set
            {
                _selectedIllness = value;
                OnPropertyChange(nameof(SelectedIllness));
                UpdateCommandStates();
            }
        }

        private Patient _patient;
        public Patient Patient
        {
            get => _patient;
            set
            {
                _patient = value;
                OnPropertyChange(nameof(Patient));
                LoadDataAsync(_patient.Id);
            }
        }

        public ICommand AddIllnessCommand { get; }
        public ICommand PrescriptPillCommand { get; }
        public ICommand UpdateDosageCommand { get; }
        public ICommand DeleteIllnessCommand { get; }


        private void UpdateCommandStates()
        {
            (PrescriptPillCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (UpdateDosageCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (DeleteIllnessCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }
        public ActualIllnessVM(Patient currentPatient)
        {
            _patientRepo = new PatientRepo();
            _patient = currentPatient;
            LoadDataAsync(currentPatient.Id);

            AddIllnessCommand = new RelayCommand(_ => AddIllness());
            PrescriptPillCommand = new RelayCommand(_ => PrescriptPill(), _ => SelectedIllness != null);
            UpdateDosageCommand = new RelayCommand(_ => UpdateDosage(), _ => SelectedIllness != null && SelectedIllness.PrescriptedPills != null);
            DeleteIllnessCommand = new RelayCommand(_ => DeleteIllness(), _ => SelectedIllness != null);
        }


        private void AddIllness()
        {
            // TODO
            AddNewIllnessView dialog = new AddNewIllnessView(new AddNewIllnessVM(_patient));

            dialog.ShowDialog();
            LoadDataAsync(_patient.Id);
        }

        private void PrescriptPill()
        {
            // TODO
            LoadDataAsync(_patient.Id);
        }

        private async void UpdateDosage()
        {
            
            EditDosageView dialog = new EditDosageView(new EditDosageVM(_selectedIllness.PrescriptedPills));

            dialog.ShowDialog();
            LoadDataAsync(_patient.Id);
        }

        private void DeleteIllness()
        {
            // TODO
            LoadDataAsync(_patient.Id);
        }

        private async void LoadDataAsync(int id)
        {
            var illnessData = await _patientRepo.GetActualIllnessByPatientIdAsync(id);
            IllnessList = null;
            IllnessList = new ObservableCollection<DataActualIllness>(illnessData);
        }
    }


    public class DataActualIllness
    {
        public Illness? Illness{ set; get; }
        public Drug? PrescriptedPills { set; get; }
    }
}
