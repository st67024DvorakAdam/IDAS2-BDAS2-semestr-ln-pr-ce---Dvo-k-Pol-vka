using Database_Hospital_Application.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Takhle asi ne no :D
namespace Database_Hospital_Application.ViewModels
{
    public class AddressViewModel : INotifyPropertyChanged
    {
        private List<Address> _addresses;

        public List<Address>? LoadAllAddresses()
        {
            return null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
