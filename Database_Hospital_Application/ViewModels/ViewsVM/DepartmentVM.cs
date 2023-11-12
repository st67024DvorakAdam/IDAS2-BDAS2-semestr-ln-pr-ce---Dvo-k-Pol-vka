using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class DepartmentVM :BaseViewModel
    {
        public ObservableCollection<Department> _departmentsList;

        public ObservableCollection<Department> DepartmentsList
        {
            get { return _departmentsList; }
            set { _departmentsList = value; OnPropertyChange(nameof(DepartmentsList)); }
        }

        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////
        public DepartmentVM()
        {
            LoadDepartmentsAsync();
            DepartmentsView = CollectionViewSource.GetDefaultView(DepartmentsList);
            DepartmentsView.Filter = DepartmentsFilter;
        }

        private async Task LoadDepartmentsAsync()
        {
            DepartmentRepo repo = new DepartmentRepo();
            DepartmentsList = await repo.GetAllDepartmentsAsync();
        }

        //FILTER/////////////////////////////////////////////////////////////////////

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
            if (department == null) return false;

            return department.Id.ToString().Contains(_searchText)
                || department.IdOfHeadOfDepartment.ToString().Contains(_searchText)
                || department.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase);
        }
        //FILTER/////////////////////////////////////////////////////////////////////
    }
}
