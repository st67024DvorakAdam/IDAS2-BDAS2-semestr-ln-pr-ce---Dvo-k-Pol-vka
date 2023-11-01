using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Enums
{
    public enum RoleEnum
    {
        [Description("Administrátor")]
        Admin = 1,

        [Description("Doktor")]
        Doctor = 2,

        [Description("Sestra")]
        Nurse = 3,

        [Description("Asistent")]
        Assistant = 4,

        [Description("Host bez přihlášení")]
        Guest = 5
    }

    public static class RoleExtensions
    {
        public static string GetRoleDescription(int roleNumber)
        {
            foreach (RoleEnum role in Enum.GetValues(typeof(RoleEnum)))
            {
                if ((int)role == roleNumber)
                {
                    return GetEnumDescription(role);
                }
            }
            return "Neznámá role";
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }
    }
}
