using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Database_Hospital_Application.Commands;
using Database_Hospital_Application.ViewModels.Dialogs.Edit;
using Database_Hospital_Application.Views.Lists.Dialogs.Contact;
using Database_Hospital_Application.Views.Lists.Dialogs.Department;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class DepartmentVM : BaseViewModel
    {
        private ObservableCollection<Department> _departmentsList;
        public ObservableCollection<Department> DepartmentsList
        {
            get { return _departmentsList; }
            set { _departmentsList = value; OnPropertyChange(nameof(DepartmentsList)); }
        }

        
        private Department _newDepartment = new Department();
        public Department NewDepartment
        {
            get { return _newDepartment; }
            set
            {
                _newDepartment = value;
                OnPropertyChange(nameof(NewDepartment));
            }
        }

        
        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand EditCommand { get; private set; }

        
        private Department _selectedDepartment;
        public Department SelectedDepartment
        {
            get
            {
                return _selectedDepartment;
                    }
            set
            {
                    if (_selectedDepartment != value)
                    {
                        _selectedDepartment = value;
                        OnPropertyChange(nameof(SelectedDepartment));
                        (EditCommand as RelayCommand)?.RaiseCanExecuteChanged();
                        (DeleteCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    }
                }
            }

        // Konstruktor
        public DepartmentVM()
        {
            LoadDepartmentsAsync();
            InitializeCommands();
        }

        private async Task LoadDepartmentsAsync()
        {
            DepartmentRepo repo = new DepartmentRepo();
            DepartmentsList = await repo.GetAllDepartmentsAsync();
            if (DepartmentsList != null)
            {
                DepartmentsView = CollectionViewSource.GetDefaultView(DepartmentsList);
                DepartmentsView.Filter = DepartmentsFilter;
            }
        }

        private void InitializeCommands()
        {
            AddCommand = new RelayCommand(AddNewDepartmentAction);
            DeleteCommand = new RelayCommand(DeleteDepartmentAction, CanExecuteDelete);
            EditCommand = new RelayCommand(EditAction, CanEdit);
        }

        private bool CanExecuteDelete(object parameter)
        {
            return SelectedDepartment != null;
        }

        private async void DeleteDepartmentAction(object parameter)
        {
            if (SelectedDepartment == null) return;

            DepartmentRepo departmentRepo = new DepartmentRepo();
            await departmentRepo.DeleteDepartment(SelectedDepartment.Id);
            await LoadDepartmentsAsync();
        }

        private async void AddNewDepartmentAction(object parameter)
        {
            DepartmentRepo departmentRepo = new DepartmentRepo();
            await departmentRepo.AddDepartment(NewDepartment);
            await LoadDepartmentsAsync();
            NewDepartment = new Department();
        }

        private bool CanEdit(object parameter)
        {
            return SelectedDepartment != null;
        }

        private void EditAction(object parameter)
        {
            if (!CanEdit(parameter)) return;


            EditDepartmentVM editVM = new EditDepartmentVM(SelectedDepartment);
            EditDepartmentDialog editDialog = new EditDepartmentDialog(editVM);

            editDialog.ShowDialog();

            LoadDepartmentsAsync();

        }

        // Filter
        private string _searchText;
        public ICollectionView DepartmentsView { get; private set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChange(nameof(SearchText));
                DepartmentsView.Refresh();
            }
        }

        private bool DepartmentsFilter(object item)
        {
            if (string.IsNullOrWhiteSpace(_searchText)) return true;

            var department = item as Department;
            return department != null &&
                   (department.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                    || department.Id.ToString().Contains(_searchText));
        }
    }
}
