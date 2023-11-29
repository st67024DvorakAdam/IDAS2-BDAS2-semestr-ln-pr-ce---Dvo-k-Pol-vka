using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.Models.Repositories.GuestRepo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Database_Hospital_Application.ViewModels.ViewsVM.GuestVM
{
    public class EmployeesOnDepartmentsVM :BaseViewModel
    {
        private ObservableCollection<string> _employeesOnDepartmentsList;
        public ObservableCollection<string> EmployeesOnDepartmentsList
        {
            get { return _employeesOnDepartmentsList; }
            set
            {
                _employeesOnDepartmentsList = value;
                OnPropertyChange(nameof(EmployeesOnDepartmentsList));
            }
        }


        public EmployeesOnDepartmentsVM()
        {
            LoadEmployeesOnDepartmentsAsync();
            EmployeesOnDepartmentsView = CollectionViewSource.GetDefaultView(EmployeesOnDepartmentsList);
            EmployeesOnDepartmentsView.Filter = EmployeesOnDepartmentsFilter;
        }

        private async Task LoadEmployeesOnDepartmentsAsync()
        {
            EmployeesOnDepartmentsRepo repo = new EmployeesOnDepartmentsRepo();
            EmployeesOnDepartmentsList = await repo.GetListOfEmployeesOnDepartments();
        }

        private string _searchText;
        public ICollectionView EmployeesOnDepartmentsView { get; private set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChange(nameof(SearchText));
                EmployeesOnDepartmentsView.Refresh();
            }
        }

        private bool EmployeesOnDepartmentsFilter(object item)
        {
            if (string.IsNullOrWhiteSpace(_searchText)) return true;

            var s = item as string;
            if (s == null) return false;


            return  s.Contains(_searchText, StringComparison.OrdinalIgnoreCase);
        }
    }
}

