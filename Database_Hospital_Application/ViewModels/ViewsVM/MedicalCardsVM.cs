using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.ViewModels.Dialogs.Edit;
using Database_Hospital_Application.Views.Lists.Dialogs.Drug;
using Database_Hospital_Application.Views.Lists.Dialogs.Illness;
using Database_Hospital_Application.Views.Lists.Dialogs.MedicalCard;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class MedicalCardsVM : BaseViewModel
    {
        private ObservableCollection<MedicalCard> _medicalCardsList;

        public ObservableCollection<MedicalCard> MedicalCardsList
        {
            get { return _medicalCardsList; }
            set { _medicalCardsList = value; OnPropertyChange(nameof(MedicalCardsList)); }
        }

        private Illness _newIllness;

        public Illness NewIllness
        {
            get { return _newIllness; }
            set { _newIllness = value; OnPropertyChange(nameof(NewIllness)); }
        }

        private Illness _selectedIllness;

        public Illness SelectedIllness
        {
            get { return _selectedIllness; }
            set { _selectedIllness = value; OnPropertyChange(nameof(SelectedIllness)); }
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

        private async Task LoadPatientsAsync()
        {
            PatientRepo repo = new PatientRepo();
            PatientsList = await repo.GetAllPatientsAsync();
        }

        private ObservableCollection<Illness> _illnessesList;
        public ObservableCollection<Illness> IllnessesList
        {
            get { return _illnessesList; }
            set
            {
                _illnessesList = value;
                OnPropertyChange(nameof(IllnessesList));
            }
        }

        private async Task LoadIllnessesAsync()
        {
            IllnessesRepo repo = new IllnessesRepo();
            IllnessesList = await repo.GetIllnessesAsync();
        }

        // BUTTONS
        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand AddIllnessCommand { get; private set; }
        public ICommand DeleteIllnessCommand { get; private set; }

        private MedicalCard _selectedMedicalCard;
        public MedicalCard SelectedMedicalCard
        {
            get { return _selectedMedicalCard; }
            set
            {
                if (_selectedMedicalCard != value)
                {
                    _selectedMedicalCard = value;
                    OnPropertyChange(nameof(SelectedMedicalCard));
                    (EditCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    (DeleteCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    (AddIllnessCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    (DeleteIllnessCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        private MedicalCard _newMedicalCard = new MedicalCard();
        public MedicalCard NewMedicalCard
        {
            get { return _newMedicalCard; }
            set
            {
                _newMedicalCard = value;
                OnPropertyChange(nameof(NewMedicalCard));
            }
        }

        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////

        public MedicalCardsVM()
        {
            LoadMedicalCardsAsync();
            MedicalCardsView = CollectionViewSource.GetDefaultView(MedicalCardsList);
            MedicalCardsView.Filter = MedicalCardsFilter;
            InitializeCommands();

            LoadPatientsAsync();
            LoadIllnessesAsync();
        }

        private async Task LoadMedicalCardsAsync()
        {
            MedicalCardsRepo repo = new MedicalCardsRepo();
            MedicalCardsList = await repo.GetAllMedicalCardsAsync();
        }
        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////
        ///

        private void InitializeCommands()
        {
            AddCommand = new RelayCommand(AddNewAction);
            DeleteCommand = new RelayCommand(DeleteAction, CanExecuteDelete);
            EditCommand = new RelayCommand(EditAction, CanEdit);
            AddIllnessCommand = new RelayCommand(AddIllnessAction, CanEdit);
            DeleteIllnessCommand = new RelayCommand(DeleteIllnessAction, CanEdit);
        }

        private bool CanExecuteDelete(object parameter)
        {
            return SelectedMedicalCard != null;
        }

        private async void DeleteAction(object parameter)
        {
            if (SelectedMedicalCard == null) return;

            MedicalCardsRepo medicalCardsRepo = new MedicalCardsRepo();
            await medicalCardsRepo.DeleteMedicalCard(SelectedMedicalCard.Id);
            await LoadMedicalCardsAsync();
        }

        private async void AddNewAction(object parameter)
        {
            MedicalCardsRepo medicalCardsRepo = new MedicalCardsRepo();
            await medicalCardsRepo.AddMedicalCard(NewMedicalCard, NewIllness);
            await LoadMedicalCardsAsync();
            NewMedicalCard = new MedicalCard();
        }

        private async void AddIllnessAction(object parameter)
        {
            if (!CanEdit(parameter)) return;

            EditMedicalCardVM editVM = new EditMedicalCardVM(SelectedMedicalCard);
            AddIllnessIntoMedicalCard editDialog = new AddIllnessIntoMedicalCard(editVM);


            editDialog.ShowDialog();

            if (editDialog.DialogResult == true)
            {
                await LoadMedicalCardsAsync();
            }
        }

        private async void DeleteIllnessAction(object parameter)
        {
            if (!CanEdit(parameter)) return;

            EditMedicalCardVM editVM = new EditMedicalCardVM(SelectedMedicalCard);
            DeleteIllnessFromMedicalCard editDialog = new DeleteIllnessFromMedicalCard(editVM);


            editDialog.ShowDialog();

            if (editDialog.DialogResult == true)
            {
                await LoadMedicalCardsAsync();
            }
        }

        private bool CanEdit(object parameter)
        {
            return SelectedMedicalCard != null;
        }

        private void EditAction(object parameter)
        {
            if (!CanEdit(parameter)) return;


            EditMedicalCardVM editVM = new EditMedicalCardVM(SelectedMedicalCard);
            EditMedicalCardDialog editDialog = new EditMedicalCardDialog(editVM);

            editDialog.ShowDialog();

            if (editDialog.DialogResult == true)
            {
                LoadMedicalCardsAsync();
            }
        }

        //FILTER/////////////////////////////////////////////////////////////////////

        private string _searchText;
        public ICollectionView MedicalCardsView { get; private set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChange(nameof(SearchText));
                MedicalCardsView.Refresh();
            }
        }

        private bool MedicalCardsFilter(object item)
        {

            if (string.IsNullOrWhiteSpace(_searchText)) return true;

            var medicalCard = item as MedicalCard;
            if (medicalCard == null) return false;

            return medicalCard.IdOfPatient.ToString().Contains(_searchText)
                || medicalCard.BirthNumberOfPatient.ToString().Contains(_searchText)
                || medicalCard.StringVersionOfIllnesses.Contains(_searchText, StringComparison.OrdinalIgnoreCase);
        }
        //FILTER/////////////////////////////////////////////////////////////////////

    }
}
