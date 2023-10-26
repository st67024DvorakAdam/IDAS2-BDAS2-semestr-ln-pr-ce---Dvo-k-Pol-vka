using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Entities
{
    public class Address
    {
        public int Id { get; }
        public string Street { get; set; }
        public string City { get; set; }
        public int HouseNumber {  get; set; }
        public string Country {  get; set; } 
        public int ZipCode {  get; set; }
    }
}
