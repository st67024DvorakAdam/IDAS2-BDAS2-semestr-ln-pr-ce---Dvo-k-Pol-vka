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
    public class PerformedProceduresRepo
    {
        private DatabaseTools.DatabaseTools dbTools = new DatabaseTools.DatabaseTools();

        public ObservableCollection<PerformedProcedure> performedProcedures { get; set; }

        public PerformedProceduresRepo()
        {
            performedProcedures = new ObservableCollection<PerformedProcedure>();
        }

        public async Task<ObservableCollection<PerformedProcedure>> GetAllPerformedProceduresAsync()
        {
            string commandText = "get_all_performed_procedures";
            DataTable result = await dbTools.ExecuteCommandAsync(commandText, null);

            performedProcedures.Clear();

            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    PerformedProcedure performedProcedure = new PerformedProcedure
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        Name = row["NAZEV"].ToString(),
                        Price = Convert.ToInt32(row["CENA"]),
                        IsCoveredByInsurence = (Convert.ToInt32(row["HRAZENO_POJISTOVNOU"])) == 1,
                        IdOfPatient = Convert.ToInt32(row["PACIENT_ID"]),
                        BirthNumberOfPatient = Convert.ToInt64(row["RODNE_CISLO"])
                    };
                    performedProcedures.Add(performedProcedure);
                }
            }

            return performedProcedures;
        }

        public void AddPerformedProcedure(PerformedProcedure performedProcedure)
        {

        }

        public void DeletePerformedProcedure(int id)
        {

        }
        public void UpdatePerformedProcedure(PerformedProcedure performedProcedure)
        {

        }

        public void DeleteAllPerformedProcedures()
        {

        }
    }
}
