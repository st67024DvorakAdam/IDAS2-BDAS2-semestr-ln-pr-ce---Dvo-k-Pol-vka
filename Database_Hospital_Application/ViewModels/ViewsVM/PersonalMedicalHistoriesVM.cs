using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Enums;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.ViewModels.Dialogs.Edit;
using Database_Hospital_Application.Views.Lists.Dialogs.Contact;
using Database_Hospital_Application.Views.Lists.Dialogs.PersonalMedicalHistory;
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
    public class PersonalMedicalHistoriesVM : BaseViewModel
    {
        public ObservableCollection<PersonalMedicalHistory> _personalMedicalHistoriesList;

        public ObservableCollection<PersonalMedicalHistory> PersonalMedicalHistoriesList
        {
            get { return _personalMedicalHistoriesList; }
            set { _personalMedicalHistoriesList = value; OnPropertyChange(nameof(PersonalMedicalHistoriesList)); }
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

        private void LoadPatientsFromPatientVM()
        {
            PatientVM patientVM = new PatientVM();
            _patientsList = patientVM.PatientsList;
        }


        // BUTTONS
        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand EditCommand { get; private set; }

        private PersonalMedicalHistory _selectedPersonalMedicalHistory;
        public PersonalMedicalHistory SelectedPersonalMedicalHistory
        {
            get { return _selectedPersonalMedicalHistory; }
            set
            {
                if (_selectedPersonalMedicalHistory != value)
                {
                    _selectedPersonalMedicalHistory = value;
                    OnPropertyChange(nameof(SelectedPersonalMedicalHistory));
                    (EditCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    (DeleteCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        private PersonalMedicalHistory _newPersonalMedicalHistory = new PersonalMedicalHistory();
        public PersonalMedicalHistory NewPersonalMedicalHistory
        {
            get { return _newPersonalMedicalHistory; }
            set
            {
                _newPersonalMedicalHistory = value;
                OnPropertyChange(nameof(NewPersonalMedicalHistory));
            }
        }

        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////
        public PersonalMedicalHistoriesVM()
        {
            LoadPersonalMedicalHistoriesAsync();
            InitializeCommands();

            LoadPatientsFromPatientVM();
        }

        private async Task LoadPersonalMedicalHistoriesAsync()
        {
            PersonalMedicalHistoriesRepo repo = new PersonalMedicalHistoriesRepo();
            PersonalMedicalHistoriesList = await repo.GetAllPersonalMedicalHistoriesAsync();
            PersonalMedicalHistoriesView = CollectionViewSource.GetDefaultView(PersonalMedicalHistoriesList);
            PersonalMedicalHistoriesView.Filter = PersonalMedicalHistoriesFilter;
        }
        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////


        private void InitializeCommands()
        {
            AddCommand = new RelayCommand(AddNewAction);
            DeleteCommand = new RelayCommand(DeleteAction, CanExecuteDelete);
            EditCommand = new RelayCommand(EditAction, CanEdit);
        }

        private bool CanExecuteDelete(object parameter)
        {
            return SelectedPersonalMedicalHistory != null;
        }

        private async void DeleteAction(object parameter)
        {
            if (SelectedPersonalMedicalHistory == null) return;

            PersonalMedicalHistoriesRepo personalMedicalHistoriesRepoRepo = new PersonalMedicalHistoriesRepo();
            await personalMedicalHistoriesRepoRepo.DeletePersonalMedicalHistory(SelectedPersonalMedicalHistory.Id);
            await LoadPersonalMedicalHistoriesAsync();
        }

        private async void AddNewAction(object parameter)
        {
            if (!PersonalMedicalHistoryValidator.IsDescriptionAndPatientFilled(NewPersonalMedicalHistory)) 
            {
                MessageBox.Show("Vyplňte všechna pole!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }     
            PersonalMedicalHistoriesRepo personalMedicalHistoriesRepoRepo = new PersonalMedicalHistoriesRepo();
            await personalMedicalHistoriesRepoRepo.AddPersonalMedicalHistory(NewPersonalMedicalHistory);
            await LoadPersonalMedicalHistoriesAsync();
            NewPersonalMedicalHistory = new PersonalMedicalHistory();
        }

        private bool CanEdit(object parameter)
        {
            return SelectedPersonalMedicalHistory != null;
        }

        private void EditAction(object parameter)
        {
            if (!CanEdit(parameter)) return;


            EditPersonalMedicalHistoryVM editVM = new EditPersonalMedicalHistoryVM(SelectedPersonalMedicalHistory);
            EditPersonalMedicalHistoryDialog editDialog = new EditPersonalMedicalHistoryDialog(editVM);

            editDialog.ShowDialog();

            LoadPersonalMedicalHistoriesAsync();
            
        }

        //FILTER/////////////////////////////////////////////////////////////////////

        // Hledaný řetezec v TextBoxu pro vyhledávání
        private string _searchText;
        public ICollectionView PersonalMedicalHistoriesView { get; private set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChange(nameof(SearchText));
                PersonalMedicalHistoriesView.Refresh();
            }
        }

        private bool PersonalMedicalHistoriesFilter(object item)
        {

            if (string.IsNullOrWhiteSpace(_searchText)) return true;

            var personalMedicalHistory = item as PersonalMedicalHistory;
            if (personalMedicalHistory == null) return false;



            return personalMedicalHistory.Id.ToString().Contains(_searchText)
                || personalMedicalHistory.BirthNumberOfPatient.ToString().Contains(_searchText)
                || personalMedicalHistory.IdOfPatient.ToString().Contains(_searchText)
                || personalMedicalHistory.Description.Contains(_searchText, StringComparison.OrdinalIgnoreCase);
        }
        //FILTER/////////////////////////////////////////////////////////////////////
    }
}
