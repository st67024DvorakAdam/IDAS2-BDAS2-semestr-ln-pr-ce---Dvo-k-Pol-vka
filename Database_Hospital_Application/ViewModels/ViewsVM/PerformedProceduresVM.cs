using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class PerformedProceduresVM: BaseViewModel
    {
        private ObservableCollection<PerformedProcedure> _performedProceduresList;

        public ObservableCollection<PerformedProcedure> PerformedProceduresList
        {
            get { return _performedProceduresList; }
            set
            {
                _performedProceduresList = value;
                OnPropertyChange(nameof(PerformedProceduresList));
            }
        }


        public PerformedProceduresVM()
        {
            PerformedProceduresRepo repo = new PerformedProceduresRepo();
            PerformedProceduresList = repo.GetAllPerformedProcedures();
        }
    }
}
