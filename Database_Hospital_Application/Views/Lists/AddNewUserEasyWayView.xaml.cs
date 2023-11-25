using Database_Hospital_Application.Models.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interakční logika pro AddNewUserEasyWayView.xaml
    /// </summary>
    public partial class AddNewUserEasyWayView : UserControl
    {
        public AddNewUserEasyWayView()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            return !Regex.IsMatch(text, "[^0-9]");
        }

        private void ResetComboBox2_Click(object sender, RoutedEventArgs e)
        {
            EmployeeCB.SelectedIndex = -1;
        }
    }

}
