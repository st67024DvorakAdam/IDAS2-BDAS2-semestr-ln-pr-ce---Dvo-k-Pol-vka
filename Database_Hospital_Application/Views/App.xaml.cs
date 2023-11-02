using Database_Hospital_Application.ViewModels.ViewsVM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Database_Hospital_Application
{

    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {

            MainWindow = new LoginWindow
            {
                //DataContext = new LoginWindowViewModel()
            };

            MainWindow.Show();

            base.OnStartup(e);
        }


    }
}