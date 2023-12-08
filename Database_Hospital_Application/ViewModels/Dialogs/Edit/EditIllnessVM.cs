using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;

namespace Database_Hospital_Application.ViewModels.Dialogs.Edit
{
    public class EditIllnessVM : BaseViewModel
    {
        private Illness _editableIllness;
        public Illness EditableIllness
        {
            get { return _editableIllness; }
            set { _editableIllness = value; OnPropertyChange(nameof(EditableIllness)); }
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

        private async Task LoadMedicalCardsAsync()
        {
            MedicalCardsRepo repo = new MedicalCardsRepo();
            MedicalCardsList = await repo.GetAllMedicalCardsAsync();
        }

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public EditIllnessVM(Illness illness)
        {
            LoadMedicalCardsAsync();
            EditableIllness = illness;
            SaveCommand = new AsyncRelayCommand(async (o) => await SaveActionAsync());
            CancelCommand = new RelayCommand(CancelAction);
            
        }
        private bool CanSaveExecute(object parameter)
        {

            return EditableIllness != null;
        }

        private async Task SaveActionAsync()
        {
            if (!IllnessValidator.IsFilled(EditableIllness))
            {
                MessageBox.Show("Vyplňte všechna pole!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                IllnessesRepo illnessRepo = new IllnessesRepo();
                int affectedRows = await illnessRepo.UpdateIllness(EditableIllness);
                if (affectedRows == 0)
                {
                    MessageBox.Show("Nemoc se nepodařilo změnit", "Not Ok", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    MessageBox.Show("Nemoc byla úspšně aktualizována", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
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
    }
}
