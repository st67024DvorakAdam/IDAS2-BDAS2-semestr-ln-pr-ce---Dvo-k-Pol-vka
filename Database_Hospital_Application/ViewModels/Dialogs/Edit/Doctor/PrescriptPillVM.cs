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
    public class PrescriptPillVM : BaseViewModel
    {
        
        private string _pill;
        public string Pill
        {
            get => _pill;
            set
            {
                _pill = value;
                OnPropertyChange(nameof(Pill));
            }
        }

        private int _dosage;
        public int Dosage
        {
            get => _dosage;
            set
            {
                _dosage = value;
                OnPropertyChange(nameof(Dosage));
            }
        }
        public event Action CloseRequested;

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        private User _currentUser;
        private Illness _selectedIllness;
        private Patient _patient;
        public PrescriptPillVM(Patient patient, User user, Illness illness)
        {
            _currentUser = user;
            _selectedIllness = illness;
            _patient = patient;

            SaveCommand = new RelayCommand(_ => PrescriptPill());
            CancelCommand = new RelayCommand(_ => CancelEdit());
        }

        private async void PrescriptPill()
        {
            if (_dosage == 0)
            {
                MessageBox.Show("Dávkování léku nesmí být 0!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DrugsRepo repo = new DrugsRepo();
            
            await repo.PrescriptDrugForIllness(new Drug(_pill, _dosage, _currentUser.Id), _selectedIllness);
            CloseRequested?.Invoke();
        }

        private void CancelEdit()
        {
            CloseRequested?.Invoke();

        }
    }
}
