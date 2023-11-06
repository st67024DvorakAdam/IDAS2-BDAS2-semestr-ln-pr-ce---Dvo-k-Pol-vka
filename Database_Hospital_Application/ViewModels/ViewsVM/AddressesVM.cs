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
    public class AddressesVM : BaseViewModel
    {
        private ObservableCollection<Address> _addressesList;

        public ObservableCollection<Address> AddressesList
        {
            get { return _addressesList; }
            set
            {
                _addressesList = value;
                OnPropertyChange(nameof(AddressesList));
            }
        }


        public AddressesVM()
        {
            LoadAddressesAsync();
        }

        private async Task LoadAddressesAsync()
        {
            AddressRepo repo = new AddressRepo();
            AddressesList = await repo.GetAllAddressesAsync();
        }
    }
}
