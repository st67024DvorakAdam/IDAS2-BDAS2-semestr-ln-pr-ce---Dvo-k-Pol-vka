using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

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

        // BUTTONS
        public ICommand AddNewAddressCommand { get; private set; }

        private void InitializeCommands()
        {
            
            AddNewAddressCommand = new RelayCommand(AddNewAddressAction);
            
        }

        private Address _newAddress;
        public Address NewAddress
        {
            get { return _newAddress; }
            set
            {
                _newAddress = value;
                OnPropertyChange(nameof(NewAddress));
            }
        }
        private void AddNewAddressAction(object parameter)
        {

            AddressRepo addressRepo = new AddressRepo();
            addressRepo.AddAddress(NewAddress);
            LoadAddressesAsync();

        }

        public AddressesVM()
        {
            LoadAddressesAsync();
            AddressesView = CollectionViewSource.GetDefaultView(AddressesList);
            AddressesView.Filter = AddressFilter;
            NewAddress = new Address();
            InitializeCommands();
        }

        private async Task LoadAddressesAsync()
        {
            AddressRepo repo = new AddressRepo();
            AddressesList = await repo.GetAllAddressesAsync();
        }



        //FILTER/////////////////////////////////////////////////////////////////////

        private string _searchText;
        public ICollectionView AddressesView { get; private set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChange(nameof(SearchText));
                AddressesView.Refresh();
            }
        }

        private bool AddressFilter(object item)
        {
            if (string.IsNullOrWhiteSpace(_searchText)) return true;

            var address = item as Address;
            if (address == null) return false;

            return address.City.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                || address.HouseNumber.ToString().Contains(_searchText)
                || address.Country.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                || address.Street.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                || address.ZipCode.ToString().Contains(_searchText);
        }
        //FILTER/////////////////////////////////////////////////////////////////////

    }
}
