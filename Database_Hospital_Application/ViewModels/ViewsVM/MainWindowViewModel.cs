using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class MainWindowViewModel : BaseViewModel
    {
        public BaseViewModel CurrentVM { get; }

        public MainWindowViewModel() 
        { 
            CurrentVM = new MainWindowViewModel();
        }
    }
}
