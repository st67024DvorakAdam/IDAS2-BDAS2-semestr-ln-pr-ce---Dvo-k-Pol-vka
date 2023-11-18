using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.Models.Tools;
using Database_Hospital_Application.ViewModels.Dialogs.Edit;
using Database_Hospital_Application.Views.Lists.Dialogs.Address;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

        private ObservableCollection<CountryInfo> _countryCodes;

        public ObservableCollection<CountryInfo> CountryCodes
        {
            get { return _countryCodes; }
            set
            {
                _countryCodes = value;
                OnPropertyChange(nameof(CountryCodes));

            }
        }

        private async Task LoadCountryCodes()
        {
            CountryCodesLoader countryCodesLoader = new CountryCodesLoader();
            await countryCodesLoader.LoadCountryCodesAsync();
            CountryCodes = countryCodesLoader.CountryCodes;
        }

        // BUTTONS
        public ICommand AddNewAddressCommand { get; private set; }

        public ICommand DeleteAddressCommand { get; private set; }
        private void InitializeCommands()
        {
            DeleteAddressCommand = new RelayCommand(DeleteAddressAction);
            AddNewAddressCommand = new RelayCommand(AddNewAddressAction);
            EditCommand = new RelayCommand(EditAction);
        }
        
        private async void DeleteAddressAction(object parameter)
        {
            if (SelectedAddress == null) return;

            AddressRepo addressRepo = new AddressRepo();

            int affectedRows = await addressRepo.DeleteAddress(SelectedAddress.Id);

            if (affectedRows == 0)
            {
                MessageBox.Show("Adresa nemohla být odstraněna.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Adresa byla úspěšně odstraněna", "Passed", MessageBoxButton.OK, MessageBoxImage.Information);
                await LoadAddressesAsync();
            }
        }

        private Address _selectedAddress;
        public Address SelectedAddress
        {
            get { return _selectedAddress; }
            set
            {
                _selectedAddress = value;
                OnPropertyChange(nameof(SelectedAddress));
                CommandManager.InvalidateRequerySuggested();
            }
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
            AddressesView = CollectionViewSource.GetDefaultView(AddressesList);
            AddressesView.Filter = AddressFilter;
            NewAddress = new Address();
        }

        
        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////
        
        public AddressesVM()
        {
            LoadCountryCodes();

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

        //EDIT///////////////////////////////////////////////////////////////////////
        

        public ICommand EditCommand { get; private set; }
        private bool CanEdit()
        {
            return SelectedAddress != null;
        }
        private void EditAction(object parametr)
        {
            if (!CanEdit()) return;
            
            
            EditAddressVM editVM = new EditAddressVM(SelectedAddress);

            EditAddressDialog editDialog = new EditAddressDialog(editVM); 
           
            
            editDialog.ShowDialog();

            
            if (editDialog.DialogResult == true)
            {
                LoadAddressesAsync();
                

            }

        }

        
    }
}
