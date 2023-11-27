using Database_Hospital_Application.Models.Entities;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Repositories
{
    public class HealthInsurancesRepo
    {
        private DatabaseTools.DatabaseTools dbTools;

        public HealthInsurancesRepo()
        {
            dbTools = new DatabaseTools.DatabaseTools();
            healthInsurances = new ObservableCollection<HealthInsurance>();
        }

        public ObservableCollection<HealthInsurance> healthInsurances { get; set; }

        public async Task<ObservableCollection<HealthInsurance>> GetAllHealthInsurancesAsync()
        {
            string commandText = "get_all_health_insurances"; 
            DataTable result = await dbTools.ExecuteCommandAsync(commandText, null);

            if (result.Rows.Count > 0)
            {
                healthInsurances.Clear(); 

                foreach (DataRow row in result.Rows)
                {
                    HealthInsurance healthInsurance = new HealthInsurance
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        Name = row["NAZEV"].ToString(),
                        Code = Convert.ToInt32(row["ZKRATKA"])
                    };
                    healthInsurances.Add(healthInsurance);
                }
            }

            return healthInsurances;
        }

        public async Task<int> AddHealthInsurance(HealthInsurance healthInsurance)
        {
            string commandText = "add_insurance";

            var parameters = new List<OracleParameter>
        {
            new OracleParameter("p_name", OracleDbType.Varchar2, healthInsurance.Name, ParameterDirection.Input),
            new OracleParameter("p_code", OracleDbType.Int32, healthInsurance.Code, ParameterDirection.Input),
            new OracleParameter("p_id", OracleDbType.Int32, ParameterDirection.Output) 
        };

            await dbTools.ExecuteNonQueryAsync(commandText, parameters);

            
            int newHealthInsuranceId = Convert.ToInt32(parameters.Last().Value.ToString());

            return newHealthInsuranceId;
        }



        public async Task<int> DeleteHealthInsurance(int id)
        {
            string commandText = "add_insurance_by_id";
            var parameters = new Dictionary<string, object>
            {
                { "p_name", id}
                
            };

            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }
        public async Task<int> UpdateHealthInsurance(HealthInsurance healthInsurance)
        {
            string commandText = "add_insurance_by_id";
            var parameters = new Dictionary<string, object>
            {
                {"p_id", healthInsurance.Id},
                { "p_name", healthInsurance.Name},
                {"p_code", healthInsurance.Code}

            };

            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }

        public void DeleteAllHealthInsurances()
        {
            throw new NotImplementedException();
        }
    }
}
