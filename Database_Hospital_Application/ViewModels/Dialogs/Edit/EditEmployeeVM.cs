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
using Database_Hospital_Application.ViewModels.ViewsVM;
using System.ComponentModel;

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


        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public EditEmployeeVM(Employee employee)
        {
            EditableEmployee = employee;
            SaveCommand = new AsyncRelayCommand(async (o) => await SaveActionAsync());
            CancelCommand = new RelayCommand(CancelAction);

            LoadPhotosFromPhotoVM();
            LoadDepartmentsFromDepartmentVM();
            LoadRolesFromRoleVM();
            LoadEmployeesFromEmployeeVM();
        }
        private bool CanSaveExecute(object parameter)
        {

            return EditableEmployee != null;
        }

        private async Task SaveActionAsync()
        {
            if (!isNewEmployeeFilled(EditableEmployee))
            {
                MessageBox.Show("Vyplňte všechna pole!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (EditableEmployee.BirthNumber.Length < 10)
            {
                MessageBox.Show("Rodné číslo má méně než 10 znaků!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                EmployeesRepo employeesRepo = new EmployeesRepo();
                int affectedRows = await employeesRepo.UpdateEmployee(EditableEmployee, isSelected);
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


        //Listy do comboboxů
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

        private ObservableCollection<Foto> _photosList;
        public ObservableCollection<Foto> PhotosList
        {
            get { return _photosList; }
            set
            {
                _photosList = value;
                OnPropertyChange(nameof(PhotosList));
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

        private ObservableCollection<Role> _roleList;
        public ObservableCollection<Role> RoleList
        {
            get { return _roleList; }
            set
            {
                _roleList = value;
                OnPropertyChange(nameof(RoleList));
            }
        }

        private void LoadPhotosFromPhotoVM()
        {
            PhotoVM p = new PhotoVM();
            _photosList = p.PhotosList;
        }

        private void LoadDepartmentsFromDepartmentVM()
        {
            DepartmentVM d = new DepartmentVM();
            _departmentList = d.DepartmentsList;
        }

        private void LoadRolesFromRoleVM()
        {
            RolesVM r = new RolesVM();
            _roleList = r.RolesList;
        }

        private void LoadEmployeesFromEmployeeVM()
        {
            EmployeeVM e = new EmployeeVM();
            _employeesList = e.EmployeesList;
        }
        private bool isNewEmployeeFilled(Employee e)
        {
            return e != null
                && !string.IsNullOrEmpty(e.FirstName) && !string.IsNullOrWhiteSpace(e.FirstName)
                && !string.IsNullOrEmpty(e.LastName) && !string.IsNullOrWhiteSpace(e.LastName)
                && !string.IsNullOrEmpty(e.UserName) && !string.IsNullOrWhiteSpace(e.UserName)
                && !string.IsNullOrEmpty(e.BirthNumber) && !string.IsNullOrWhiteSpace(e.BirthNumber)
                && e._department != null && e._department.Id != null
                && !string.IsNullOrEmpty(e.Password) && !string.IsNullOrWhiteSpace(e.Password)
                && !string.IsNullOrEmpty(e.RoleID.ToString()) && !string.IsNullOrWhiteSpace(e.RoleID.ToString());
        }
    }
}
