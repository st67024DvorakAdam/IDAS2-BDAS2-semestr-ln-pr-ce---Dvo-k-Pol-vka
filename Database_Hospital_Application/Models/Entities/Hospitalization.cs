using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Entities
{
    public class Hospitalization
    {
        public int Id { get; set; }
        public OracleDate DateIn { get; set; }
        public OracleDate? DateOut { get; set; }
        public string? Details { get; set; }
        public int PatientId { get; set; }
        public int DepartmentId { get; set; }

        

        public Hospitalization() { }

        
    }
}
