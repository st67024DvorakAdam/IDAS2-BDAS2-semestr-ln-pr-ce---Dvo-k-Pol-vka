using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.Views.Doctor.Patient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM.PatientViewVM
{
    public class ActualIllnessVM : BaseViewModel
    {
        private ObservableCollection<DataActualIllness> _illnessList;
        private PatientRepo _patientRepo;

        public ObservableCollection<DataActualIllness> IllnessList
        {
            get { return _illnessList; }
            set
            {
                _illnessList = value;
                OnPropertyChange(nameof(IllnessList));
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

        

        public ActualIllnessVM(Patient currentPatient) 
        {
            _patientRepo = new PatientRepo();
            Patient = currentPatient; 
            LoadDataAsync(currentPatient.Id);
        }

        private async void LoadDataAsync(int id)
        {
            var illnessData = await _patientRepo.GetActualIllnessByPatientIdAsync(id);
            IllnessList = new ObservableCollection<DataActualIllness>(illnessData);
        }
    }


    public class DataActualIllness
    {
        public Illness? Illness{ set; get; }
        public Drug? PrescriptedPills { set; get; }
    }
}
