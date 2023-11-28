using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public EditDosageVM(Drug drug)
        {
            _drug = drug;

            SaveDosageCommand = new RelayCommand(_ => SaveDosage());
            CancelEditDosageCommand = new RelayCommand(_ => CancelEdit());
        }

        private async void SaveDosage()
        {
            DrugsRepo repo = new DrugsRepo();
            _drug.Dosage = _newDosage;
            await repo.UpdateDosageAsync(_drug);
            CloseRequested?.Invoke();
        }

        private void CancelEdit()
        {
            CloseRequested?.Invoke();

        }
    }
}
