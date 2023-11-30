using Database_Hospital_Application.Models.Entities;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Database_Hospital_Application.Models.Repositories
{
    public class IllnessesRepo
    {
        private DatabaseTools.DatabaseTools dbTools;

        public IllnessesRepo()
        {
            dbTools = new DatabaseTools.DatabaseTools();
            illnesses = new ObservableCollection<Illness>();
        }

        public ObservableCollection<Illness> illnesses { get; set; }

        public async Task<ObservableCollection<Illness>> GetIllnessesAsync()
        {
            string commandText = "get_all_illnesses";

            DataTable dataTable = await dbTools.ExecuteCommandAsync(commandText);

            if (dataTable.Rows.Count > 0)
            {
                illnesses.Clear();

                foreach (DataRow row in dataTable.Rows)
                {
                    Illness illness = new Illness
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        Name = row["NAZEV"].ToString(),
                        Details = row.IsNull("PODROBNOSTI") ? "" : row["PODROBNOSTI"].ToString(),
                        MedicalCardId = Convert.ToInt32(row["ZDRAVOTNI_KARTA_ID"]),
                    };
                    illnesses.Add(illness);
                }
            }

            return illnesses;
        }

        public async Task AddIllness(Illness illness)
        {
            string commandText = "add_illness";
            var parameters = new Dictionary<string, object>
            {
                { "p_nazev", illness.Name },
                { "p_podrobnosti", illness.Details },
                { "p_zdravotni_karta_id", illness.MedicalCardId }
            };

            await dbTools.ExecuteNonQueryAsync(commandText, parameters);

        }

        public async Task AddIllness(string illness, int patient_id)
        {
            string commandText = "add_patient_illness";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("p_patient_id", OracleDbType.Int32) { Value = patient_id },
                new OracleParameter("p_illness_name", OracleDbType.Varchar2) { Value = illness }
            };

            await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }


        public async Task<int> DeleteIllness(int id)
        {
            string commandText = "delete_illness_by_id";
            var parameters = new Dictionary<string, object>
            {
                { "p_id", id }
            };

            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }

        public async Task<int> UpdateIllness(Illness illness)
        {
            string commandText = "update_illness";

            var parameters = new Dictionary<string, object>
            {
                {"p_id", illness.Id },
                { "p_nazev", illness.Name },
                { "p_podrobnosti", illness.Details },
                { "p_zdravotni_karta_id", illness.MedicalCardId }
            };

            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }

        public void DeleteAllContacts()
        {

        }
    }
}
