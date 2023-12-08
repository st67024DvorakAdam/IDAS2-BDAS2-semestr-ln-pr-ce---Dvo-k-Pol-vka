using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Entities
{
    public class PersonalMedicalHistory //osobní anamnéza
    {
        public int? Id { get; set; }
        public string Description {  get; set; } //Záznam
        public int? IdOfPatient {  get; set; }
        public long? BirthNumberOfPatient { get; set; }

        public PersonalMedicalHistory(string description, int patientId) {
            Description = description;
            IdOfPatient = patientId;
        }

        public PersonalMedicalHistory() { }
    }

    public static class PersonalMedicalHistoryValidator
    {
        public static bool IsDescriptionAndPatientFilled(PersonalMedicalHistory personalMedicalHistory)
        {
            return personalMedicalHistory != null
                && !string.IsNullOrEmpty(personalMedicalHistory.Description)
                && !string.IsNullOrEmpty(personalMedicalHistory.IdOfPatient.ToString()) && personalMedicalHistory.IdOfPatient != null && personalMedicalHistory.IdOfPatient != 0;
        }
    }
}
