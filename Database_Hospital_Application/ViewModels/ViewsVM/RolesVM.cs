using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Enums;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.Models.Tools;
using Database_Hospital_Application.ViewModels.Dialogs.Edit;
using Database_Hospital_Application.Views.Lists.Dialogs.Employee;
using Database_Hospital_Application.Views.Lists.Dialogs.Role;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class RolesVM : BaseViewModel
    {
        public ObservableCollection<Role> _rolesList;

        public ObservableCollection<Role> RolesList
        {
            get { return _rolesList; }
            set { _rolesList = value; OnPropertyChange(nameof(RolesList)); }
        }


        // BUTTONS
        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand EditCommand { get; private set; }

        private Role _selectedRole;
        public Role SelectedRole
        {
            get { return _selectedRole; }
            set
            {
                if (_selectedRole != value)
                {
                    _selectedRole = value;
                    OnPropertyChange(nameof(SelectedRole));
                    (EditCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    (DeleteCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        private Role _newRole = new Role();
        public Role NewRole
        {
            get { return _newRole; }
            set
            {
                _newRole = value;
                OnPropertyChange(nameof(NewRole));
            }
        }


        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////
        public RolesVM()
        {
            LoadRolesAsync();
            RolesView = CollectionViewSource.GetDefaultView(RolesList);
            RolesView.Filter = RolesFilter;
            InitializeCommands();
        }

        private async Task LoadRolesAsync()
        {
            RolesRepo repo = new RolesRepo();
            RolesList = await repo.GetAllRoleDescriptionsAsync();
        }

        private void InitializeCommands()
        {
            AddCommand = new RelayCommand(AddNewAction);
            DeleteCommand = new RelayCommand(DeleteAction, CanExecuteDelete);
            EditCommand = new RelayCommand(EditAction, CanEdit);
        }

        private bool CanExecuteDelete(object parameter)
        {
            return SelectedRole != null;
        }

        private async void DeleteAction(object parameter)
        {
            try
            {
                if (SelectedRole == null) return;

                RolesRepo roleRepo = new RolesRepo();
                await roleRepo.DeleteRole(SelectedRole.Id);
                await LoadRolesAsync();
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Nelze mazat základní role!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private async void AddNewAction(object parameter)
        {
            
                RolesRepo roleRepo = new RolesRepo();
                await roleRepo.AddRole(NewRole);
                await LoadRolesAsync();
                NewRole = new Role();
            
        }

        private bool CanEdit(object parameter)
        {
            return SelectedRole != null;
        }

        private void EditAction(object parameter)
        {
            if(SelectedRole.Id <= 5)
            {
                MessageBox.Show("Nelze upravovat základní role!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (!CanEdit(parameter)) return;

                EditRoleVM editVM = new EditRoleVM(SelectedRole);
                EditRoleDialog editDialog = new EditRoleDialog(editVM);

                editDialog.ShowDialog();

                if (editDialog.DialogResult == true)
                {
                    LoadRolesAsync();
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Nelze upravovat základní role!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //FILTER/////////////////////////////////////////////////////////////////////

        private string _searchText;
        public ICollectionView RolesView { get; private set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChange(nameof(SearchText));
                RolesView.Refresh();
            }
        }

        private bool RolesFilter(object item)
        {

            if (string.IsNullOrWhiteSpace(_searchText)) return true;

            var role = item as Role;
            if (role == null) return false;

            return role.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
            || role.Id.ToString().Contains(_searchText);
        }
        //FILTER/////////////////////////////////////////////////////////////////////
    }
}
