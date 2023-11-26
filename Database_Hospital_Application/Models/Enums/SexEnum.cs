using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Enums
{
    public enum SexEnum
    {
        [Description("muž")]
        Muž,
        [Description("žena")]
        Žena
    }

    public static class SexEnumParser{
        public static SexEnum GetEnumFromString(string input)
        {
            if (input.ToLower() == "muž" || input.ToLower() == "male") return SexEnum.Muž;
            else if (input.ToLower() == "žena" || input.ToLower() == "female") return SexEnum.Žena;
            else throw new Exception("Neplatný výčtový typ SexEnum");
        }

        public static string GetStringFromEnumEnglish(SexEnum? sex)
        {
            if (sex == SexEnum.Muž) return "Male";
            else if (sex == SexEnum.Žena) return "Female";
            else return "Neodpovídá žádnému pohaví";
        }

        public static string GetStringFromEnumCzech(SexEnum? sex)
        {
            if (sex == SexEnum.Muž) return "MUŽ";
            else if (sex == SexEnum.Žena) return "ŽENA";
            else return "Neodpovídá žádnému pohaví";
        }
    }
}
