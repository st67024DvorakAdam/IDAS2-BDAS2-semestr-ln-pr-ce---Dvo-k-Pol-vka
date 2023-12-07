using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.Dialogs.Edit.Doctor
{
    public class EditDosageVM : BaseViewModel
    {
        private Drug _drug;
        public Drug Drug
        {
            get => _drug;
            set
            {
                _drug = value;
                OnPropertyChange(nameof(Drug));
            }
        }

        private Illness _illness;
        public Illness Illness
        {
            get => _illness;
            set
            {
                _illness = value;
                OnPropertyChange(nameof(Illness));
            }
        }


        private int _newDosage;
        public int NewDosage
        {
            get => _newDosage;
            set
            {
                _newDosage = value;
                OnPropertyChange(nameof(NewDosage));
            }
        }
        public event Action CloseRequested;

        public ICommand SaveDosageCommand { get; }
        public ICommand CancelEditDosageCommand { get; }

        public EditDosageVM(Drug drug, Illness illness)
        {
            _drug = drug;
            _illness = illness;

            SaveDosageCommand = new RelayCommand(_ => SaveDosage());
            CancelEditDosageCommand = new RelayCommand(_ => CancelEdit());
        }

        private async void SaveDosage()
        {
            if (NewDosage == 0)
            {
                MessageBox.Show("Dávkování léku nesmí být 0!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DrugsRepo repo = new DrugsRepo();
            _drug.Dosage = _newDosage;
            await repo.UpdateDosageAsync(_drug, _illness);
            CloseRequested?.Invoke();
        }

        private void CancelEdit()
        {
            CloseRequested?.Invoke();

        }
    }
}
