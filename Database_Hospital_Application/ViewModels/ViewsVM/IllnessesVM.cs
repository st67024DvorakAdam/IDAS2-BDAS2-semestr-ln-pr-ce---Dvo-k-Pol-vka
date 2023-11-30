using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.ViewModels.Dialogs.Edit;
using Database_Hospital_Application.Views.Lists.Dialogs.Contact;
using Database_Hospital_Application.Views.Lists.Dialogs.Illness;
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
    public class IllnessesVM : BaseViewModel
    {
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

        private ObservableCollection<MedicalCard> _medicalCardsList;

        public ObservableCollection<MedicalCard> MedicalCardsList
        {
            get { return _medicalCardsList; }
            set
            {
                _medicalCardsList = value;
                OnPropertyChange(nameof(MedicalCardsList));
            }
        }

        // BUTTONS
        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand EditCommand { get; private set; }

        private Illness _selectedIllness;
        public Illness SelectedIllness
        {
            get { return _selectedIllness; }
            set
            {
                if (_selectedIllness != value)
                {
                    _selectedIllness = value;
                    OnPropertyChange(nameof(SelectedIllness));
                    (EditCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    (DeleteCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        private Illness _newIllness = new Illness();
        public Illness NewIllness
        {
            get { return _newIllness; }
            set
            {
                _newIllness = value;
                OnPropertyChange(nameof(NewIllness));
            }
        }

        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////

        public IllnessesVM()
        {
            LoadIllnessesAsync();

            LoadMedicalCardsAsync();
            IllnessesView = CollectionViewSource.GetDefaultView(IllnessesList);
            IllnessesView.Filter = IllnessesFilter;
            InitializeCommands();
        }
        private async Task LoadIllnessesAsync()
        {
            IllnessesRepo repo = new IllnessesRepo();
            IllnessesList = await repo.GetIllnessesAsync();
        }
        private async Task LoadMedicalCardsAsync()
        {
            MedicalCardsRepo repo = new MedicalCardsRepo();
            MedicalCardsList = await repo.GetAllMedicalCardsAsync();
        }


        private void InitializeCommands()
        {
            AddCommand = new RelayCommand(AddNewIllnessAction);
            DeleteCommand = new RelayCommand(DeleteIllnessAction, CanExecuteDelete);
            EditCommand = new RelayCommand(EditAction, CanEdit);
        }

        private bool CanExecuteDelete(object parameter)
        {
            return SelectedIllness != null;
        }

        private async void DeleteIllnessAction(object parameter)
        {
            if (SelectedIllness == null) return;

            IllnessesRepo illnessesRepo = new IllnessesRepo();
            await illnessesRepo.DeleteIllness(SelectedIllness.Id);
            await LoadIllnessesAsync();
        }

        private async void AddNewIllnessAction(object parameter)
        {
            IllnessesRepo illnessesRepo = new IllnessesRepo();
            await illnessesRepo.AddIllness(NewIllness);
            await LoadIllnessesAsync();
            NewIllness = new Illness();
        }

        private bool CanEdit(object parameter)
        {
            return SelectedIllness != null;
        }

        private void EditAction(object parameter)
        {
            if (!CanEdit(parameter)) return;


            EditIllnessVM editVM = new EditIllnessVM(SelectedIllness);
            EditIllnessDialog editDialog = new EditIllnessDialog(editVM);

            editDialog.ShowDialog();

            if (editDialog.DialogResult == true)
            {
                LoadIllnessesAsync();
            }
        }

        //FILTER/////////////////////////////////////////////////////////////////////

        private string _searchText;
        public ICollectionView IllnessesView { get; private set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChange(nameof(SearchText));
                IllnessesView.Refresh();
            }
        }

        private bool IllnessesFilter(object item)
        {

            if (string.IsNullOrWhiteSpace(_searchText)) return true;

            var illness = item as Illness;
            if (illness == null) return false;

            return illness.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                || illness.Id.ToString().Contains(_searchText);
        }
        //FILTER/////////////////////////////////////////////////////////////////////
    }
}
