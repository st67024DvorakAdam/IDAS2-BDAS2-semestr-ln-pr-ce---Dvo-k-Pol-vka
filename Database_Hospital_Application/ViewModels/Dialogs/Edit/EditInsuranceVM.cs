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
            set { _editableInsurance = value; OnPropertyChange(nameof(EditableInsurance));
                
            }

        }


        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public EditInsuranceVM(HealthInsurance insurance)
        {
            EditableInsurance = insurance;
            SaveCommand = new AsyncRelayCommand(async (o) => await SaveActionAsync());
            CancelCommand = new RelayCommand(CancelAction);
        }
        private bool CanSaveExecute(object parameter)
        {

            return EditableInsurance != null;
        }

        private async Task SaveActionAsync()
        {
            if (!isValidAndFilled(EditableInsurance))
            {
                MessageBox.Show("Vyplňte správně všechna pole!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                HealthInsurancesRepo insuranceRepo = new HealthInsurancesRepo();
                int affectedRows = await insuranceRepo.UpdateHealthInsurance(EditableInsurance);
                if (affectedRows == 0)
                {
                    MessageBox.Show("Pojištovnu se nepodařilo změnit", "Not Ok", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                else
                {
                    MessageBox.Show("Pojišťovna byla úspšně aktualizována", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private bool isValidAndFilled(HealthInsurance insurance)
        {
            return (!string.IsNullOrEmpty(insurance.Name) && (insurance.Code != 0 || insurance.Code != null) && insurance.Code.ToString().Length == 3);
        }
    }
}
