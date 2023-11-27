using Database_Hospital_Application.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM.PatientViewVM
{
    public class PersonalDetailsVM : BaseViewModel
    {

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
        public PersonalDetailsVM(Patient currentPatient)
        {
            _currentPatient = currentPatient;
        }

        public PersonalDetailsVM() { }
    }
}
