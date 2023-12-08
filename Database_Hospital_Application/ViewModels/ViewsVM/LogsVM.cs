using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

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
        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////
        public LogsVM()
        {
            LoadLogsAsync();
            if (LogsList != null)
            {
                LogsView = CollectionViewSource.GetDefaultView(LogsList);
                LogsView.Filter = LogsFilter;
            }
        }

        private async Task LoadLogsAsync()
        {

            LogRepo repo = new LogRepo();
            LogsList = await repo.GetAllLogsAsync();
        }

        //FILTER/////////////////////////////////////////////////////////////////////

        private string _searchText;
        public ICollectionView LogsView { get; private set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChange(nameof(SearchText));
                LogsView.Refresh();
            }
        }

        private bool LogsFilter(object item)
        {

            if (string.IsNullOrWhiteSpace(_searchText)) return true;

            var log = item as Log;
            if (log == null) return false;

            return log.Id.ToString().Contains(_searchText)
                || log.NewState.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                || log.OldState.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                || log.OperationType.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                || log.Table.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                || log.TimeStamp.ToString().Contains(_searchText);
        }
        //FILTER/////////////////////////////////////////////////////////////////////
    }
}
