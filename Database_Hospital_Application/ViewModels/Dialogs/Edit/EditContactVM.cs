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
using Database_Hospital_Application.ViewModels.ViewsVM;
using System.Collections.ObjectModel;

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

        private ObservableCollection<Employee> _employeesList;

        public ObservableCollection<Employee> EmployeesList
        {
            get { return _employeesList; }
            set
            {
                _employeesList = value;
                OnPropertyChange(nameof(EmployeesList));
            }
        }

        private void LoadEmployeesFromEmployeeVM()
        {
            EmployeeVM employeeVM = new EmployeeVM();
            _employeesList = employeeVM.EmployeesList;
        }



        private ObservableCollection<Patient> _patientsList;

        public ObservableCollection<Patient> PatientsList
        {
            get { return _patientsList; }
            set
            {
                _patientsList = value;
                OnPropertyChange(nameof(PatientsList));
            }
        }

        private void LoadPatientsFromPatientVM()
        {
            PatientVM patientVM = new PatientVM();
            _patientsList = patientVM.PatientsList;
        }

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public EditContactVM(Contact contact)
        {
            EditableContact = contact;
            SaveCommand = new AsyncRelayCommand(async (o) => await SaveActionAsync());
            CancelCommand = new RelayCommand(CancelAction);

            LoadEmployeesFromEmployeeVM();
            LoadPatientsFromPatientVM();
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
