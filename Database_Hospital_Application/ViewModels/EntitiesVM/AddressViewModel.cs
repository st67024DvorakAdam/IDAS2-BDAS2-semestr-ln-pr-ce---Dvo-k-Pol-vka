using Database_Hospital_Application.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Database_Hospital_Application.ViewModels
{
    public class AddressViewModel : BaseViewModel
    {
        // MODEL
        private readonly Address _address;
        

        public int Id => _address.Id;
        public string Street => _address.Street;
        public string City => _address.Street;
        public int HouseNumber => _address.HouseNumber;
        public string Country => _address.Country;
        public int ZipCode => _address.ZipCode;

        // VIEWMODEL
        public AddressViewModel(Address address) {
            _address = address;
        }


    }
}
