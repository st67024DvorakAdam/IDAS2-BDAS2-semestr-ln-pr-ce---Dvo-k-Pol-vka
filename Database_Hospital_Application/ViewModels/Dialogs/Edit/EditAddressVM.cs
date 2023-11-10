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
            SaveCommand = new AsyncRelayCommand(async (o) => await SaveActionAsync(), (o) => CanSaveExecute(o));
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
                await addressRepo.UpdateAddress(EditableAddress);
                OnClosingRequest();
            }
            catch (Exception ex)
            {   
                MessageBox.Show("Nepodařilo se provést změny: " + ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                OnClosingRequest(); 
            }
        }

        private void CancelAction(object parameter)
        {
            OnClosingRequest();
        }

        public event EventHandler ClosingRequest;

        protected void OnClosingRequest()
        {
            if (this.ClosingRequest != null)
            {
                this.ClosingRequest(this, EventArgs.Empty);
            }
        }
    }
}
