using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int HouseNumber {  get; set; }
        public string Country {  get; set; } 
        public int ZipCode {  get; set; }

        public Address(string street, string city, string houseNumber, string country, string zipCode)
        {
            Street = street;
            City = city;
            Country = country;

            
            if (int.TryParse(houseNumber, out int parsedHouseNumber))
            {
                HouseNumber = parsedHouseNumber;
            }
            else
            {
                throw new ArgumentException("Hodnota 'houseNumber' není platné celé číslo.", nameof(houseNumber));
            }

            if (int.TryParse(zipCode, out int parsedZipCode))
            {
                ZipCode = parsedZipCode;
            }
            else
            {
                throw new ArgumentException("Hodnota 'zipCode' není platné celé číslo.", nameof(zipCode));
            }
        }

        public Address() { }

        public override string? ToString()
        {
            return $"{Street} {HouseNumber}, {City}, {Country} {ZipCode}";
        }
    }
}
