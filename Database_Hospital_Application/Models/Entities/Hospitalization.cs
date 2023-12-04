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
        public DateTime DateIn { get; set; }
        public DateTime? DateOut { get; set; }
        public string? Details { get; set; }
        public int? PatientId { get; set; }
        public int? DepartmentId { get; set; }
        public string? FormattedDateIn { get; set; }
        public string? FormattedDateOut { get; set; }
        public string? PatientName { get; set; }
        public string? DepartmentName { get; set; }
        public long? PatientBirthbumber { get; set; }

        public Hospitalization() { }

        public Hospitalization(DateTime dateIn, string details, int patientId, int departmentId) {
            DateIn = dateIn;
            Details = details;
            PatientId = patientId;
            DepartmentId = departmentId;
        }

    }
}
