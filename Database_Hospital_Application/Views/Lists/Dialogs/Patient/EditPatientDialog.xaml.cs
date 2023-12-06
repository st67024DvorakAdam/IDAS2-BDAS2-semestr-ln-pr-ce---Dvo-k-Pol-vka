using Database_Hospital_Application.Models.Enums;
using Database_Hospital_Application.ViewModels.Dialogs.Edit;
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

namespace Database_Hospital_Application.Views.Lists.Dialogs.Patient
{
    /// <summary>
    /// Interakční logika pro EditPatientDialog.xaml
    /// </summary>
    public partial class EditPatientDialog : Window
    {
        public EditPatientDialog(EditPatientVM viewModel)
        {
            DataContext = viewModel;
            viewModel.ClosingRequest += (sender, e) => this.Close();
            InitializeComponent();
        }
        private void TextBox_PreviewTextInputForBirthNumber(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string newText = textBox.Text + e.Text;
            e.Handled = newText.Length > 10 || !Regex.IsMatch(newText, "^[0-9]*$");
        }
    }

    public class SexToIndexConverter2 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((SexEnum)value == SexEnum.Muž) ? 0 : 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((int)value == 0) ? SexEnum.Muž : SexEnum.Žena;
        }
    }
}
