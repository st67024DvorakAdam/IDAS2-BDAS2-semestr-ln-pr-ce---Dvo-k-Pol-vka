using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Enums;
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

        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////
        public EmployeeVM()
        {
            LoadEmployeesAsync();
            EmployeesView = CollectionViewSource.GetDefaultView(EmployeesList);
            EmployeesView.Filter = EmployeesFilter;
        }
        private async Task LoadEmployeesAsync()
        {
            EmployeesRepo repo = new EmployeesRepo();
            EmployeesList = await repo.GetAllEmployeesAsync();
        }
        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////

        //FILTER/////////////////////////////////////////////////////////////////////

        private string _searchText;
        public ICollectionView EmployeesView { get; private set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChange(nameof(SearchText));
                EmployeesView.Refresh();
            }
        }

        private bool EmployeesFilter(object item)
        {

            if (string.IsNullOrWhiteSpace(_searchText)) return true;

            var employee = item as Employee;
            if (employee == null) return false;

            return employee.FirstName.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                || employee.LastName.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                || employee.Id.ToString().Contains(_searchText)
                || employee.BirthNumber.ToString().Contains(_searchText)
                || SexEnumParser.GetStringFromEnumEnglish(employee.Sex).StartsWith(_searchText, StringComparison.OrdinalIgnoreCase)
                || EmployeeTypeExtensions.GetStringFormEnumEnglish(employee._employeeType).Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                || employee.IdOfSuperiorEmployee.ToString().Contains(_searchText)
                || employee._foto.Id.ToString().Contains(_searchText);
        }
        //FILTER/////////////////////////////////////////////////////////////////////
    }
}
