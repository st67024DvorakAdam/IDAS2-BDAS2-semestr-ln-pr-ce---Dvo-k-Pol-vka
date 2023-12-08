using Database_Hospital_Application.Models.Enums;
using Database_Hospital_Application.ViewModels.ViewsVM;
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
            UploadOwnPhotoButton.IsEnabled = false;
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

        private void TextBox_PreviewTextInputForBirthNumber(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string newText = textBox.Text + e.Text;
            e.Handled = newText.Length > 10 || !Regex.IsMatch(newText, "^[0-9]*$");
        }

        private void TextBox_PreviewTextInputForPhoneNumber(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string newText = textBox.Text + e.Text;
            e.Handled = newText.Length > 9 || !Regex.IsMatch(newText, "^[0-9]*$");
        }

        private void OwnPhtoCheckBox_Clicked(object sender, RoutedEventArgs e)
        {
            if (OwnPhtoCheckBox.IsChecked == true)
            {
                UploadOwnPhotoButton.IsEnabled = true;
            }
            else
            {
                UploadOwnPhotoButton.IsEnabled = false;
            }
        }

    }

}
