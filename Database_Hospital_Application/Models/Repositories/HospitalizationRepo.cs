using Database_Hospital_Application.Models.Entities;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Repositories
{
    public class HospitalizationRepo
    {
        private DatabaseTools.DatabaseTools dbTools = new DatabaseTools.DatabaseTools();

        public ObservableCollection<Hospitalization> hospitalizations { get; set; }

        public HospitalizationRepo()
        {
            hospitalizations = new ObservableCollection<Hospitalization>();
        }

        public async Task<ObservableCollection<Hospitalization>> GetAllHospitalizationsAsync()
        {
            ObservableCollection<Hospitalization> hospitalizations = new ObservableCollection<Hospitalization>();
            string commandText = "get_all_hospitalizations";
            DataTable result = await dbTools.ExecuteCommandAsync(commandText, null);

            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    Hospitalization hospitalization = new Hospitalization
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        DateIn = (OracleDate)row["DATE_IN"],
                        DateOut = (OracleDate)row["DATE_OUT"],
                        Details = row["DETAILS"].ToString(),
                        PatientId = Convert.ToInt32(row["PATIENT_ID"]),
                        DepartmentId = Convert.ToInt32(row["DEPARTMENT_ID"])
                    };
                    hospitalizations.Add(hospitalization);
                }
            }
            return hospitalizations;
        }

        public async Task AddHospitalization(Hospitalization hospitalization)
        {
            string commandText = "add_hospitalization";
            var parameters = new Dictionary<string, object>
            {
                { "p_date_in", hospitalization.DateIn },
                { "p_date_out", hospitalization.DateOut },
                { "p_details", hospitalization.Details },
                { "p_patient_id", hospitalization.PatientId },
                { "p_department_id", hospitalization.DepartmentId }
            };

            await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }

        public async Task<int> UpdateHospitalization(Hospitalization hospitalization)
        {
            string commandText = "update_hospitalization";
            var parameters = new Dictionary<string, object>
            {
                { "p_id", hospitalization.Id },
                { "p_date_in", hospitalization.DateIn },
                { "p_date_out", hospitalization.DateOut },
                { "p_details", hospitalization.Details },
                { "p_patient_id", hospitalization.PatientId },
                { "p_department_id", hospitalization.DepartmentId }
            };

            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }

        public async Task<int> DeleteHospitalization(int id)
        {
            string commandText = "delete_hospitalization_by_id";
            var parameters = new Dictionary<string, object>
            {
                { "p_id", id }
            };

            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }
    }
}
