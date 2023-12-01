using Database_Hospital_Application.Models.Entities.HelpEntities;
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
using System.Windows.Input;
using Database_Hospital_Application.Commands;
using Database_Hospital_Application.ViewModels.Dialogs.Edit;
using Database_Hospital_Application.Views.Lists.Dialogs.Employee;

namespace Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM
{
    public class PreceptedPillsVM : BaseViewModel
    {
        private User _currUser { get; set; }

        private ObservableCollection<DrugsPreceptedByDoctor> _drugsPreceptedByDoctorList;

        public ObservableCollection<DrugsPreceptedByDoctor> DrugsPreceptedByDoctorList
        {
            get { return _drugsPreceptedByDoctorList; }
            set
            {
                _drugsPreceptedByDoctorList = value;
                OnPropertyChange(nameof(DrugsPreceptedByDoctorList));
            }
        }

        private DrugsPreceptedByDoctor _selectedDrugsPreceptedByDoctor;
        public DrugsPreceptedByDoctor SelectedDrugsPreceptedByDoctor
        {
            get { return _selectedDrugsPreceptedByDoctor; }
            set
            {
                if (_selectedDrugsPreceptedByDoctor != value)
                {
                    _selectedDrugsPreceptedByDoctor = value;
                    OnPropertyChange(nameof(SelectedDrugsPreceptedByDoctor));
                    (EditCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    (DeleteCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }


        public PreceptedPillsVM(User user)
        {
            _currUser = user;
            LoadPreceptedDrugsAsync();
            PreceptedPillsView = CollectionViewSource.GetDefaultView(DrugsPreceptedByDoctorList);
            PreceptedPillsView.Filter = PreceptedPillsFilter;
            InitializeCommands();
        }
        
        private async Task LoadPreceptedDrugsAsync()
        {
            DrugsRepo repo = new DrugsRepo();
            DrugsPreceptedByDoctorList = await repo.GetPreceptedDrugByDoctor(_currUser);
        }


        public ICommand DeleteCommand { get; private set; }
        public ICommand EditCommand { get; private set; }

        private void InitializeCommands()
        {
            DeleteCommand = new RelayCommand(DeleteAction, CanExecuteDelete);
            EditCommand = new RelayCommand(EditAction, CanEdit);
        }

        private bool CanExecuteDelete(object parameter)
        {
            return SelectedDrugsPreceptedByDoctor != null;
        }

        private async void DeleteAction(object parameter)
        {
            if (SelectedDrugsPreceptedByDoctor == null) return;

            DrugsRepo drugRepo = new DrugsRepo();
            IllnessDrugConnectionRepo illnessDrugConnectionRepo = new IllnessDrugConnectionRepo();
            
            //mazání vazby mezi lékem a nemocí
            await illnessDrugConnectionRepo.DeleteIllnessDrugConnection(new IllnessDrugConnection 
            { 
                _drug = SelectedDrugsPreceptedByDoctor._drug,
                _illness = SelectedDrugsPreceptedByDoctor._illness
            });

            await drugRepo.DeleteDrug(SelectedDrugsPreceptedByDoctor._drug.Id);
            await LoadPreceptedDrugsAsync();
        }

        private bool CanEdit(object parameter)
        {
            return SelectedDrugsPreceptedByDoctor != null;
        }

        private void EditAction(object parameter)
        {
            //if (!CanEdit(parameter)) return;


            //EditEmployeeVM editVM = new EditEmployeeVM(SelectedEmployee);
            //EditEmployeeDialog editDialog = new EditEmployeeDialog(editVM);

            //editDialog.ShowDialog();

            //if (editDialog.DialogResult == true)
            //{
            //    LoadEmployeesAsync();
            //}
            throw new NotImplementedException();
        }

        // Filter
        private string _searchText;
        public ICollectionView PreceptedPillsView { get; private set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChange(nameof(SearchText));
                PreceptedPillsView.Refresh();
            }
        }

        private bool PreceptedPillsFilter(object item)
        {
            if (string.IsNullOrWhiteSpace(_searchText)) return true;

            var drugs = item as DrugsPreceptedByDoctor;
            var fullName = $"{drugs._patient.FirstName} {drugs._patient.LastName}";
            var fullName2 = $"{drugs._patient.LastName} {drugs._patient.FirstName}";
            return drugs != null &&
                   (drugs._patient.FirstName.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                   || drugs._patient.LastName.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                   || fullName.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                   || fullName2.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                   || drugs._drug.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                    || drugs._drug.Dosage.ToString().Contains(_searchText));
        }
    }

}

