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
            DepartmentView = CollectionViewSource.GetDefaultView(SubordinatesList);
            DepartmentView.Filter = SubordinatesFilter;
        }

        private string _searchText;
        public ICollectionView DepartmentView { get; private set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChange(nameof(SearchText));
                DepartmentView.Refresh();
            }
        }

        private bool SubordinatesFilter(object item)
        {
            if (string.IsNullOrWhiteSpace(_searchText)) return true;

            var subordinate = item as string;
            if (subordinate == null) return false;


            return subordinate.Contains(_searchText, StringComparison.OrdinalIgnoreCase);
        }
    }
}
