using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Repositories
{
    public class PatientRepo
    {
        private DatabaseTools.DatabaseTools dbTools;

        public ObservableCollection<Patient> Patients { get; set; }

        public PatientRepo()
        {
            dbTools = new DatabaseTools.DatabaseTools();
            Patients = new ObservableCollection<Patient>();
        }

        public async Task<ObservableCollection<Patient>> GetAllPatientsAsync()
        {
            string commandText = "get_all_patients";
            DataTable result = await dbTools.ExecuteCommandAsync(commandText, null);

            Patients.Clear(); 

            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    Patient patient = new Patient
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        FirstName = row["JMENO"].ToString(),
                        LastName = row["PRIJMENI"].ToString(),
                        BirthNumber = Convert.ToInt64(row["RODNE_CISLO"])
                    };
                    patient.Sex = SexEnumParser.GetEnumFromString(row["POHLAVI"].ToString());
                    Patients.Add(patient);
                }
            }
            return Patients;
        }

        public async Task AddPatient(Patient patient)
        {
            string commandText = "add_patient";
            var parameters = new Dictionary<string, object>
            {
                { "p_first_name", patient.FirstName },
                { "p_last_name", patient.LastName },
                { "p_birth_number", patient.BirthNumber },
                { "p_sex", patient.Sex.ToString() }
            };

            await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }

        public async Task<int> DeletePatient(int id)
        {
            string commandText = "delete_patient_by_id";
            var parameters = new Dictionary<string, object>
            {
                { "p_id", id }
            };

            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }

        public async Task<int> UpdatePatient(Patient patient)
        {
            string commandText = "update_patient";

            var parameters = new Dictionary<string, object>
            {
                { "p_id", patient.Id },
                { "p_first_name", patient.FirstName },
                { "p_last_name", patient.LastName },
                { "p_birth_number", patient.BirthNumber },
                { "p_sex", patient.Sex.ToString() }
            };

            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }

        
    }
}
