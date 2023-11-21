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

namespace Database_Hospital_Application.Views.Lists.Dialogs.MedicalCard
{
    /// <summary>
    /// Interakční logika pro MedicalCardDialog.xaml
    /// </summary>
    public partial class EditMedicalCardDialog : Window
    {
        public EditMedicalCardDialog(EditMedicalCardVM viewModel)
        {

            DataContext = viewModel;
            viewModel.ClosingRequest += (sender, e) => this.Close();
            InitializeComponent();
        }

    }

    public class BoolToIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value == true) ? 0 : 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((int)value == 0) ? true : false;
        }
    }

}
