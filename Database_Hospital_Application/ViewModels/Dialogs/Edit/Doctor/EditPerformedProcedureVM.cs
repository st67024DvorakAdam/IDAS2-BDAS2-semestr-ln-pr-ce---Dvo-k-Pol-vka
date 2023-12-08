using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using System;
using System.Windows;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.Dialogs.Edit.Doctor
{
    public class EditPerformedProcedureVM : BaseViewModel
    {
        public ICommand EditCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChange(nameof(Name));
                    UpdateCommands();
                }
            }
        }

        private double _price;
        public double Price
        {
            get => _price;
            set
            {
                if (_price != value)
                {
                    _price = value;
                    OnPropertyChange(nameof(Price));
                    UpdateCommands();
                }
            }
        }

        private bool _isCoveredByInsurance;
        public bool IsCoveredByInsurance
        {
            get => _isCoveredByInsurance;
            set
            {
                if (_isCoveredByInsurance != value)
                {
                    _isCoveredByInsurance = value;
                    OnPropertyChange(nameof(IsCoveredByInsurance));
                    UpdateCommands();
                }
            }
        }



        private PerformedProcedure currentProcedure;
        private readonly PerformedProceduresRepo _proceduresRepo;
        public event Action CloseRequested;

        public EditPerformedProcedureVM(PerformedProcedure procedure)
        {
            Name = procedure.Name;
            Price = procedure.Price;
            IsCoveredByInsurance = procedure.IsCoveredByInsurence;
            currentProcedure = procedure;

            _proceduresRepo = new PerformedProceduresRepo();

            EditCommand = new RelayCommand(_ => EditProcedure(), _ => Name != null);
            CancelCommand = new RelayCommand(_ => Cancel());
        }

        public void UpdateCommands()
        {
            (EditCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }

        // TODO upravit detaily u hospitalizace u zakroku s hospitalizací
        private void EditProcedure()
        {
            if(!IsProcedureValidAndFilled())
            {
                MessageBox.Show("Vyplňte všechna pole!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
                PerformedProcedure editedProcedure = new PerformedProcedure();
                editedProcedure.Name = Name;    
                editedProcedure.Price = Price;  
                editedProcedure.IsCoveredByInsurence = IsCoveredByInsurance;
                editedProcedure.Id = currentProcedure.Id;
                editedProcedure.IdOfPatient = currentProcedure.IdOfPatient;

                _proceduresRepo.UpdatePerformedProcedure(editedProcedure);
                CloseRequested?.Invoke();
            
            

        }

        private void Cancel()
        {
            CloseRequested?.Invoke();
        }

        private bool IsProcedureValidAndFilled()
        {
            return (!string.IsNullOrEmpty(Name) && Price > 0);
        }
    }
}
