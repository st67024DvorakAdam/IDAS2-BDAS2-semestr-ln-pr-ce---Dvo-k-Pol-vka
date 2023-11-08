using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Database_Hospital_Application.Models.Tools
{
    public class DialogService : IDialogService
    {
        public bool? ShowDialog(Type dialogType, object viewModel)
        {
            var dialog = (Window)Activator.CreateInstance(dialogType);
            if (dialog == null)
                throw new InvalidOperationException("Dialog nemůže být otevřen.");

            dialog.DataContext = viewModel;

            return dialog.ShowDialog();
        }
    }
}
