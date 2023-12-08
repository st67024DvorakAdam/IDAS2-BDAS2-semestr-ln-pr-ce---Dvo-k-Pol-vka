using Database_Hospital_Application.ViewModels.Dialogs.Edit;
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

namespace Database_Hospital_Application.Views.Lists.Dialogs.Contact
{
    /// <summary>
    /// Interakční logika pro EditContactDialog.xaml
    /// </summary>
    public partial class EditContactDialog : Window
    {
        public EditContactDialog(EditContactVM viewModel)
        {
            DataContext = viewModel;
            viewModel.ClosingRequest += (sender, e) => this.Close();
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

        private void ResetComboBox_Click(object sender, RoutedEventArgs e)
        {
            PatientCB.SelectedIndex = -1;
        }

        private void TextBox_PreviewTextInputForPhoneNumber(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string newText = textBox.Text + e.Text;
            e.Handled = newText.Length > 9 || !Regex.IsMatch(newText, "^[0-9]*$");
        }
    }
}
