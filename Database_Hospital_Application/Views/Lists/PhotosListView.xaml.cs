using Database_Hospital_Application.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Database_Hospital_Application.Views.Lists
{
    /// <summary>
    /// Interakční logika pro PhotosListView.xaml
    /// </summary>
    public partial class PhotosListView : UserControl
    {
        public PhotosListView()
        {
            InitializeComponent();
        }
        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            var img = sender as Image;
            if (img != null && img.DataContext is Foto photo)
            {
                img.Source = photo.Image; 
            }
        }
    }
}
