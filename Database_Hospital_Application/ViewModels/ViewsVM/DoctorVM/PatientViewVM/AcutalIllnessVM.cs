using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Views.Doctor.Patient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM.PatientViewVM
{
    public class AcutalIllnessVM : BaseViewModel
    {

        private ObservableCollection<DataActualIllness> _illnessList;

        public ObservableCollection<DataActualIllness> IllnessList
        {
            get { return _illnessList; }
            set
            {
                _illnessList = value;
                OnPropertyChange(nameof(IllnessList));

            }
        }
        public AcutalIllnessVM() { }
        public AcutalIllnessVM(Patient currentPatient)
        {
            LoadDataAsync(currentPatient.Id);
        }

        private void LoadDataAsync(int id)
        {

        }
    }

    public class DataActualIllness
    {
        public string Illness { set; get; }
        public string PrescriptedPills { set; get; }
        public string Dosage { set; get; }
    }
}
