using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Database_Hospital_Application.Models.Tools
{
    public class OpenFileDialogService
    {
        public string OpenFileDialog(out string nazev_souboru, out string pripona)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.png";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;

            nazev_souboru = string.Empty;
            pripona = string.Empty;

            if (openFileDialog.ShowDialog() == true)
            {
                nazev_souboru = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                pripona = Path.GetExtension(openFileDialog.FileName);
                return openFileDialog.FileName;
            }

            return string.Empty;
        }
    }
}
