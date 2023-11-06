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


        public void AddPersonalMedicalHistory(PersonalMedicalHistory personalMedicalHistory)
        {

        }

        public void DeletePersonalMedicalHistory(int id)
        {

        }
        public void UpdatePersonalMedicalHistory(PersonalMedicalHistory personalMedicalHistory)
        {

        }

        public void DeleteAllPersonalMedicalHistory()
        {

        }
    }
}
