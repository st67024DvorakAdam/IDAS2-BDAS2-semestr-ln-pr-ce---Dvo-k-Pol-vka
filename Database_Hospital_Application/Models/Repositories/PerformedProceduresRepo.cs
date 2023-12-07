using Database_Hospital_Application.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net;
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
            string commandText = "performed_procedure.get_all_performed_procedures";
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

        public async Task AddPerformedProcedure(PerformedProcedure performedProcedure)
        {
            string commandText = "performed_procedure.add_performed_procedure";

            var parameters = new Dictionary<string, object>
            {
                { "p_nazev", performedProcedure.Name },
                { "p_cena", performedProcedure.Price },
                { "p_hrazeno_pojistovnou", performedProcedure.IsCoveredByInsurence == true?1:0},
                { "p_pacient_id", performedProcedure.IdOfPatient }
            };

            await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }

        public async Task<ObservableCollection<PerformedProcedure>> GetAllPerformedProceduresAsync(int patientId)
        {
            ObservableCollection<PerformedProcedure> performedProcedures = new ObservableCollection<PerformedProcedure>();
            string commandText = "performed_procedure.get_all_performed_procedures_by_patient_id";

            var parameters = new Dictionary<string, object>
            {
                { "p_patient_id", patientId }
            };

            DataTable result = await dbTools.ExecuteCommandAsync(commandText, parameters);

            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    PerformedProcedure procedure = new PerformedProcedure
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        Name = row["NAZEV"].ToString(),
                        Price = Convert.ToInt32(row["CENA"]),
                        IsCoveredByInsurence = Convert.ToBoolean(row["HRAZENO_POJISTOVNOU"]),
                        IdOfPatient = Convert.ToInt32(row["PACIENT_ID"])
                    };
                    if(procedure.IsCoveredByInsurence == true)
                    {
                        procedure.IsCoveredByInsurenceString = "Ano";
                    }
                    else
                    {
                        procedure.IsCoveredByInsurenceString = "Ne";
                    }
                    performedProcedures.Add(procedure);
                }
            }
            return performedProcedures;
        }


        public async Task<int> DeletePerformedProcedure(int id)
        {
            string commandText = "performed_procedure.delete_performed_procedure_by_id";
            var parameters = new Dictionary<string, object>
            {
                { "p_id", id }
            };

            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);   
        }
        public async Task<int> UpdatePerformedProcedure(PerformedProcedure performedProcedure)
        {
            string commandText = "performed_procedure.update_performed_procedure";

            var parameters = new Dictionary<string, object>
            {
                {"p_id", performedProcedure.Id },
                { "p_nazev", performedProcedure.Name },
                { "p_cena", performedProcedure.Price },
                { "p_hrazeno_pojistovnou", performedProcedure.IsCoveredByInsurence == true?1:0},
                { "p_pacient_id", performedProcedure.IdOfPatient }
            };
            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }

        public void DeleteAllPerformedProcedures()
        {

        }
    }
}
