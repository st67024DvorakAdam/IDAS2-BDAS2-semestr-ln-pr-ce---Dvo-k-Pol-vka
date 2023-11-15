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
    public class EditPerformedProcedureVM : BaseViewModel
    {
        private PerformedProcedure _editablePerformedProcedure;
        public PerformedProcedure EditablePerformedProcedure
        {
            get { return _editablePerformedProcedure; }
            set { _editablePerformedProcedure = value; OnPropertyChange(nameof(EditablePerformedProcedure)); }
        }


        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public EditPerformedProcedureVM(PerformedProcedure performedProcedure)
        {
            EditablePerformedProcedure = performedProcedure;
            SaveCommand = new AsyncRelayCommand(async (o) => await SaveActionAsync());
            CancelCommand = new RelayCommand(CancelAction);
        }
        private bool CanSaveExecute(object parameter)
        {

            return EditablePerformedProcedure != null;
        }

        private async Task SaveActionAsync()
        {
            try
            {
                PerformedProceduresRepo performedProceduresRepo = new PerformedProceduresRepo();
                int affectedRows = await performedProceduresRepo.UpdatePerformedProcedure(EditablePerformedProcedure);
                if (affectedRows == 0)
                {
                    MessageBox.Show("Provedenou proceduru(výkon) se nepodařilo změnit", "Not Ok", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Provedenou proceduru(výkon) byla úspšně aktualizována", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
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
