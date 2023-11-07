using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public DepartmentVM()
        {
            LoadDepartmentsAsync();
        }

        private async Task LoadDepartmentsAsync()
        {
            DepartmentRepo repo = new DepartmentRepo();
            DepartmentsList = await repo.GetAllDepartmentsAsync();
        }
    }
}
