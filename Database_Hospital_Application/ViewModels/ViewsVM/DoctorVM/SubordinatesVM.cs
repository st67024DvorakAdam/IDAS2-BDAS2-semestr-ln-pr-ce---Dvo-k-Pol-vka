using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM
{
    public class SubordinatesVM: BaseViewModel
    {
        private Employee _employee;
        private ObservableCollection<string> _subordinatesList;
        public ObservableCollection<string> SubordinatesList
        {
            get { return _subordinatesList; }
            set
            {
                _subordinatesList = value;
                OnPropertyChange(nameof(SubordinatesList));
            }
        }


        public SubordinatesVM(User user)
        {
            _employee = user.Employee;
            LoadSubordinatesAsync();
        }

        private async Task LoadSubordinatesAsync()
        {
            EmployeesRepo repo = new EmployeesRepo();
            SubordinatesList = await repo.GetListOfSubordinates(_employee.Id);
        }
    }
}
