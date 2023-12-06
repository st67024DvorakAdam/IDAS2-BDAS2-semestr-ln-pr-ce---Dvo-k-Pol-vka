using Database_Hospital_Application.ViewModels.Dialogs.Edit.Doctor;
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
    /// Interakční logika pro MakeProcedureView.xaml
    /// </summary>
    public partial class MakeProcedureView : Window
    {
        public MakeProcedureView(MakeProcedureVM viewModel)
        {
            InitializeComponent();
            viewModel.CloseRequested += CloseWindow;
            DataContext = viewModel;
        }

        private void CloseWindow()
        {
            this.Close();
        }


        private void TextBox_PreviewTextInputForPrice(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string newText = textBox.Text + e.Text;
            e.Handled = newText.Length > 9 || !Regex.IsMatch(newText, "^[0-9]*$");
        }
    }
}
