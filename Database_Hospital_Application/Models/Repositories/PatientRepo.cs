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
    public class PatientRepo
    {
        private DatabaseTools.DatabaseTools dbTools;
        // Konstruktor
        public PatientRepo()
        {
            dbTools = new DatabaseTools.DatabaseTools();
            patients = new ObservableCollection<Patient>();
        }

        public ObservableCollection<Patient> patients { get; set; }

        public async Task<ObservableCollection<Patient>> GetPatientsAsync()
        {
            string commandText = "get_all_patients";

            DataTable dataTable = await dbTools.ExecuteCommandAsync(commandText);

            if (dataTable.Rows.Count > 0)
            {
                patients.Clear();

                foreach (DataRow row in dataTable.Rows)
                {
                    Patient patient = new Patient
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        FirstName = row["JMENO"].ToString(),
                        LastName = row["PRIJMENI"].ToString(),
                        BirthNumber = Convert.ToInt32(row["RODNE_CISLO"])
                    };
                    patients.Add(patient);
                }
            }

            return patients;
        }
    }
}
