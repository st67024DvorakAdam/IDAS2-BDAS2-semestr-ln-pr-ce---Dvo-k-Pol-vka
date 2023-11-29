using Database_Hospital_Application.Models.DatabaseTools;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Enums;
using Database_Hospital_Application.ViewModels.ViewsVM;
using Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM.PatientViewVM;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

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

        public async Task<Patient> GetPatientByBirthNumber(string birthNumber)
        {
            
            string commandText = "get_patient_info_by_birth_number";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "p_birth_number", Convert.ToInt64(birthNumber) }
            };

            DataTable result = await dbTools.ExecuteCommandAsync(commandText, parameters);

            if (result.Rows.Count == 0)
            {
                
                return null;
            }

            DataRow row = result.Rows[0];
            Patient patient = new Patient
            {
                Id = Convert.ToInt32(row["PACIENT_ID"]),
                FirstName = row["JMENO"].ToString(),
                LastName = row["PRIJMENI"].ToString(),
                BirthNumber = row["RODNE_CISLO"].ToString(),
                Sex = SexEnumParser.GetEnumFromString(row["POHLAVI"].ToString()),

                Contact = new Contact
                {
                    Email = row["email"].ToString(),
                    PhoneNumber = Convert.ToInt32(row["TELEFON"].ToString()),
                },
                    
                Address = new Address
                {
                    Street = row["ULICE"].ToString(),
                    City = row["MESTO"].ToString(),
                    HouseNumber = Convert.ToInt32(row["CISLO_POPISNE"].ToString()),
                    Country = row["STAT"].ToString(),
                    ZipCode = Convert.ToInt32(row["PSC"].ToString()),
                },

                
                HealthInsurance = new HealthInsurance
                {
                    Name = row["POJISTOVNA_NAZEV"].ToString(),
                    Code = Convert.ToInt32(row["POJISTOVNA_ZKRATKA"].ToString()),
                }
            };

            return patient;
        }

        public async Task<ObservableCollection<DataActualIllness>> GetActualIllnessByPatientIdAsync(int patientId)
        {
            string commandText = "get_patient_illnesses_meds";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("p_patient_id", OracleDbType.Int32, patientId, ParameterDirection.Input)
            };

            DataTable result = await dbTools.ExecuteCommandAsyncOracle(commandText, parameters);

            var illnesses = new ObservableCollection<DataActualIllness>();

            foreach (DataRow row in result.Rows)
            {
                var illness = new Illness
                {
                    Id = Convert.ToInt32(row["NEMOC_ID"]),
                    Name = row["NEMOC"].ToString()
                };

                Drug? drug = null;
                if (row["LEK_ID"] != DBNull.Value && row["LEK"] != DBNull.Value && row["DAVKOVANI"] != DBNull.Value)
                {
                    drug = new Drug
                    {
                        Id = Convert.ToInt32(row["LEK_ID"]),
                        Name = row["LEK"].ToString(),
                        Dosage = Convert.ToInt32(row["DAVKOVANI"])
                    };
                }

                var dataActualIllness = new DataActualIllness
                {
                    Illness = illness,
                    PrescriptedPills = drug
                };

                illnesses.Add(dataActualIllness);
            }

            return illnesses;
        }


        //načte procentuální podíl kuřáků v celé nemocnici
        public async Task<double> GetPerceteOfSmokersAsync()
        {
            string commandText = "SELECT ProcentoKuraku FROM DUAL";

            double output = 0;
            DatabaseConnection dbConnection = new DatabaseConnection();

            OracleConnection conn = dbConnection.GetConnection();
            await conn.OpenAsync();

            using (OracleCommand command = new OracleCommand(commandText, conn))
            {
                // Zde už není potřeba přidávat žádné parametry, protože funkce nic nečeká
                object result = await command.ExecuteScalarAsync();

                if (result != null)
                {
                    output = Convert.ToDouble(result);
                }
            }




            return output;

        }





    }
}
