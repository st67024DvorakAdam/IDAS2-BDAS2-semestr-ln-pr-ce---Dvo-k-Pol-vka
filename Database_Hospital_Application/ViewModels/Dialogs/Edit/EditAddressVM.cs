using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using System;
using System.Collections.Generic;
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

        public EditAddressVM(Address address)
        {
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
    }
}
