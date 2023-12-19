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
using System.Windows.Shapes;

namespace Database_Hospital_Application.Views
{
    /// <summary>
    /// Interakční logika pro VerificationView.xaml
    /// </summary>
    public partial class VerificationView : Window
    {
        public VerificationView(VerifyVM viewModel)
        {
            InitializeComponent();
            viewModel.CloseRequested += CloseWindow;
            DataContext = viewModel;
        }

        private void CloseWindow()
        {
            this.Close();
        }

        private void TextBox_PreviewTextInputForCode(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string newText = textBox.Text + e.Text;
            e.Handled = newText.Length > 4 || !Regex.IsMatch(newText, "^[0-9]*$");
        }
    }
}
