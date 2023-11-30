using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Entities
{
    public class Illness
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Details { get; set; }
        public int MedicalCardId { get; set; }

        public Illness()
        {
        }
        


    }
}
