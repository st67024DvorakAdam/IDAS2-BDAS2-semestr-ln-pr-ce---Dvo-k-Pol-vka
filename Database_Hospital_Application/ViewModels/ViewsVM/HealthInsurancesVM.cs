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
    public class HealthInsurancesVM : BaseViewModel
    {
        private ObservableCollection<HealthInsurance> _healthInsurances;

        public ObservableCollection<HealthInsurance> HealthInsurancesList
        {
            get { return _healthInsurances; }
            set
            {
                _healthInsurances = value;
                OnPropertyChange(nameof(HealthInsurancesList));
            }
        }


        public HealthInsurancesVM()
        {
            HealthInsurancesRepo repo = new HealthInsurancesRepo();
            HealthInsurancesList = repo.GetAllHealthInsurances();
        }
    }
}
