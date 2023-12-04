using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.Dialogs.Edit.Doctor
{
    public class HospitalizeCurrentPatientVM : BaseViewModel
    {
        public event Action CloseRequested;
        public ICommand HospitalizeCommand { get; }
        public ICommand CancelCommand { get; }

        private DepartmentRepo departmentRepo;
        public ObservableCollection<Department> DepartmentList { get; private set; }

        private Patient _patient;
        public Patient Patient
        {
            get => _patient;
            set
            {
                _patient = value;
                OnPropertyChange(nameof(Patient));
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

        private string _details;
        public string Details
        {
            get => _details;
            set
            {
                _details = value;
                OnPropertyChange(nameof(Details));
            }
        }

        public HospitalizeCurrentPatientVM(Patient currentPatient)
        {
            _patient = currentPatient;
            departmentRepo = new DepartmentRepo();
            DepartmentList = new ObservableCollection<Department>();
            LoadDepartmentsAsync();
            HospitalizeCommand = new RelayCommand(_ => Hospitalize(), _ => _details != null && _selectedDepartment != null);
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

        private async void Hospitalize()
        {
            HospitalizationRepo hospitalizationRepo = new HospitalizationRepo();
            hospitalizationRepo.AddHospitalization(new Hospitalization(DateTime.Now, _details, _patient.Id, _selectedDepartment.Id));
            CloseRequested?.Invoke();
        }
        private void Cancel()
        {
            CloseRequested?.Invoke();
        }
    }
}
