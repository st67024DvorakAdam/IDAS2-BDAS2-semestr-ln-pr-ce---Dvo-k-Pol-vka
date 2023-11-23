using Database_Hospital_Application.ViewModels.Dialogs.Edit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Database_Hospital_Application.Views.Lists.Dialogs.CurrentUser
{
    /// <summary>
    /// Interakční logika pro EditCurrentUserDialog.xaml
    /// </summary>
    public partial class EditCurrentUserDialog : Window
    {
        public EditCurrentUserDialog(EditCurrentUserVM viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.ClosingRequest += (sender, e) => this.Close();
            
        }

        private void NewAgainPasswordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NewAgainPasswordTextBox.Text != NewPasswordTextBox.Text)
            {
                PasswordControl.Foreground = Brushes.Red;
                ConfirmButton.IsEnabled = false;
            }
            else
            {
                PasswordControl.Foreground = Brushes.Transparent;
                ConfirmButton.IsEnabled = true;
            }
        }

        private void NewPasswordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NewAgainPasswordTextBox.Text != NewPasswordTextBox.Text)
            {
                PasswordControl.Foreground = Brushes.Red;
                ConfirmButton.IsEnabled = false;
            }
            else
            {
                PasswordControl.Foreground = Brushes.Transparent;
                ConfirmButton.IsEnabled = true;
            }
        }
    }
}
