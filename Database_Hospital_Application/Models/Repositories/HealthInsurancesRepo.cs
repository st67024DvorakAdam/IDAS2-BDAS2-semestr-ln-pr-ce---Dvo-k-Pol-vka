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
    public class HealthInsurancesRepo
    {
        public ObservableCollection<HealthInsurance> healthInsurances { get; set; }

        public ObservableCollection<HealthInsurance> GetAllHealthInsurances()
        {
            DatabaseTools.DatabaseTools dbTools = new DatabaseTools.DatabaseTools();
            string commandText = "get_all_health_insurances";

            DataTable result = dbTools.ExecuteCommand(commandText, null);

            if (result.Rows.Count > 0)
            {
                if (healthInsurances == null)
                {
                    healthInsurances = new ObservableCollection<HealthInsurance>();
                }

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

        public void AddAddress(Address address)
        {

        }

        public void DeleteAddress(int id)
        {

        }
        public void UpdateAddress(Address address)
        {

        }

        public void DeleteAllAddresses()
        {

        }
    }
}
