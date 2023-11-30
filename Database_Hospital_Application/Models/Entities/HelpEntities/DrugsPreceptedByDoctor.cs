using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Entities.HelpEntities
{
    public class DrugsPreceptedByDoctor
    {
        public Drug _drug {  get; set; }
        public Patient _patient { get; set; }
        public Illness _illness { get; set; }
    }
}
