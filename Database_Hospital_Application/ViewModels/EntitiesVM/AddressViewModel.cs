using Database_Hospital_Application.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.ViewModels
{
    public class AddressViewModel : INotifyPropertyChanged
    {
        private Address _address;

        public AddressViewModel()
        {
            _address = new Address();
        }

        public int Id
        {
            get { return _address.Id; }
        }

        public string Street
        {
            get { return _address.Street; }
            set
            {
                _address.Street = value;
                OnPropertyChanged(nameof(Street));
            }
        }

        public string City
        {
            get { return _address.City; }
            set
            {
                _address.City = value;
                OnPropertyChanged(nameof(City));
            }
        }

        public int HouseNumber
        {
            get { return _address.HouseNumber; }
            set
            {
                _address.HouseNumber = value;
                OnPropertyChanged(nameof(HouseNumber));
            }
        }

        public string Country
        {
            get { return _address.Country; }
            set
            {
                _address.Country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        public int ZipCode
        {
            get { return _address.ZipCode; }
            set
            {
                _address.ZipCode = value;
                OnPropertyChanged(nameof(ZipCode));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
