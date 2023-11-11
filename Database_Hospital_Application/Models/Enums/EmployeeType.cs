using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Enums
{
    public enum EmployeeType
    {
        [Description("Doktor")]
        Doctor = 1,

        [Description("Sestra")]
        Nurse = 2,

        [Description("Asistent")]
        Assistant = 3
    }


    public static class EmployeeTypeExtensions
    {
        public static EmployeeType GetEnumFromString(string input)
        {
            if (input.ToLower() == "doktor") return EmployeeType.Doctor;
            else if (input.ToLower() == "sestra") return EmployeeType.Nurse;
            else if (input.ToLower() == "asistent") return EmployeeType.Assistant;
            else throw new Exception("Neplatný výčtový typ EmployeeType");
        }

        public static string GetStringFormEnumEnglish(EmployeeType employeeType)
        {
            if (employeeType == EmployeeType.Doctor) return "Doctor";
            else if (employeeType == EmployeeType.Nurse) return "Nurse";
            else if (employeeType == EmployeeType.Assistant) return "Assistant";
            else return "Neurčený typ zaměstnance - EnumType";
        }

    }

}
