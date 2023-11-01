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
        Male,
        [Description("žena")]
        Female
    }

    public static class SexEnumParser{
        public static SexEnum GetEnumFromString(string input)
        {
            if (input.ToLower() == "muž") return SexEnum.Male;
            else if (input.ToLower() == "žena") return SexEnum.Female;
            else throw new Exception("Neplatný výčtový typ SexEnum");
        }
    }
}
