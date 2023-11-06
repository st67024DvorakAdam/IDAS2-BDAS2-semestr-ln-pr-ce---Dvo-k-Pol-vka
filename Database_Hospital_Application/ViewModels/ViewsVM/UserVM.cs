using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class UserVM : BaseViewModel
    {
        public ObservableCollection<User> _usersList;
        public ObservableCollection<Patient> _patientList;

        public ObservableCollection<User> UsersList
        {
            get { return _usersList; }
            set
            {
                _usersList = value;
                OnPropertyChange(nameof(UsersList));
            }
        }

        public ObservableCollection<Patient> PatientList
        {
            get { return _patientList;}
            set { _patientList = value;
                OnPropertyChange(nameof(PatientList));
            }
        }

        public UserVM()
        {
            LoadUsersAsync();
        }
        private async Task LoadUsersAsync()
        {
            UserRepo repo = new UserRepo();
            UsersList = await repo.GetAllUsersAsync();
            // PatientList = await repo.GetAllPatientsAsync();
        }
    }
}
