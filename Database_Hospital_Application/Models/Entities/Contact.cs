using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Entities
{
    public class Contact
    {
        public int Id { get; }
        public string Email {  get; set; }
        public int PhoneNumber {  get; set; }
    }
}
