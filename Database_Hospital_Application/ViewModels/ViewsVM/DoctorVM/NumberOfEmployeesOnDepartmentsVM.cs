using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.Views.Doctor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM
{
    public class NumberOfEmployeesOnDepartmentsVM : BaseViewModel
    {
        private string _numberOfEmployeesOnDepartmentsList;
        public string NumberOfEmployeesOnDepartmentsList
        {
            get { return _numberOfEmployeesOnDepartmentsList; }
            set
            {
                _numberOfEmployeesOnDepartmentsList = value;
                OnPropertyChange(nameof(NumberOfEmployeesOnDepartmentsList));
            }
        }


        public NumberOfEmployeesOnDepartmentsVM()
        {
            LoadNumberOfEmployeesOnDepartmentsAsync();
        }

        public async Task LoadNumberOfEmployeesOnDepartmentsAsync()
        {
            DepartmentRepo repo = new DepartmentRepo();
            NumberOfEmployeesOnDepartmentsList = await repo.GetNumberOfEmployeesOnDepartments();
        }
    }
}
