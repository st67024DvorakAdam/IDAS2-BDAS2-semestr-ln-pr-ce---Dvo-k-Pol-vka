using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.ViewModels.ViewsVM;
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
using System.Windows.Shapes;


namespace Database_Hospital_Application.Views
{
    /// <summary>
    /// Interakční logika pro ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : Window
    {
        private User _currentUser;
        public ProfileWindow(User user)
        {
            InitializeComponent();
            _currentUser = user;
            DataContext = new ProfileWindowViewModel(_currentUser);
        }
    }
}
