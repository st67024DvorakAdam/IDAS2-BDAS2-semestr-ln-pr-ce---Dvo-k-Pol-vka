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

namespace Database_Hospital_Application.Views.Lists.Dialogs.HealthInsurance
{
    /// <summary>
    /// Interakční logika pro EditInsuranceDialog.xaml
    /// </summary>
    public partial class EditInsuranceDialog : Window
    {
        public EditInsuranceDialog(EditInsuranceVM viewModel)
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
    }
}
