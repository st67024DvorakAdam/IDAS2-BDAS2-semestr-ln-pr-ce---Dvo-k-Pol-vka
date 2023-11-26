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

namespace Database_Hospital_Application.Views.Lists.Dialogs.Employee
{
    /// <summary>
    /// Interakční logika proEmployeeDialog.xaml
    /// </summary>
    public partial class EditEmployeeDialog : Window
    {
        public EditEmployeeDialog(EditEmployeeVM viewModel)
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

    }

    public class SexToIndexConverter : IValueConverter
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
