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
    public class EditDepartmentVM : BaseViewModel
    {
        private Department _editableDepartment;
        public Department EditableDepartment
        {
            get { return _editableDepartment; }
            set { _editableDepartment = value; OnPropertyChange(nameof(EditableDepartment)); }
        }


        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public EditDepartmentVM(Department department)
        {
            EditableDepartment = department;
            SaveCommand = new AsyncRelayCommand(async (o) => await SaveActionAsync());
            CancelCommand = new RelayCommand(CancelAction);
        }
        private bool CanSaveExecute(object parameter)
        {

            return EditableDepartment != null;
        }

        private async Task SaveActionAsync()
        {
            try
            {
                DepartmentRepo departmentRepo = new DepartmentRepo();
                int affectedRows = await departmentRepo.UpdateDepartment(EditableDepartment);
                if (affectedRows == 0)
                {
                    MessageBox.Show("Oddělení se nepodařilo změnit", "Not Ok", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    MessageBox.Show("Oddělení bylo úspšně aktualizován", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
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
