using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
}
