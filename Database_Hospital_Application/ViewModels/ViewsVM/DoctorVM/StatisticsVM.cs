using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.Views.Doctor.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM
{
    public class StatisticsVM : BaseViewModel
    {
        private UserControl _currentView;
        public UserControl CurrentView
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

        public ICommand NumberOfEmployeesOnDepartmentsCommand { get; private set; }

        private async void NumberOfEmployeesOnDepartments(object obj) 
        {
            NumberOfEmployeesOnDepartmentsVM viewModel = new NumberOfEmployeesOnDepartmentsVM();
            var numberOfEmployeesOnDepartmentsView = new NumberOfEmployeesOnDepartments()
            {
                DataContext = viewModel
            };
            CurrentView = numberOfEmployeesOnDepartmentsView; 

        }

        public StatisticsVM()
        {
            NumberOfEmployeesOnDepartmentsCommand = new RelayCommand(NumberOfEmployeesOnDepartments);
        }
    }
}
