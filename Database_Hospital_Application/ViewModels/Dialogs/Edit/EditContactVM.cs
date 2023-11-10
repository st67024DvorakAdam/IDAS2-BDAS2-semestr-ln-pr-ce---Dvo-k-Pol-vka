using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace Database_Hospital_Application.ViewModels.Dialogs.Edit
{
    public class EditContactVM : BaseViewModel
    {
        private Contact _editableContact;
        public Contact EditableContact
        {
            get { return _editableContact; }
            set { _editableContact = value; OnPropertyChange(nameof(EditableContact)); }
        }


        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public EditContactVM(Contact contact)
        {
            EditableContact = contact;
            SaveCommand = new AsyncRelayCommand(async (o) => await SaveActionAsync());
            CancelCommand = new RelayCommand(CancelAction);
        }
        private bool CanSaveExecute(object parameter)
        {

            return EditableContact != null;
        }

        private async Task SaveActionAsync()
        {
            try
            {
                ContactRepo contactRepo = new ContactRepo();
                int affectedRows = await contactRepo.UpdateContact(EditableContact);
                if (affectedRows == 0)
                {
                    MessageBox.Show("Kontakt se nepodařilo změnit", "Not Ok", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    MessageBox.Show("Kontakt byl úspšně aktualizován", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
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
