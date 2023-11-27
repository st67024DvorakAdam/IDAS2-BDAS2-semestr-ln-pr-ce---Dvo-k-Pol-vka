using Database_Hospital_Application.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Repositories
{
    public class PersonalMedicalHistoriesRepo
    {
        private DatabaseTools.DatabaseTools dbTools = new DatabaseTools.DatabaseTools();

        public ObservableCollection<PersonalMedicalHistory> personalMedicalHistories { get; set; }

        public PersonalMedicalHistoriesRepo()
        {
            personalMedicalHistories = new ObservableCollection<PersonalMedicalHistory>();
        }

        public async Task<ObservableCollection<PersonalMedicalHistory>> GetAllPersonalMedicalHistoriesAsync()
        {
            string commandText = "get_all_personal_medical_histories";
            DataTable result = await dbTools.ExecuteCommandAsync(commandText, null);

            if (result.Rows.Count > 0)
            {
                personalMedicalHistories.Clear(); 

                foreach (DataRow row in result.Rows)
                {
                    PersonalMedicalHistory personalMedicalHistory = new PersonalMedicalHistory
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        Description = row["ZAZNAM"].ToString(),
                        IdOfPatient = Convert.ToInt32(row["PACIENT_ID"]),
                        BirthNumberOfPatient = Convert.ToInt64(row["RODNE_CISLO"])
                    };
                    personalMedicalHistories.Add(personalMedicalHistory);
                }
            }

            return personalMedicalHistories;
        }

        public async Task<ObservableCollection<PersonalMedicalHistory>> GetPersonalMedicalHistoryByPatientIdAsync(int id)
        {
            string commandText = "get_personal_history_by_patient_id";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "p_patient_id", id }
            };

            DataTable result = await dbTools.ExecuteCommandAsync(commandText, parameters);

            ObservableCollection<PersonalMedicalHistory> personalHistories = new ObservableCollection<PersonalMedicalHistory>();

            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    PersonalMedicalHistory personalMedicalHistory = new PersonalMedicalHistory
                    {
                        Description = row["ZAZNAM"].ToString()
                    };

                    personalHistories.Add(personalMedicalHistory);
                }
            }

            return personalHistories;
        }



        public async Task AddPersonalMedicalHistory(PersonalMedicalHistory personalMedicalHistory)
        {
            string commandText = "add_personal_medical_history";
            var parameters = new Dictionary<string, object>
            {
                { "p_zaznam", personalMedicalHistory.Description },
                { "p_pacient_id", personalMedicalHistory.IdOfPatient}
            };

            await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }

        public async Task<int> DeletePersonalMedicalHistory(int id)
        {
            string commandText = "delete_personal_medical_history_by_id";
            var parameters = new Dictionary<string, object>
            {
                { "p_id", id }
            };

            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }
        public async Task<int> UpdatePersonalMedicalHistory(PersonalMedicalHistory personalMedicalHistory)
        {
            string commandText = "update_personal_medical_history";

            var parameters = new Dictionary<string, object>
            {
                {"p_id", personalMedicalHistory.Id },
                { "p_zaznam", personalMedicalHistory.Description },
                { "p_pacient_id", personalMedicalHistory.IdOfPatient}
            };

            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }

        public void DeleteAllPersonalMedicalHistory()
        {

        }
    }
}
