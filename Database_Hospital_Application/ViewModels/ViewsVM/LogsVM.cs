using Database_Hospital_Application.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class LogsVM : BaseViewModel
    {
        private ObservableCollection<Log> _logsList;

        public ObservableCollection<Log> LogsList
        {
            get { return _logsList; }
            set
            {
                _logsList = value;
                OnPropertyChange(nameof(LogsList));
            }
        }

        public LogsVM()
        {
            LoadLogsAsync();
        }

        private async Task LoadLogsAsync()
        {
            
            //LogRepo repo = new LogRepo(); 
            //LogsList = await repo.GetAllLogsAsync(); 
        }
    }
}
