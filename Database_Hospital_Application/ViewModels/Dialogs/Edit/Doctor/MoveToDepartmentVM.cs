using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.Dialogs.Edit.Doctor
{
    public class MoveToDepartmentVM : BaseViewModel
    {
        public event Action CloseRequested;
        public ICommand MoveCommand { get; }
        public ICommand CancelCommand { get; }

        private DepartmentRepo departmentRepo;
        public ObservableCollection<Department> DepartmentList { get; private set; }

        private Hospitalization _hospitalization;
        public Hospitalization Hospitaliazation
        {
            get => _hospitalization;
            set
            {
                _hospitalization = value;
                OnPropertyChange(nameof(Hospitaliazation));
            }
        }

        private Department _selectedDepartment;
        public Department SelectedDepartment
        {
            get => _selectedDepartment;
            set
            {
                _selectedDepartment = value;
                OnPropertyChange(nameof(SelectedDepartment));
            }
        }

        

        public MoveToDepartmentVM(Hospitalization currentHospitalization)
        {
            _hospitalization = currentHospitalization;
            departmentRepo = new DepartmentRepo();
            DepartmentList = new ObservableCollection<Department>();
            LoadDepartmentsAsync();
            MoveCommand = new RelayCommand(_ => MoveToDepartment());
            CancelCommand = new RelayCommand(_ => Cancel());
        }

        private async Task LoadDepartmentsAsync()
        {
            var departments = await departmentRepo.GetAllDepartmentsAsync();
            DepartmentList.Clear();
            foreach (var dept in departments)
            {
                DepartmentList.Add(dept);
            }
        }

        private async void MoveToDepartment()
        {
            if (_selectedDepartment == null)
            {
                MessageBox.Show("Vyberte nové oddělení!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (_hospitalization.DepartmentId == _selectedDepartment.Id)
            {
                MessageBox.Show("Pacienta nelze přemístit na stejné oddělení", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            HospitalizationRepo hospitalizationRepo = new HospitalizationRepo();
            _hospitalization.DateOut = DateTime.Now;
            hospitalizationRepo.UpdateHospitalization(_hospitalization);
            _hospitalization.DepartmentId = _selectedDepartment.Id;
            _hospitalization.DateIn = DateTime.Now;
            _hospitalization.DateOut = null;
            
            hospitalizationRepo.AddHospitalization(_hospitalization);
            CloseRequested?.Invoke();
        }
        private void Cancel()
        {
            CloseRequested?.Invoke();
        }
    }
}
