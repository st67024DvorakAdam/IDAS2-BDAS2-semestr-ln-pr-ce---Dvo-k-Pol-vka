using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Entities
{
    public class IllnessDrugConnection
    {
        public Illness _illness { get; set; }
        public Drug _drug { get; set; }

        public IllnessDrugConnection()
        {
            _illness = new Illness();
            _drug = new Drug();
        }
    }
}
