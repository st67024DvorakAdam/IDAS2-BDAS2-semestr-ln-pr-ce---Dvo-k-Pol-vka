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

namespace Database_Hospital_Application.Views.Lists.Dialogs.Address
{
    /// <summary>
    /// Interakční logika pro EditAddressDialog.xaml
    /// </summary>
    public partial class EditAddressDialog : Window
    {
        public EditAddressDialog()
        {
            InitializeComponent();
            var viewModel = new EditAddressVM(new Database_Hospital_Application.Models.Entities.Address());
            DataContext = viewModel;

            viewModel.ClosingRequest += (sender, e) => this.Close();
        }

        
    }
}
