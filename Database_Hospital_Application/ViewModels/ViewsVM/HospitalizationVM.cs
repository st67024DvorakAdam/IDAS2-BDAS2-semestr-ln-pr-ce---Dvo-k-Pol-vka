using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.ViewModels.Dialogs.Edit;
using Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM.PatientViewVM;
using Database_Hospital_Application.Views.Lists.Dialogs.Hospitalization;
using Database_Hospital_Application.Views.Lists.Dialogs.Illness;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class HospitalizationVM : BaseViewModel
    {
        private ObservableCollection<Hospitalization> _hospitalizationsList;

        public ObservableCollection<Hospitalization> HospitalizationsList
        {
            get { return _hospitalizationsList; }
            set
            {
                _hospitalizationsList = value;
                OnPropertyChange(nameof(HospitalizationsList));
            }
        }

        private ObservableCollection<Hospitalization> _hospitalizationsListForSpecificallyEmployee;
        public ObservableCollection<Hospitalization> HospitalizationsListForSpecificallyEmployee
        {
            get { return _hospitalizationsListForSpecificallyEmployee; }
            set
            {
                _hospitalizationsListForSpecificallyEmployee = value;
                OnPropertyChange(nameof(HospitalizationsListForSpecificallyEmployee));
            }
        }

        private ObservableCollection<Patient> _patientsList;

        public ObservableCollection<Patient> PatientsList
        {
            get { return _patientsList; }
            set
            {
                _patientsList = value;
                OnPropertyChange(nameof(PatientsList));
            }
        }

        private ObservableCollection<Department> _departmentsList;

        public ObservableCollection<Department> DepartmentsList
        {
            get { return _departmentsList; }
            set
            {
                _departmentsList = value;
                OnPropertyChange(nameof(DepartmentsList));
            }
        }

        // BUTTONS
        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand EditCommand { get; private set; }

        private Hospitalization _selectedHospitalization;
        public Hospitalization SelectedHospitalization
        {
            get { return _selectedHospitalization; }
            set
            {
                if (_selectedHospitalization != value)
                {
                    _selectedHospitalization = value;
                    OnPropertyChange(nameof(SelectedHospitalization));
                    (EditCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    (DeleteCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        private Hospitalization _newHospitalization = new Hospitalization();
        public Hospitalization NewHospitalization
        {
            get { return _newHospitalization; }
            set
            {
                _newHospitalization = value;
                OnPropertyChange(nameof(NewHospitalization));
            }
        }

        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////

        public HospitalizationVM()
        {
            NewHospitalization.DateIn = DateTime.Today;
            LoadHospitalizationsAsync();

            LoadPatientsAsync();
            LoadDepartmentsAsync();
            InitializeCommands();
        }

        //konstruktor který je využíván pro načtení hospitalizací např u sestry na jejím oddělení
        public HospitalizationVM(Employee employee)
        {
            NewHospitalization.DateIn = DateTime.Today;
            LoadHospitalizationsAsync();

            HospitalizationsListForSpecificallyEmployee = new ObservableCollection<Hospitalization>();
            foreach (var hospitalization in HospitalizationsList)
            {
                if (hospitalization.DepartmentId == employee._department.Id)
                {
                    HospitalizationsListForSpecificallyEmployee.Add(hospitalization);
                }
            }

            LoadPatientsAsync();
            LoadDepartmentsAsync();
            InitializeCommands();
        }
        private async Task LoadHospitalizationsAsync()
        {
            HospitalizationRepo repo = new HospitalizationRepo();
            HospitalizationsList = await repo.GetAllHospitalizationsAsync();
            HospitalizationView = CollectionViewSource.GetDefaultView(HospitalizationsList);
            HospitalizationView.Filter = HospitalizationFilter;
        }
        private async Task LoadPatientsAsync()
        {
            PatientRepo repo = new PatientRepo();
            PatientsList = await repo.GetAllPatientsAsync();
        }

        private async Task LoadDepartmentsAsync()
        {
            DepartmentRepo repo = new DepartmentRepo();
            DepartmentsList = await repo.GetAllDepartmentsAsync();
        }


        private void InitializeCommands()
        {
            AddCommand = new RelayCommand(AddNewHospitalizationAction);
            DeleteCommand = new RelayCommand(DeleteHospitalizationAction, CanExecuteDelete);
            EditCommand = new RelayCommand(EditAction, CanEdit);
        }

        private bool CanExecuteDelete(object parameter)
        {
            return SelectedHospitalization != null;
        }

        private async void DeleteHospitalizationAction(object parameter)
        {
            if (SelectedHospitalization == null) return;

            HospitalizationRepo repo = new HospitalizationRepo();
            await repo.DeleteHospitalization(SelectedHospitalization.Id);
            await LoadHospitalizationsAsync();
        }

        private async void AddNewHospitalizationAction(object parameter)
        {
            if (!ValidateHospitalization(NewHospitalization))
            {
                MessageBox.Show("Vyplňte všechna povinná pole!\n" +
                    "tj.: Datum nástupu, pacient a oddělení", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (NewHospitalization.DateOut != null && NewHospitalization.DateOut < NewHospitalization.DateIn) 
            {
                MessageBox.Show("Datum propuštění musí být kalendářně později než datum nástupu!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            HospitalizationRepo repo = new HospitalizationRepo();
            await repo.AddHospitalization(NewHospitalization);
            await LoadHospitalizationsAsync();
            HospitalizationView = CollectionViewSource.GetDefaultView(HospitalizationsList);
            HospitalizationView.Filter = HospitalizationFilter;
            NewHospitalization = new Hospitalization();
            NewHospitalization.DateIn = DateTime.Today;
        }

        private bool CanEdit(object parameter)
        {
            return SelectedHospitalization != null;
        }

        private void EditAction(object parameter)
        {
            if (!CanEdit(parameter)) return;


            if (!ValidateHospitalization(SelectedHospitalization))
            {
                MessageBox.Show("Vyplňte všechna povinná pole!\n" +
                    "tj.: Datum nástupu, pacient a oddělení", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (SelectedHospitalization.DateOut != null && SelectedHospitalization.DateOut < SelectedHospitalization.DateIn)
            {
                MessageBox.Show("Datum propuštění musí být kalendářně později než datum nástupu!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            EditHospitalizationVM editVM = new EditHospitalizationVM(SelectedHospitalization);
            EditHospitalizationDialog editDialog = new EditHospitalizationDialog(editVM);

            editDialog.ShowDialog();

            LoadHospitalizationsAsync();
            HospitalizationView = CollectionViewSource.GetDefaultView(HospitalizationsList);
            HospitalizationView.Filter = HospitalizationFilter;

        }

        //FILTER/////////////////////////////////////////////////////////////////////

        private string _searchText;
        public ICollectionView HospitalizationView { get; private set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChange(nameof(SearchText));
                HospitalizationView.Refresh();
            }
        }

        private bool HospitalizationFilter(object item)
        {

            if (string.IsNullOrWhiteSpace(_searchText)) return true;

            var hospitalization = item as Hospitalization;
            if (hospitalization == null) return false;

            return hospitalization.DateIn.ToString().Contains(_searchText)
                || hospitalization.DateOut.ToString().Contains(_searchText)
                || hospitalization.DepartmentId.ToString().Contains(_searchText)
                || hospitalization.PatientId.ToString().Contains(_searchText)
                || hospitalization.Details.Contains(_searchText, StringComparison.OrdinalIgnoreCase);
        }

        private bool ValidateHospitalization(Hospitalization hospitalization) 
        { 
            return hospitalization != null && hospitalization.DateIn != null && hospitalization.DepartmentId != null && hospitalization.PatientId != null;
        }
    }
}
