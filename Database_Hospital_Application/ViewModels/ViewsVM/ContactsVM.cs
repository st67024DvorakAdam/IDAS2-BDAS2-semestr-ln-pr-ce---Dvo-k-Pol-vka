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
    public class ContactsVM : BaseViewModel
    {
        private ObservableCollection<Contact> _contactsList;

        public ObservableCollection<Contact> ContactsList
        {
            get { return _contactsList; }
            set
            {
                _contactsList = value;
                OnPropertyChange(nameof(ContactsList));
            }
        }


        public ContactsVM()
        {
            LoadContactsAsync();
        }
        private async Task LoadContactsAsync()
        {
            ContactRepo repo = new ContactRepo();
            ContactsList = await repo.GetAllContactsAsync();
        }
    }
}
