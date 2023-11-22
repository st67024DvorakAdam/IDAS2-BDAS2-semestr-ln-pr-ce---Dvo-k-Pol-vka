using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.Models.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Database_Hospital_Application.ViewModels.ViewsVM;

namespace Database_Hospital_Application.ViewModels.Dialogs.Edit
{
    public class EditMedicalCardVM:BaseViewModel
    {
        private MedicalCard _editableMedicalCard;
        public MedicalCard EditableMedicalCard
        {
            get { return _editableMedicalCard; }
            set { _editableMedicalCard = value; OnPropertyChange(nameof(EditableMedicalCard)); }
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


        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand SaveNewIllnessCommand { get; set; }
        public ICommand CancelNewIllnessCommand { get; set; }
        public ICommand SaveDeletingIllnessCommand { get; set; }

        public EditMedicalCardVM(MedicalCard medicalCard)
        {
            EditableMedicalCard = medicalCard;
            NewIllness = new Illness();

            SaveCommand = new AsyncRelayCommand(async (o) => await SaveActionAsync());
            CancelCommand = new RelayCommand(CancelAction);

            SaveNewIllnessCommand = new AsyncRelayCommand(async (o) => await SaveNewIllnessActionAsync());
            SaveDeletingIllnessCommand = new AsyncRelayCommand(async (o) => await SaveDeletingIllnessActionAsync());
            CancelNewIllnessCommand = new RelayCommand(CancelNewIllnessAction);

            LoadPatientsAsync();
            LoadIllnessesAsync();
        }
        private bool CanSaveExecute(object parameter)
        {

            return EditableMedicalCard != null;
        }

        private async Task SaveActionAsync()
        {
            try
            {
                MedicalCardsRepo medicalCardsRepo = new MedicalCardsRepo();
                int affectedRows = await medicalCardsRepo.UpdateMedicalCard(EditableMedicalCard);
                if (affectedRows == 0)
                {
                    MessageBox.Show("Zdravotní kartu se nepodařilo změnit", "Not Ok", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    MessageBox.Show("Zdravotní karta byl úspšně aktualizován", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
                    OnClosingRequest();
                }


            }
            catch (Exception ex)
            {
                OnClosingRequest();
            }
        }

        public void CancelAction(object parameter)
        {
            OnClosingRequest();
        }

        public event EventHandler ClosingRequest;

        public void OnClosingRequest()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ClosingRequest?.Invoke(this, EventArgs.Empty);
            });
        }


        //Pro úpravu listu nemocí v kartě
        private Illness _newIllness;

        public Illness NewIllness
        {
            get { return _newIllness; }
            set { _newIllness = value; OnPropertyChange(nameof(NewIllness)); }
        }

        private async Task SaveNewIllnessActionAsync()
        {
            try
            {
                MedicalCardsRepo medicalCardsRepo = new MedicalCardsRepo();
                int affectedRows = await medicalCardsRepo.AddIllnessIntoMedicalCard(EditableMedicalCard,NewIllness);

                if (affectedRows == 0)
                {
                    MessageBox.Show("Zdravotní kartu se nepodařilo změnit", "Not Ok", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    MessageBox.Show("Zdravotní karta byl úspšně aktualizován", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
                    OnClosingRequest();
                }


            }
            catch (Exception ex)
            {
                OnClosingRequest();
            }
        }

        private async Task SaveDeletingIllnessActionAsync()
        {
            try
            {
                MedicalCardsRepo medicalCardsRepo = new MedicalCardsRepo();
                int affectedRows = await medicalCardsRepo.DeleteIllnessFromMedicalCard(EditableMedicalCard, NewIllness);

                if (affectedRows == 0)
                {
                    MessageBox.Show("Zdravotní kartu se nepodařilo změnit", "Not Ok", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    MessageBox.Show("Zdravotní karta byl úspšně aktualizován", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
                    OnClosingRequest();
                }


            }
            catch (Exception ex)
            {
                OnClosingRequest();
            }
        }

        public void CancelNewIllnessAction(object parameter)
        {
            OnClosingRequest();
        }

    }
}
