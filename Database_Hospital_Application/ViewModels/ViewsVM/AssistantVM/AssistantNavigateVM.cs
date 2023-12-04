using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.ViewModels.ViewsVM.NurseVM;
using Database_Hospital_Application.Views.Assistant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.ViewsVM.AssistantVM
{
    public class AssistantNavigateVM:BaseViewModel
    {
        private object _currentView;
        public User _currentUser;

        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                if (_currentUser != value)
                {
                    _currentUser = value;
                    OnPropertyChange(nameof(CurrentUser));
                }
            }
        }

        public object CurrentView
        {
            get => _currentView;
            set
            {
                if (_currentView != value)
                {
                    _currentView = value;
                    OnPropertyChange(nameof(CurrentView));
                }
            }
        } 

        public ICommand ProfileCommand { get; }
        public ICommand GeneralInfoCommand { get; }


        private void Profile(object obj) => CurrentView = new CurrUserVM(CurrentUser);
        private void GeneralInfo(object obj) => CurrentView = new GeneralInfoVM();

        public AssistantNavigateVM(User user)
        {
            //defaultní pohled
            CurrentUser = user;
            CurrentView = new CurrUserVM(CurrentUser);


            //Inicializace příkazů
            ProfileCommand = new RelayCommand(Profile);
            GeneralInfoCommand = new RelayCommand(GeneralInfo);
        }
    }
}
