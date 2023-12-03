using Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM.PatientViewVM;
using System;
using System.Collections.Generic;
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
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]*$");
        }

        private void TextBox_PreviewTextInputForPostalCode(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string newText = textBox.Text + e.Text;
            e.Handled = newText.Length > 5 || !Regex.IsMatch(newText, "^[0-9]*$");
        }

        private void TextBox_PreviewTextInputForPhoneNumber(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string newText = textBox.Text + e.Text;
            e.Handled = newText.Length > 10 || !Regex.IsMatch(newText, "^[0-9]*$");
        }
    }
}
