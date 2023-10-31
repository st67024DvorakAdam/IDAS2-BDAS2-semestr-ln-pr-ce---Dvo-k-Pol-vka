﻿using Database_Hospital_Application.ViewModels.ViewsVM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Database_Hospital_Application
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            

           

           MainWindow = new MainWindow
           {
               //DataContext = new MainWindowViewModel()
           };

            MainWindow.Show();

            base.OnStartup(e);
        }


    }
}