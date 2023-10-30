using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Entities
{
    public class HealthInsurance  //Zdravotní pojišťovna
    {
        public int Id { get; }
        public string Name { get; set; }
        public int Code {  get; set; }  //Zkratka
    }
}
