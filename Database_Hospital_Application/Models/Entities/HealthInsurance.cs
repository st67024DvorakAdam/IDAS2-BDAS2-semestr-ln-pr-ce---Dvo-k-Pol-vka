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
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code {  get; set; }  //Zkratka

        public bool IsAllergic { get; set; }

        public bool IsSmoker { get; set; }
        public HealthInsurance(string name, string code)
        {
            Name = name;

            if (int.TryParse(code, out int parsedCode))
            {
                Code = parsedCode;
            }
            else
            {
                throw new ArgumentException("Hodnota 'code' není platné celé číslo.", nameof(code));
            }
        }

        public HealthInsurance() { }

        public override string? ToString()
        {
            return $"{Name} ({Code})";
        }
    }
}
