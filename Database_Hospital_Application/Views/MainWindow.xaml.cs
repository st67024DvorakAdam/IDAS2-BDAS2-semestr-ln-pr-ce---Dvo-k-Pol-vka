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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Database_Hospital_Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }

    public static class PasswordBoxHelper
    {
        public static readonly DependencyProperty BoundPassword =
            DependencyProperty.RegisterAttached("BoundPassword", typeof(string), typeof(PasswordBoxHelper), new PropertyMetadata(string.Empty, OnBoundPasswordChanged));

        public static string GetBoundPassword(DependencyObject d)
        {
            return (string)d.GetValue(BoundPassword);
        }

        public static void SetBoundPassword(DependencyObject d, string value)
        {
            d.SetValue(BoundPassword, value);
        }

        private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var box = d as PasswordBox;
            if (box != null)
            {
                box.PasswordChanged -= PasswordBox_PasswordChanged;
                box.Password = GetBoundPassword(d);
                box.PasswordChanged += PasswordBox_PasswordChanged;
            }
        }

        private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var box = sender as PasswordBox;
            SetBoundPassword(box, box.Password);
        }

      
    }

}