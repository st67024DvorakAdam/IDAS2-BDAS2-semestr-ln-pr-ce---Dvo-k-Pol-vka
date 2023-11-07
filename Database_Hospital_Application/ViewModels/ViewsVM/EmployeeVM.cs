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
    public class EmployeeVM : BaseViewModel
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


        public EmployeeVM()
        {
            LoadEmployeesAsync();
        }
        private async Task LoadEmployeesAsync()
        {
            EmployeesRepo repo = new EmployeesRepo();
            EmployeesList = await repo.GetAllEmployeesAsync();
        }
    }
}
