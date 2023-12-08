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
    public class EditPersonalMedicalHistoryVM : BaseViewModel
    {
        private PersonalMedicalHistory _editablePersonalMedicalHistory;
        public PersonalMedicalHistory EditablePersonalMedicalHistory
        {
            get { return _editablePersonalMedicalHistory; }
            set { _editablePersonalMedicalHistory = value; OnPropertyChange(nameof(EditablePersonalMedicalHistory)); }
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

        public EditPersonalMedicalHistoryVM(PersonalMedicalHistory personalMedicalHistory)
        {
            EditablePersonalMedicalHistory = personalMedicalHistory;
            SaveCommand = new AsyncRelayCommand(async (o) => await SaveActionAsync());
            CancelCommand = new RelayCommand(CancelAction);

            LoadPatientsFromPatientVM();
        }
        private bool CanSaveExecute(object parameter)
        {

            return EditablePersonalMedicalHistory != null;
        }

        private async Task SaveActionAsync()
        {
            if (!PersonalMedicalHistoryValidator.IsDescriptionAndPatientFilled(EditablePersonalMedicalHistory))
            {
                MessageBox.Show("Vyplňte všechna pole!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                PersonalMedicalHistoriesRepo personalMedicalHistoriesRepo = new PersonalMedicalHistoriesRepo();
                int affectedRows = await personalMedicalHistoriesRepo.UpdatePersonalMedicalHistory(EditablePersonalMedicalHistory);
                if (affectedRows == 0)
                {
                    MessageBox.Show("osobní anamnézu se nepodařilo změnit", "Not Ok", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    MessageBox.Show("osobní anamnéza byla úspšně aktualizována", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
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
