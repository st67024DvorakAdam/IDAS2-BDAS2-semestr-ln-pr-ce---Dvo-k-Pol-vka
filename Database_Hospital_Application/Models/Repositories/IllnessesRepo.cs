using Database_Hospital_Application.Models.Entities;
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
                        Name = row["NAZEV"].ToString()
                    };
                    illnesses.Add(illness);
                }
            }

            return illnesses;
        }
    }
}
