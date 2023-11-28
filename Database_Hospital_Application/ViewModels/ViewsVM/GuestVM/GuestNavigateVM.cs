using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.ViewsVM.GuestVM
{
    public class GuestNavigateVM : BaseViewModel
    {
        private object _currentView;

        public object CurrentView
        {
            get => _currentView;
            set
            {
                if (_currentView != value)
                {
                    _currentView = value;
                    OnPropertyChange(nameof(CurrentView));
                }
            }
        }


        public ICommand EmployeesOnDepartmentsCommand { get; }

        private void EmployeesOnDepartments(object obj) => CurrentView = new EmployeesOnDepartmentsVM();


        public GuestNavigateVM()
        {
            //defaultní pohled
            CurrentView = new EmployeesOnDepartmentsVM();


            //Inicializace příkazů
            EmployeesOnDepartmentsCommand = new RelayCommand(EmployeesOnDepartments);


        }
    }
}
