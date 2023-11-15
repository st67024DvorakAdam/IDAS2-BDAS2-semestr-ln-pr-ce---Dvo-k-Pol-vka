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
    public class DrugsRepo
    {
        private DatabaseTools.DatabaseTools dbTools = new DatabaseTools.DatabaseTools();

        public ObservableCollection<Drug> drugs { get; set; }

        public DrugsRepo()
        {
            drugs = new ObservableCollection<Drug>();
        }

        public async Task<ObservableCollection<Drug>> GetAllDrugsAsync()
        {
            ObservableCollection<Drug> drugs = new ObservableCollection<Drug>();
            string commandText = "get_all_drugs";
            DataTable result = await dbTools.ExecuteCommandAsync(commandText, null);


            if (result.Rows.Count > 0)
            {
                if (drugs == null)
                {
                    drugs = new ObservableCollection<Drug>();
                }

                foreach (DataRow row in result.Rows)
                {
                    Drug drug = new Drug
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        Name = row["NAZEV"].ToString(),
                        Dosage = Convert.ToInt32(row["DAVKOVANI"]),
                        Employee_id = Convert.ToInt32(row["ZAMESTNANEC_ID"]) //kdo lek predepsal
                    };
                    drugs.Add(drug);
                }
            }
            return drugs;
        }

        public async Task AddDrug(Drug drug)
        {
            string commandText = "add_drug";
            var parameters = new Dictionary<string, object>
            {
                { "p_nazev", drug.Name },
                { "p_davkovani", drug.Dosage },
                { "p_zamestnanec_id", drug.Employee_id}
            };

            await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }

        public async Task<int> DeleteDrug(int id)
        {
            string commandText = "delete_drug_by_id";
            var parameters = new Dictionary<string, object>
            {
                { "p_id", id }
            };

            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }
        public async Task<int> UpdateDrug(Drug drug)
        {
            string commandText = "update_drug";

            var parameters = new Dictionary<string, object>
            {
                {"p_id", drug.Id },
                { "p_nazev", drug.Name },
                { "p_davkovani", drug.Dosage },
                { "p_zamestnanec_id", drug.Employee_id}
            };

            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }

        public void DeleteAllDrugs()
        {

        }
    }
}
