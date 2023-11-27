using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Enums;
using Database_Hospital_Application.ViewModels.ViewsVM;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
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
                        BirthNumber = row["RODNE_CISLO"].ToString(),
                        IdAddress = Convert.ToInt32(row["ADRESA_ID"]),
                        IdHealthInsurance = Convert.ToInt32(row["ZDRAVOTNI_POJISTOVNA_ID"])
                        
                        
                    };

                    //přidání nul pro RČ když začíná nulama
                    if (patient.BirthNumber.Length < 8) patient.BirthNumber = "0" + patient.BirthNumber;
                    if (patient.BirthNumber.Length < 9) patient.BirthNumber = "0" + patient.BirthNumber;
                    if (patient.BirthNumber.Length < 10) patient.BirthNumber = "0" + patient.BirthNumber;

                    patient.Sex = SexEnumParser.GetEnumFromString(row["POHLAVI"].ToString());
                    Patients.Add(patient);
                }
            }
            return Patients;
        }

        public async Task<int> AddPatient(Patient patient)
        {
            string commandText = "add_patient";
            var parameters = new List<OracleParameter>
        {
            new OracleParameter("p_jmeno", OracleDbType.Varchar2, patient.FirstName, ParameterDirection.Input),
            new OracleParameter("p_prijmeni", OracleDbType.Varchar2, patient.LastName, ParameterDirection.Input),
            new OracleParameter("p_rodne_cislo", OracleDbType.Varchar2, patient.BirthNumber, ParameterDirection.Input),
            new OracleParameter("p_pohlavi", OracleDbType.Varchar2, SexEnumParser.GetStringFromEnumCzech(patient.Sex), ParameterDirection.Input),
            new OracleParameter("p_zdravotni_pojistovna_id", OracleDbType.Int32, patient.IdHealthInsurance, ParameterDirection.Input),
            new OracleParameter("p_adresa_id", OracleDbType.Int32, patient.IdAddress, ParameterDirection.Input),
            new OracleParameter("p_id", OracleDbType.Int32, ParameterDirection.Output)
        };

            await dbTools.ExecuteNonQueryAsync(commandText, parameters);

            
            int newPatientId = Convert.ToInt32(parameters.Last().Value.ToString());

            return newPatientId;
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
                { "p_jmeno", patient.FirstName },
                { "p_prijmeni", patient.LastName },
                { "p_rodne_cislo", patient.BirthNumber },
                { "p_pohlavi", SexEnumParser.GetStringFromEnumCzech(patient.Sex) },
                { "p_zdravotni_pojistovna_id", patient.IdHealthInsurance },
                { "p_adresa_id", patient.IdAddress }
            };

            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }

        public async Task<Patient> GetPatientByBirthNumber(int birthNumber)
        {
            string commandText = "get_patient_by_birth_number";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "p_birth_number", birthNumber }
            };


            DataTable result = await dbTools.ExecuteCommandAsync(commandText, parameters);

            if (result.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = result.Rows[0];
            Patient patient = new Patient
            {
                Id = Convert.ToInt32(row["ID"]),
                FirstName = row["JMENO"].ToString(),
                LastName = row["PRIJMENI"].ToString(),
                BirthNumber = birthNumber.ToString(),
                Sex = SexEnumParser.GetEnumFromString(row["POHLAVI"].ToString()),
                IdAddress = Convert.ToInt32(row["ADRESA_ID"].ToString()),
                IdHealthInsurance  = Convert.ToInt32(row["ZDRAVOTNI_POJISTOVNA_ID"].ToString())
            };

            

            return patient;
        }




    }
}
