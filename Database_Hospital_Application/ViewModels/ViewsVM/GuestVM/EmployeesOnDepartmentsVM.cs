using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.Models.Repositories.GuestRepo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        private async Task LoadEmployeesOnDepartmentsAsync()
        {
            EmployeesOnDepartmentsRepo repo = new EmployeesOnDepartmentsRepo();
            EmployeesOnDepartmentsList = await repo.GetListOfEmployeesOnDepartments();
        }
    }
}

