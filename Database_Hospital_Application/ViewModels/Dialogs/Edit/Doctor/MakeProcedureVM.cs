using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.Dialogs.Edit.Doctor
{
    public class MakeProcedureVM : BaseViewModel
    {
        public event Action CloseRequested;

        public ICommand AddCommand { get; }
        public ICommand CancelCommand { get; }

        private PerformedProcedure _newPerformedProcedure = new PerformedProcedure();
        public PerformedProcedure NewPerformedProcedure
        {
            get => _newPerformedProcedure;
            set
            {
                _newPerformedProcedure = value;
                OnPropertyChange(nameof(NewPerformedProcedure));
            }
        }

        private ObservableCollection<Department> _departmentList;
        public ObservableCollection<Department> DepartmentList
        {
            get => _departmentList;
            set
            {
                _departmentList = value;
                OnPropertyChange(nameof(DepartmentList));
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

        private readonly PerformedProceduresRepo _proceduresRepo;
        private readonly DepartmentRepo _departmentRepo;

        public MakeProcedureVM()
        {
            AddCommand = new RelayCommand(_ => AddProcedureAndHospitalize());
            CancelCommand = new RelayCommand(_ => Cancel());

            _proceduresRepo = new PerformedProceduresRepo();
            _departmentRepo = new DepartmentRepo();

            LoadDepartments();
        }

        private void LoadDepartments()
        {
            
            DepartmentList = _departmentRepo.GetAllDepartmentsAsync().Result;
        }

        private void AddProcedureAndHospitalize()
        {
            
            _proceduresRepo.AddPerformedProcedure(NewPerformedProcedure);

            

            CloseRequested?.Invoke();
        }

        private void Cancel()
        {
            CloseRequested?.Invoke();
        }
    }
}

