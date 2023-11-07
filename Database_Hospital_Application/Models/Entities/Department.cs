using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdOfHeadOfDepartment { get; set; }

    }
}
