using Database_Hospital_Application.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM
{
    public class PercenteOfSmokersVM : BaseViewModel
    {
        private double _percenteOfSmokers;
        public double PercenteOfSmokers
        {
            get { return _percenteOfSmokers; }
            set
            {
                _percenteOfSmokers = value;
                OnPropertyChange(nameof(PercenteOfSmokers));
            }
        }


        public PercenteOfSmokersVM()
        {
            LoadPercenteOfSmokersAsync();
        }

        public async Task LoadPercenteOfSmokersAsync()
        {
            PatientRepo repo = new PatientRepo();
            PercenteOfSmokers = await repo.GetPerceteOfSmokersAsync();
        }
    }
}
