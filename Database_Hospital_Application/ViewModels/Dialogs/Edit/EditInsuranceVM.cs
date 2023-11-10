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
    public class EditInsuranceVM : BaseViewModel
    {
        private HealthInsurance _editableInsurance;
        public HealthInsurance EditableInsurance
        {
            get { return _editableInsurance; }
            set { _editableInsurance = value; OnPropertyChange(nameof(EditableInsurance)); }
        }
        public EditInsuranceVM(HealthInsurance healthInsurance)
        {
            EditableInsurance = healthInsurance;
            //SaveCommand = new AsyncRelayCommand(async (o) => await SaveActionAsync());
            //CancelCommand = new RelayCommand(CancelAction);
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
