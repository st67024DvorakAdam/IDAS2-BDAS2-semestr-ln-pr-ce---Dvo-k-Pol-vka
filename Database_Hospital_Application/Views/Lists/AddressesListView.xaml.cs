using Database_Hospital_Application.ViewModels.ViewsVM;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Database_Hospital_Application.Views.Lists
{
    /// <summary>
    /// Interakční logika pro AddressesListView.xaml
    /// </summary>
    public partial class AddressesListView : UserControl
    {
        public AddressesListView()
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


        private void TextBox_PreviewTextInputForCountry(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            // Získej text v TextBoxu včetně nově zadávaného znaku
            string newText = textBox.Text + e.Text;

            // Omez délku textu na 3 znaky a zkontroluj, zda všechny znaky jsou písmena
            e.Handled = newText.Length > 3 || !Regex.IsMatch(newText, "^[a-zA-Z]*$");
        }

        private void TextBox_PreviewTextInputForPostalCode(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string newText = textBox.Text + e.Text;
            e.Handled = newText.Length > 5 || !Regex.IsMatch(newText, "^[0-9]*$");
        }

        private void TextBox_PreviewTextInputForHouseNumber(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string newText = textBox.Text + e.Text;
            e.Handled = newText.Length > 7 || !Regex.IsMatch(newText, "^[0-9]*$");
        }

    }
}
