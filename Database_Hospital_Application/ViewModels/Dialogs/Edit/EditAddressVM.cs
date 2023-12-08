using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.Models.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.Dialogs.Edit
{
    public class EditAddressVM : BaseViewModel
    {
        private Address _editableAddress;
        public Address EditableAddress
        {
            get { return _editableAddress; }
            set { _editableAddress = value; OnPropertyChange(nameof(EditableAddress)); }
        }

        
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }


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

        public EditAddressVM(Address address)
        {
            LoadCountryCodes();

            EditableAddress = address;
            SaveCommand = new AsyncRelayCommand(async (o) => await SaveActionAsync());
            CancelCommand = new RelayCommand(CancelAction);
        }
        private bool CanSaveExecute(object parameter)
        {
            
            return EditableAddress != null; 
        }

        private async Task SaveActionAsync()
        {
            if (!isAddressValidAndFilled(EditableAddress))
            {
                MessageBox.Show("Vyplňte všechna pole!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (EditableAddress.ZipCode.ToString().Length < 5)
            {
                MessageBox.Show("PSČ musí být minimálně 5 znaků!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;

            }
            try
            {
                AddressRepo addressRepo = new AddressRepo();
                int affectedRows = await addressRepo.UpdateAddress(EditableAddress);
                if (affectedRows == 0)
                {   
                    MessageBox.Show("Adresu se nepodařilo změnit", "Not Ok", MessageBoxButton.OK, MessageBoxImage.Information);
                   
                }
                else
                {
                     MessageBox.Show("Adresa byla úspšně aktualizována","Ok", MessageBoxButton.OK, MessageBoxImage.Information);
                     OnClosingRequest();
                }
                
                
            }
            catch (Exception ex)
            {
                OnClosingRequest(); 
            }
        }

        public void CancelAction(object parameter)
        {
            OnClosingRequest();
        }

        public event EventHandler ClosingRequest;

        public void OnClosingRequest()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ClosingRequest?.Invoke(this, EventArgs.Empty);
            });
        }

        private bool isAddressValidAndFilled(Address address)
        {
            return (address != null && address.Street != null && address.Country != null && address.City != null && address.HouseNumber != null && address.ZipCode != null
                && address.Street != "" && address.Country != "" && address.City != "" && address.HouseNumber != 0 && address.ZipCode != 0
                );
        }
    }
}
