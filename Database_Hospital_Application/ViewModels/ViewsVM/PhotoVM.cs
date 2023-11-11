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
    public class PhotoVM : BaseViewModel
    {
        public ObservableCollection<Foto> _photosList;

        public ObservableCollection<Foto> PhotosList
        {
            get { return _photosList; }
            set { _photosList = value; OnPropertyChange(nameof(PhotosList)); }
        }

        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////
        public PhotoVM()
        {
            LoadPhotosAsync();
            PhotosView = CollectionViewSource.GetDefaultView(PhotosList);
            PhotosView.Filter = PhotosFilter;
        }

        private async Task LoadPhotosAsync()
        {
            PhotosRepo repo = new PhotosRepo();
            PhotosList = await repo.GetAllPhotosAsync();
        }
        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////
        ///

        //FILTER/////////////////////////////////////////////////////////////////////

        private string _searchText;
        public ICollectionView PhotosView { get; private set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChange(nameof(SearchText));
                PhotosView.Refresh();
            }
        }

        private bool PhotosFilter(object item)
        {

            if (string.IsNullOrWhiteSpace(_searchText)) return true;

            var foto = item as Foto;
            if (foto == null) return false;

            return foto.Id.ToString().Contains(_searchText)
                || foto.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                || foto.Suffix.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                || foto.DateOfUpload.ToString().Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                || foto.DateOfModification.ToString().Contains(_searchText, StringComparison.OrdinalIgnoreCase);
        }
        //FILTER/////////////////////////////////////////////////////////////////////
    }
}
