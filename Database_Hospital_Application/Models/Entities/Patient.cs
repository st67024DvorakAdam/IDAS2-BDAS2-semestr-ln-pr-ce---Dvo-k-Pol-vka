using Database_Hospital_Application.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthNumber { get; set; }
        public SexEnum Sex { get; set; }
        public int IdAddress { get; set; }
        public int IdHealthInsurance { get; set; }

        public Address? Address { get; set; }
        public HealthInsurance? HealthInsurance { get; set; }
        public Contact? Contact { get; set; }

        public MedicalCard? MedicalCard { get; set; }

        public Patient(string firstName, string lastName, string birthNumber, string sex, string idAddress, string idHealthInsurance)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthNumber = birthNumber;
            Sex = SexEnumParser.GetEnumFromString(sex);

            if (int.TryParse(idAddress, out int parsedIdAddress))
            {
                IdAddress = parsedIdAddress;
            }
            else
            {
                throw new ArgumentException("Hodnota 'idAddress' není platné celé číslo.", nameof(idAddress));
            }

            if (int.TryParse(idHealthInsurance, out int parsedIdHealthInsurance))
            {
                IdHealthInsurance = parsedIdHealthInsurance;
            }
            else
            {
                throw new ArgumentException("Hodnota 'idHealthInsurance' není platné celé číslo.", nameof(idHealthInsurance));
            }
        }

        public Patient() { }

        public string completeBirthNumber(string birthNumber)
        {
            for (int i = 0; i < 9; i++)
            {
                if(birthNumber.Length < 10)
                {
                    birthNumber = "0" + birthNumber;
                }
            }
            return birthNumber;
        }
    }
}
