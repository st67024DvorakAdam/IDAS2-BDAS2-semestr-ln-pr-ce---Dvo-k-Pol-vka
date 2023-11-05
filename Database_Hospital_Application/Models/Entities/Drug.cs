using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Entities
{
    public class Drug //užitý lék
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Dosage { get; set; } //dávkování
        public int Doctor_id { get; set; } //id zaměstnance (s vlastností EmployeeType = Doctor), který lék předepsal
    }
}
