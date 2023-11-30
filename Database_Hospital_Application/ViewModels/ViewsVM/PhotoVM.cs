﻿using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.Models.Tools;
using Database_Hospital_Application.ViewModels.Dialogs.Edit;
using Database_Hospital_Application.Views.Lists.Dialogs.Employee;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class PhotoVM : BaseViewModel
    {
        public ObservableCollection<Foto> _photosList;
        private OpenFileDialogService fileDialogService;

        public ObservableCollection<Foto> PhotosList
        {
            get { return _photosList; }
            set { _photosList = value; OnPropertyChange(nameof(PhotosList)); }
        }


        // BUTTONS
        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand EditCommand { get; private set; }

        private Foto _selectedPhoto;
        public Foto SelectedPhoto
        {
            get { return _selectedPhoto; }
            set
            {
                if (_selectedPhoto != value)
                {
                    _selectedPhoto = value;
                    OnPropertyChange(nameof(SelectedPhoto));
                    (EditCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    (DeleteCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        private Foto _newFoto = new Foto();
        public Foto NewPhoto
        {
            get { return _newFoto; }
            set
            {
                _newFoto = value;
                OnPropertyChange(nameof(NewPhoto));
            }
        }

        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////
        public PhotoVM()
        {
            this.fileDialogService = new OpenFileDialogService();
            LoadPhotosAsync();
            PhotosView = CollectionViewSource.GetDefaultView(PhotosList);
            PhotosView.Filter = PhotosFilter;
            InitializeCommands();
        }

        private async Task LoadPhotosAsync()
        {
            PhotosRepo repo = new PhotosRepo();
            PhotosList = await repo.GetAllPhotosAsync();
        }


        private void InitializeCommands()
        {
            AddCommand = new RelayCommand(AddNewAction);
            DeleteCommand = new RelayCommand(DeleteAction, CanExecuteDelete);
            EditCommand = new RelayCommand(EditAction, CanEdit);
        }

        private bool CanExecuteDelete(object parameter)
        {
            return SelectedPhoto != null;
        }

        private async void DeleteAction(object parameter)
        {
            if (SelectedPhoto == null) return;
            if (SelectedPhoto.Id == 1) 
            {
                MessageBox.Show("Nelze mazat základní foto!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return; 
            } //foto pro účty bez vlastního fota nelze => nemůžeme ho smazat

            PhotosRepo photosRepo = new PhotosRepo();
            await photosRepo.DeletePhoto(SelectedPhoto.Id);
            await LoadPhotosAsync();
        }

        private async void AddNewAction(object parameter)
        {
            try
            {
                string filename = string.Empty;
                string suffix = string.Empty;
                var selectedFilePath = fileDialogService.OpenFileDialog(out filename, out suffix);
                byte[] imageBytes = File.ReadAllBytes(selectedFilePath);
                PhotosRepo photosRepo = new PhotosRepo();
                await photosRepo.AddPhoto(imageBytes, filename, suffix);
                await LoadPhotosAsync();
                NewPhoto = new Foto();
            }
            catch (Exception ex)
            {

            }
            

        }

        private bool CanEdit(object parameter)
        {
            return SelectedPhoto != null;
        }

        private async void EditAction(object parameter)
        {
            if (!CanEdit(parameter)) return;

            try
            {
                string filename = string.Empty;
                string suffix = string.Empty;
                var selectedFilePath = fileDialogService.OpenFileDialog(out filename, out suffix);
                byte[] imageBytes = File.ReadAllBytes(selectedFilePath);
                PhotosRepo photosRepo = new PhotosRepo();
                await photosRepo.UpdatePhoto(SelectedPhoto.Id, imageBytes, filename, suffix);
                LoadPhotosAsync();
                NewPhoto = new Foto();
            }
            catch (Exception ex)
            {

            }
           
        }

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
                //|| foto.ListOfUserNamesWhichUseMeWithTheirRoles.ToString().Contains(_searchText, StringComparison.OrdinalIgnoreCase);
        }
        //FILTER/////////////////////////////////////////////////////////////////////
    }
}
