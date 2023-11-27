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

        public Contact(string email, string phoneNumber, int? idOfPatient, int? idOfEmployee)
        {
            Email = email;
            IdOfPatient = idOfPatient;
            IdOfEmployee = idOfEmployee;

            
            if (int.TryParse(phoneNumber, out int parsedPhoneNumber))
            {
                PhoneNumber = parsedPhoneNumber;
            }
            else
            {
                throw new ArgumentException("Hodnota 'phoneNumber' není platné celé číslo.", nameof(phoneNumber));
            }
        }

        public Contact() { }   
    }
}
