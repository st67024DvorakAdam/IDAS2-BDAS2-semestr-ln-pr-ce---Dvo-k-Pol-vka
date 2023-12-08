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
    public class AddNewIllnessVM : BaseViewModel
    {
        private string _newIllness;
        public string NewIllness
        {
            get => _newIllness;
            set
            {
                _newIllness = value;
                OnPropertyChange(nameof(NewIllness));
            }
        }

        private string _newIllnessDescription;
        public string NewIllnessDescription
        {
            get => _newIllnessDescription;
            set
            {
                _newIllnessDescription = value;
                OnPropertyChange(nameof(NewIllnessDescription));
            }
        }

        private Patient _patient;
        public Patient Patient
        {
            get => _patient;
            set
            {
                _patient = value;
                OnPropertyChange(nameof(Patient));
            }
        }

        public event Action CloseRequested;

        public ICommand AddNewIllnessCommand { get; }
        public ICommand CancelCommand { get; }

        public AddNewIllnessVM(Patient patient)
        {
            _patient = patient;
            AddNewIllnessCommand = new RelayCommand(_ => AddIllness());
            CancelCommand = new RelayCommand(_ => Cancel());
        }

        private void AddIllness()
        {
            IllnessesRepo repo = new IllnessesRepo();
            if (!IsIllnessValidAndFilled(_newIllness, _newIllnessDescription))
            {
                MessageBox.Show("Vyplňte správně všechna pole!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            repo.AddIllness(_newIllness, _patient.Id, _newIllnessDescription);
            CloseRequested?.Invoke();
        }

        private void Cancel()
        {
            
            CloseRequested?.Invoke();
        }

        private bool IsIllnessValidAndFilled(string illnessName, string description)
        {
            return (!string.IsNullOrEmpty(illnessName) && !string.IsNullOrEmpty(description));
        }
    }
}
