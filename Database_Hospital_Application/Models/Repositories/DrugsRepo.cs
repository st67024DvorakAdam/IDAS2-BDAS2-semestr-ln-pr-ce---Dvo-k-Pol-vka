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

        public void AddDrugs(Drug drug)
        {

        }

        public void DeleteDrugs(int id)
        {

        }
        public void UpdateDrugs(Drug drug)
        {

        }

        public void DeleteAllDrugs()
        {

        }
    }
}
