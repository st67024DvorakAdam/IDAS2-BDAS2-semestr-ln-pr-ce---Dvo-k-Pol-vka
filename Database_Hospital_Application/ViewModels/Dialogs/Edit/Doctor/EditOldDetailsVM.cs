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
    public class EditOldDetailsVM : BaseViewModel
    {
      

        
        private Hospitalization _hospitalization;

        private string _newDetails;
        public string NewDetails
        {
            get => _newDetails;
            set
            {
                _newDetails = value;
                OnPropertyChange(nameof(NewDetails));
            }
        }
        public event Action CloseRequested;

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public EditOldDetailsVM(Hospitalization hospitalization)
        {
            _hospitalization = hospitalization;
            SaveCommand = new RelayCommand(_ => SaveDetails());
            CancelCommand = new RelayCommand(_ => CancelEdit());
        }

        private async void SaveDetails()
        {
            _hospitalization.Details = _newDetails;
            HospitalizationRepo repo = new HospitalizationRepo();
            repo.UpdateHospitalization(_hospitalization);
            CloseRequested?.Invoke();
        }

        private void CancelEdit()
        {
            CloseRequested?.Invoke();

        }
    }
}
