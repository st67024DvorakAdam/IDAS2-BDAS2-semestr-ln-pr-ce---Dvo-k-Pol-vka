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
    public class PersonalMedicalHistoriesVM :BaseViewModel
    {
        private ObservableCollection<PersonalMedicalHistory> _personalMedicalHistoriesList;

        public ObservableCollection<PersonalMedicalHistory> PersonalMedicalHistoriesList
        {
            get { return _personalMedicalHistoriesList; }
            set
            {
                _personalMedicalHistoriesList = value;
                OnPropertyChange(nameof(PersonalMedicalHistoriesList));
            }
        }


        public PersonalMedicalHistoriesVM()
        {
            PersonalMedicalHistoriesRepo repo = new PersonalMedicalHistoriesRepo();
            PersonalMedicalHistoriesList = repo.GetAllPersonalMedicalHistories();
        }
    }
}
