using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Entities
{
    public class PerformedProcedure //Provedený zákrok
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsCoveredByInsurence { get; set; }
        public int IdOfPatient { get; set; }
        public long BirthNumberOfPatient{get;set;}

        public string? IsCoveredByInsurenceString { get; set; }
    }
}
