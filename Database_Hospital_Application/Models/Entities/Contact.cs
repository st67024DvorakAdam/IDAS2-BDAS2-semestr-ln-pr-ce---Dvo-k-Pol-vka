using Database_Hospital_Application.ViewModels.ViewsVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        public string Email {  get; set; }
        public int PhoneNumber {  get; set; }
        public int? IdOfPatient { get; set; }
        public int? IdOfEmployee { get; set; }


    }
}
