using Database_Hospital_Application.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.ViewModels.EntitiesVM
{
    public class ContactViewModel
    {
        // MODEL
        private readonly Contact contact;
        // TODO

        // VIEWMODEL
        public ContactViewModel(Contact contact)
        {
            this.contact = contact;
        }
    }
}
