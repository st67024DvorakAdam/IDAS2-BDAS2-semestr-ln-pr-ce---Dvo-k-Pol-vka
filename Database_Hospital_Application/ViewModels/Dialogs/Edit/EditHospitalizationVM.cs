using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.ViewModels.ViewsVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace Database_Hospital_Application.ViewModels.Dialogs.Edit
{
    public class EditHospitalizationVM :BaseViewModel
    {
        private Hospitalization _editableHospitalization;
        public Hospitalization EditableHospitalization
        {
            get { return _editableHospitalization; }
            set { _editableHospitalization = value; OnPropertyChange(nameof(EditableHospitalization)); }
        }


        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public EditHospitalizationVM(Hospitalization hospitalization)
        {
            EditableHospitalization = hospitalization;
            SaveCommand = new AsyncRelayCommand(async (o) => await SaveActionAsync());
            CancelCommand = new RelayCommand(CancelAction);

            LoadDepartments();
            LoadPatients();
        }
        private bool CanSaveExecute(object parameter)
        {

            return EditableHospitalization != null;
        }

        private async Task SaveActionAsync()
        {
            try
            {
                HospitalizationRepo repo = new HospitalizationRepo();
                int affectedRows = await repo.UpdateHospitalization(EditableHospitalization);
                if (affectedRows == 0)
                {
                    MessageBox.Show("Hospitalizaci se nepodařilo změnit", "Not Ok", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    MessageBox.Show("Hospitalizace byla úspšně aktualizován", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
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


        //Listy do comboboxů
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


        private ObservableCollection<Department> _departmentList;
        public ObservableCollection<Department> DepartmentList
        {
            get { return _departmentList; }
            set
            {
                _departmentList = value;
                OnPropertyChange(nameof(DepartmentList));
            }
        }



        private void LoadDepartments()
        {
            DepartmentVM d = new DepartmentVM();
            _departmentList = d.DepartmentsList;
        }


        private void LoadPatients()
        {
            PatientVM e = new PatientVM();
            _patientsList = e.PatientsList;
        }
    }
}
