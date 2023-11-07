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
    public class DrugsVM : BaseViewModel
    {
        private ObservableCollection<Drug> _drugsList;

        public ObservableCollection<Drug> DrugsList
        {
            get { return _drugsList; }
            set
            {
                _drugsList = value;
                OnPropertyChange(nameof(DrugsList));
            }
        }


        public DrugsVM()
        {
            LoadDrugsAsync();
        }
        private async Task LoadDrugsAsync()
        {
            DrugsRepo repo = new DrugsRepo();
            DrugsList = await repo.GetAllDrugsAsync();
        }
    }
}
