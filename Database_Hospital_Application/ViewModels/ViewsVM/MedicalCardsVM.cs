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
    public class MedicalCardsVM : BaseViewModel
    {
        private ObservableCollection<MedicalCard> _medicalCardsList;

        public ObservableCollection<MedicalCard> MedicalCardsList
        {
            get { return _medicalCardsList; }
            set
            {
                _medicalCardsList = value;
                OnPropertyChange(nameof(MedicalCardsList));
            }
        }


        public MedicalCardsVM()
        {
            MedicalCardsRepo repo = new MedicalCardsRepo();
            MedicalCardsList = repo.GetAllMedicalCards();
        }
    }
}
