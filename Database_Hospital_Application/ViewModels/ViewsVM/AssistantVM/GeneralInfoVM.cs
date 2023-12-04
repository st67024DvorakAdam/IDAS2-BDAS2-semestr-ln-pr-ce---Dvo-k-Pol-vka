using Database_Hospital_Application.Models.Entities.HelpEntities;
using Database_Hospital_Application.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.ViewModels.ViewsVM.AssistantVM
{
    public class GeneralInfoVM:BaseViewModel
    {
        private string _openingHours;

        public string OpeningHours
        {
            get { return _openingHours; }
            set
            {
                _openingHours = value;
                OnPropertyChange(nameof(OpeningHours));
            }
        }

        private string _addressOfHospital;

        public string AddressOfHospital
        {
            get { return _addressOfHospital; }
            set
            {
                _addressOfHospital = value;
                OnPropertyChange(nameof(AddressOfHospital));
            }
        }


        private string _gps;

        public string Gps
        {
            get { return _gps; }
            set
            {
                _gps = value;
                OnPropertyChange(nameof(Gps));
            }
        }

        private Contact _generalContact;

        public Contact GeneralContact
        {
            get { return _generalContact; }
            set
            {
                _generalContact = value;
                OnPropertyChange(nameof(GeneralContact));
            }
        }


    }
}
