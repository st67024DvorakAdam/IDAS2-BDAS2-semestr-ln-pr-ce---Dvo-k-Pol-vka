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
using System.Collections.ObjectModel;

namespace Database_Hospital_Application.ViewModels.Dialogs.Edit
{
    public class EditDrugVM : BaseViewModel
    {
        private Drug _editableDrug;
        public Drug EditableDrug
        {
            get { return _editableDrug; }
            set { _editableDrug = value; OnPropertyChange(nameof(EditableDrug)); }
        }


        private ObservableCollection<Employee> _doctorsList;
        public ObservableCollection<Employee> DoctorsList
        {
            get { return _doctorsList; }
            set
            {
                _doctorsList = value;
                OnPropertyChange(nameof(DoctorsList));
            }
        }

        private async Task LoadDoctorsAsync()
        {
            EmployeesRepo repo = new EmployeesRepo();
            DoctorsList = await repo.GetAllEmployeesAsync();
            DoctorsList = new ObservableCollection<Employee>(
            DoctorsList.Where(emp => emp.RoleID == 2 || emp._role.Id == 2));
        }


        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public EditDrugVM(Drug drug)
        {
            EditableDrug = drug;
            SaveCommand = new AsyncRelayCommand(async (o) => await SaveActionAsync());
            CancelCommand = new RelayCommand(CancelAction);

            LoadDoctorsAsync();
        }
        private bool CanSaveExecute(object parameter)
        {

            return EditableDrug != null;
        }

        private async Task SaveActionAsync()
        {
            if (EditableDrug.Dosage == 0)
            {
                MessageBox.Show("Dávkování léku nesmí být 0!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                DrugsRepo drugsRepo = new DrugsRepo();
                int affectedRows = await drugsRepo.UpdateDrug(EditableDrug);
                if (affectedRows == 0)
                {
                    MessageBox.Show("Lék se nepodařilo změnit", "Not Ok", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Lék byl úspšně aktualizován", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
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
