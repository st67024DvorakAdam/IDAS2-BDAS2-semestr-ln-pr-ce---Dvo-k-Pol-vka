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
        public int Employee_id { get; set; } //id zaměstnance (který je doktor), který lék předepsal
    }
}
