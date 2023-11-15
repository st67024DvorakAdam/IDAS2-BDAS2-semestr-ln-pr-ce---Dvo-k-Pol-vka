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
    public class EditEmployeeVM:BaseViewModel
    {
        private Employee _editableEmployee;
        public Employee EditableEmployee
        {
            get { return _editableEmployee; }
            set { _editableEmployee = value; OnPropertyChange(nameof(EditableEmployee)); }
        }


        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public EditEmployeeVM(Employee employee)
        {
            EditableEmployee = employee;
            SaveCommand = new AsyncRelayCommand(async (o) => await SaveActionAsync());
            CancelCommand = new RelayCommand(CancelAction);
        }
        private bool CanSaveExecute(object parameter)
        {

            return EditableEmployee != null;
        }

        private async Task SaveActionAsync()
        {
            try
            {
                EmployeesRepo employeesRepo = new EmployeesRepo();
                int affectedRows = await employeesRepo.UpdateEmployee(EditableEmployee);
                if (affectedRows == 0)
                {
                    MessageBox.Show("Zaměstnance se nepodařilo změnit", "Not Ok", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    MessageBox.Show("Zaměstnanec byl úspšně aktualizován", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
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
