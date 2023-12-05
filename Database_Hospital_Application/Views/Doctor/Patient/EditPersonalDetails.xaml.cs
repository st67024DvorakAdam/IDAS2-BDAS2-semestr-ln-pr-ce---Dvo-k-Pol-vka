using Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM.PatientViewVM;
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
using System.Windows.Shapes;

namespace Database_Hospital_Application.Views.Doctor.Patient
{
    /// <summary>
    /// Interakční logika pro EditPersonalDetails.xaml
    /// </summary>
    public partial class EditPersonalDetails : Window
    {
        public EditPersonalDetails(PersonalDetailsVM viewModel)
        {
            InitializeComponent();
            viewModel.CloseRequested += CloseWindow;
            DataContext = viewModel;
        }

        private void CloseWindow()
        {
            this.Close();
        }

        private void TextBox_PreviewTextInputForBirthNumber(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string newText = textBox.Text + e.Text;
            e.Handled = newText.Length > 10 || !Regex.IsMatch(newText, "^[0-9]*$");
        }

        private void TextBox_PreviewTextInputForHouseNumber(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string newText = textBox.Text + e.Text;
            e.Handled = newText.Length > 7 || !Regex.IsMatch(newText, "^[0-9]*$");
        }

        private void TextBox_PreviewTextInputForPostalCode(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string newText = textBox.Text + e.Text;
            e.Handled = newText.Length > 5 || !Regex.IsMatch(newText, "^[0-9]*$");
        }

        private void TextBox_PreviewTextInputForInsuranceCode(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string newText = textBox.Text + e.Text;
            e.Handled = newText.Length > 3 || !Regex.IsMatch(newText, "^[0-9]*$");
        }

        private void TextBox_PreviewTextInputForPhoneNumber(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string newText = textBox.Text + e.Text;
            e.Handled = newText.Length > 10 || !Regex.IsMatch(newText, "^[0-9]*$");
        }
    }

    public class StringToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                return stringValue.Equals("Ano", StringComparison.OrdinalIgnoreCase);
            }

            return false; // Default value, if not a string or not "Ano"
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? "Ano" : "Ne";
            }

            return "Ne"; // Default value, if not a bool
        }
    }
}
