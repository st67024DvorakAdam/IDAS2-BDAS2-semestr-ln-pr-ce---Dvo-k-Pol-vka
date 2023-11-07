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
    public class PhotoVM : BaseViewModel
    {
        public ObservableCollection<Foto> _photosList;

        public ObservableCollection<Foto> PhotosList
        {
            get { return _photosList; }
            set { _photosList = value; OnPropertyChange(nameof(PhotosList)); }
        }

        public PhotoVM()
        {
            LoadPhotosAsync();
        }

        private async Task LoadPhotosAsync()
        {
            PhotosRepo repo = new PhotosRepo();
            PhotosList = await repo.GetAllPhotosAsync();
        }
    }
}
