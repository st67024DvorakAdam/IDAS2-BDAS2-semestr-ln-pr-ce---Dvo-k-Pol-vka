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
using System.Windows;
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
        public ICommand DeletePillFromIllnessCommand { get; }


        private void UpdateCommandStates()
        {
            (PrescriptPillCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (UpdateDosageCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (DeleteIllnessCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (DeletePillFromIllnessCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }
        private User _currentUser;
        public ActualIllnessVM(Patient currentPatient, User user)
        {
            _patientRepo = new PatientRepo();
            _patient = currentPatient;
            _currentUser = user;
            LoadDataAsync(currentPatient.Id);

            AddIllnessCommand = new RelayCommand(_ => AddIllness());
            PrescriptPillCommand = new RelayCommand(_ => PrescriptPill(), _ => SelectedIllness != null);
            UpdateDosageCommand = new RelayCommand(_ => UpdateDosage(), _ => SelectedIllness != null && SelectedIllness.PrescriptedPills != null);
            DeleteIllnessCommand = new RelayCommand(_ => DeleteIllness(), _ => SelectedIllness != null);
            DeletePillFromIllnessCommand = new RelayCommand(_ => DeletePillFromIllness(), _ => SelectedIllness != null && SelectedIllness.PrescriptedPills != null);
        }

        private void DeletePillFromIllness()
        {
            DrugsRepo repo = new DrugsRepo();
            repo.DeleteDrugFromIllness(_selectedIllness.PrescriptedPills, _selectedIllness.Illness);
            LoadDataAsync(_patient.Id);
        }

        private void AddIllness()
        {
            AddNewIllnessView dialog = new AddNewIllnessView(new AddNewIllnessVM(_patient));

            dialog.ShowDialog();
            LoadDataAsync(_patient.Id);
        }

        private void PrescriptPill()
        {
            PrescriptPillView dialog = new PrescriptPillView(new PrescriptPillVM(_patient, _currentUser, _selectedIllness.Illness));

            dialog.ShowDialog();
            LoadDataAsync(_patient.Id);
        }

        private async void UpdateDosage()
        {
            _selectedIllness.PrescriptedPills.Employee_id = _currentUser.Employee.Id;
            EditDosageView dialog = new EditDosageView(new EditDosageVM(_selectedIllness.PrescriptedPills, _selectedIllness.Illness));

            dialog.ShowDialog();
            LoadDataAsync(_patient.Id);
        }

        private void DeleteIllness()
        {
            IllnessesRepo repo = new IllnessesRepo();
            repo.DeleteIllnessMeds(SelectedIllness.Illness.Id);
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
