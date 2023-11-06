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
        public int BirthNumber { get; set; }
        public SexEnum? Sex { get; set; }
        public HealthInsurance? HealthInsurer { get; set; }
        public Address? Address { get; set; }
        public Contact? Contact {  get; set; }
        public PerformedProcedure? _PerformedProcedure { get; set; }
    }
}
