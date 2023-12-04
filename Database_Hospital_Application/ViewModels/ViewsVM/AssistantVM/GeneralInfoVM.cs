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
            }
        }

        private string _addressOfHospital;

        public string AddressOfHospital
        {
            get { return _addressOfHospital; }
            set
            {
                _addressOfHospital = value;
            }
        }


        private string _gps;

        public string Gps
        {
            get { return _gps; }
            set
            {
                _gps = value;
            }
        }

        public GeneralInfoVM()
        {
            _addressOfHospital = "nám. Čs. legií 565\nPardubice I\n 530 02";
            _openingHours = "Po-NE 0:00 - 23:59";
            _gps = "50.03390232418073, 15.767215938621169";
        }
    }
}
