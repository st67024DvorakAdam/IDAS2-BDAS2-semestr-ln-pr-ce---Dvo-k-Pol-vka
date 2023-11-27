using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.Models.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class AddNewUserEasyWayVM : BaseViewModel
    {
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

        public ICommand AddCommand { get; private set; }
        public ICommand UploadPhotoCommand { get; private set; }

        private Employee _newEmployee = new Employee();
        public Employee NewEmployee
        {
            get { return _newEmployee; }
            set
            {
                _newEmployee = value;
                OnPropertyChange(nameof(NewEmployee));
            }
        }

        private Contact _newContact = new Contact();
        public Contact NewContact
        {
            get { return _newContact; }
            set
            {
                _newContact = value;
                OnPropertyChange(nameof(NewContact));
            }
        }

        private Foto _newFoto = new Foto();
        public Foto NewFoto
        {
            get { return _newFoto; }
            set
            {
                _newFoto = value;
                OnPropertyChange(nameof(NewFoto));
            }
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
            if (IsSelected == false) LoadBasicFoto();
        }

        public AddNewUserEasyWayVM()
        {
            LoadBasicFoto();
            LoadEmployeesAsync();
            LoadDepartmentsFromDepartmentVM();
            LoadRolesFromRoleVM();
            InitializeCommands();
        }

        public async void LoadBasicFoto()
        {
            PhotosRepo photosRepo = new PhotosRepo();
            NewFoto = await photosRepo.GetBasicPhotoAsync();
            OnPropertyChange(nameof(NewFoto));
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

        private async Task LoadEmployeesAsync()
        {
            EmployeesRepo repo = new EmployeesRepo();
            EmployeesList = await repo.GetAllEmployeesAsync();
        }

        private void InitializeCommands()
        {
            AddCommand = new RelayCommand(AddNewAction);
            UploadPhotoCommand = new RelayCommand(UploadPhoto);

        }

        private void UploadPhoto(object? obj)
        {
            OpenFileDialogService fileDialogService =  new OpenFileDialogService();
            try
            {
                string filename = string.Empty;
                string suffix = string.Empty;
                var selectedFilePath = fileDialogService.OpenFileDialog(out filename, out suffix);
                byte[] imageBytes = File.ReadAllBytes(selectedFilePath);
                BitmapImage bmimg = FotoExtension.ConvertBytesToBitmapImage(imageBytes);
                if (bmimg != null) NewFoto.Image = bmimg;
                NewFoto.Name = filename;
                NewFoto.Suffix = suffix;
                OnPropertyChange(nameof(NewFoto));
            }
            catch (Exception ex)
            {

            }
        }

        private async void AddNewAction(object? obj)
        {

            NewEmployee.Salt = PasswordHasher.GenerateSalt();
            NewEmployee.Password = PasswordHasher.HashPassword(NewEmployee.Password, NewEmployee.Salt);
            AddNewUserEasyWayRepo repo = new AddNewUserEasyWayRepo();
            if (IsSelected) repo.AddEmployee(1,NewEmployee, NewFoto, NewContact);
            else repo.AddEmployee(0, NewEmployee, NewFoto, NewContact);
            NewEmployee = new Employee();
            NewFoto = new Foto();
            NewContact = new Contact();
            //IsSelected = false;
            LoadBasicFoto();
        }
    }
}
