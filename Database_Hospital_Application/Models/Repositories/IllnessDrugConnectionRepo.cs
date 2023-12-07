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
    public class IllnessDrugConnectionRepo
    {
        private DatabaseTools.DatabaseTools dbTools = new DatabaseTools.DatabaseTools();

        public ObservableCollection<IllnessDrugConnection> illness_drugConnections { get; set; }

        public IllnessDrugConnectionRepo()
        {
            illness_drugConnections = new ObservableCollection<IllnessDrugConnection>();
        }

        public async Task<ObservableCollection<IllnessDrugConnection>> GetAllIllness_drugConnectionsAsync()
        {
            string commandText = "illness_drug.get_all_illness_drugConnection";
            DataTable result = await dbTools.ExecuteCommandAsync(commandText, null);

            illness_drugConnections.Clear(); // Vyčistíme stávající kolekci před načtením nových dat

            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    IllnessDrugConnection illness_drugConnection = new IllnessDrugConnection
                    {
                        _drug = new Drug
                        {
                            Id = Convert.ToInt32(row["UZITY_LEK_ID"]),
                            Name = row["LEK_NAZEV"].ToString(),
                            Dosage = Convert.ToInt32(row["DAVKOVANI"]),
                            Employee_id = Convert.ToInt32(row["ZAMESTNANEC_ID"]),
                        },

                        _illness = new Illness
                        {
                            Id = Convert.ToInt32(row["PRUBEZNA_NEMOC_ID"]),
                            Name = row["NEMOC_NAZEV"].ToString()
                        }
                        
                    };

                    illness_drugConnections.Add(illness_drugConnection);
                }
            }
            return illness_drugConnections;
        }

        public async Task AddIllnessDrugConnection(IllnessDrugConnection illnessDrugConnection)
        {
            string commandText = "illness_drug.add_illnessDrugConnection";
            var parameters = new Dictionary<string, object>
            {
                { "p_UZITY_LEK_ID", illnessDrugConnection._drug.Id },
                { "p_PRUBEZNA_NEMOC_ID", illnessDrugConnection._illness.Id }
            };

            await dbTools.ExecuteNonQueryAsync(commandText, parameters);

        }

        public async Task<int> DeleteIllnessDrugConnection(IllnessDrugConnection illnessDrugConnection)
        {
            string commandText = "illness_drug.delete_illnessDrugConnection";
            var parameters = new Dictionary<string, object>
            {
                { "p_UZITY_LEK_ID", illnessDrugConnection._drug.Id },
                { "p_PRUBEZNA_NEMOC_ID", illnessDrugConnection._illness.Id }
            };

            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }

        //public async Task<int> UpdateIllnessDrugConnection(IllnessDrugConnection illnessDrugConnection)
        //{
        //    string commandText = "update_illnessDrugConnection";

        //    var parameters = new Dictionary<string, object>
        //    {
        //        { "p_UZITY_LEK_ID", illnessDrugConnection._drug.Id },
        //        { "p_PRUBEZNA_NEMOC_ID", illnessDrugConnection._illness.Id }
        //    };

        //    return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        //}

        public void DeleteAllIllnessDrugConnections()
        {

        }

    }
}
