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
        public ObservableCollection<Patient> patients { get; set; }

        public ObservableCollection<Patient> GetAllPatients()
        {
            DatabaseTools.DatabaseTools dbTools = new DatabaseTools.DatabaseTools();
            string commandText = "get_all_patients";

            DataTable result = dbTools.ExecuteCommand(commandText, null);

            if (result.Rows.Count > 0)
            {
                if (patients == null)
                {
                    patients = new ObservableCollection<Patient>();
                }

                foreach (DataRow row in result.Rows)
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
