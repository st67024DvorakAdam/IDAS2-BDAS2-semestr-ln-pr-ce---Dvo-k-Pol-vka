using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Entities.HelpEntities
{
    public class DosageForHospitalizated
    {
        public Drug _drug {  get; set; }
        public string NameOfDoctor { get; set; } //FirstName + LastName
        public Illness _illness { get; set; }
        public Patient _patient { get; set; }
        public Department _department { get; set; }

    }
}
