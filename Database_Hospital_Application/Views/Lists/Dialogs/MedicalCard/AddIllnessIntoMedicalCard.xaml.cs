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

namespace Database_Hospital_Application.Views.Lists.Dialogs.MedicalCard
{
    /// <summary>
    /// Interakční logika pro AddIllnessIntoMedicalCard.xaml
    /// </summary>
    public partial class AddIllnessIntoMedicalCard : Window
    {
        public AddIllnessIntoMedicalCard(EditMedicalCardVM viewModel)
        {
            DataContext = viewModel;
            viewModel.ClosingRequest += (sender, e) => this.Close();
            InitializeComponent();
        }
    }
}
