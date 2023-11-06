using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class PatientVM : BaseViewModel
    {
        private ObservableCollection<Patient> _patientsList;

        public ObservableCollection<Patient> PatientsList
        {
            get { return _patientsList; }
            set
            {
                _patientsList = value;
                OnPropertyChange(nameof(PatientsList));
            }
        }


        public PatientVM()
        {
            LoadPatientsAsync();
        }
        private async Task LoadPatientsAsync()
        {
            PatientRepo repo = new PatientRepo();
            PatientsList = await repo.GetPatientsAsync();
        }
    }
}
