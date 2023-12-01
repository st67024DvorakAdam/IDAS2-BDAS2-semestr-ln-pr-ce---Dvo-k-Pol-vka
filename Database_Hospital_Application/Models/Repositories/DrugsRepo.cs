using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Entities.HelpEntities;
using Database_Hospital_Application.Models.Enums;
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

        public async Task AddDrug(Drug drug)
        {
            string commandText = "add_drug";
            var parameters = new Dictionary<string, object>
            {
                { "p_nazev", drug.Name },
                { "p_davkovani", drug.Dosage },
                { "p_zamestnanec_id", drug.Employee_id}
            };

            await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }

        public async Task PrescriptDrugForIllness(Drug drug, Illness illness)
        {
            string commandText = "add_drug_for_illness";
            var parameters = new Dictionary<string, object>
            {
                { "p_nazev", drug.Name },
                { "p_nemoc_id", illness.Id },
                { "p_davkovani", drug.Dosage },
                { "p_zamestnanec_id", drug.Employee_id}
            };
            
            await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }

        public async Task<int> DeleteDrug(int id)
        {
            string commandText = "delete_drug_by_id";
            var parameters = new Dictionary<string, object>
            {
                { "p_id", id }
            };

            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }
        public async Task<int> UpdateDrug(Drug drug)
        {
            string commandText = "update_drug";

            var parameters = new Dictionary<string, object>
            {
                {"p_id", drug.Id },
                { "p_nazev", drug.Name },
                { "p_davkovani", drug.Dosage },
                { "p_zamestnanec_id", drug.Employee_id}
            };

            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }

        public async Task<int> UpdateDosageAsync(Drug drug)
        {
            string commandText = "update_pills_dosage";

            var parameters = new Dictionary<string, object>
            {
                {"p_pill_id", drug.Id },
                { "p_new_dosage", drug.Dosage }
            };

            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }


        public void DeleteAllDrugs()
        {

        }


        //metoda která vrátí ke každému hospitalizovanému pacientovi co užívá za léky
        //využívá to sestra, aby věděla jaké léky má podávat
        public async Task<ObservableCollection<DosageForHospitalizated>> GetDosageForHospitalizatedPatients(User user)
        {
            ObservableCollection<DosageForHospitalizated> drugs = new ObservableCollection<DosageForHospitalizated>();
            string commandText = "get_dosage_for_hospitalizated";
            Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "p_id", user.Employee._department.Id }
                };
            DataTable result = await dbTools.ExecuteCommandAsync(commandText, parameters);


            if (result.Rows.Count > 0)
            {

                foreach (DataRow row in result.Rows)
                {
                    DosageForHospitalizated drug = new DosageForHospitalizated
                    {
                        NameOfDoctor = row["JMENO_ZAMESTNANCE"].ToString() + " " + row["PRIJMENI_ZAMESTNANCE"].ToString(),
                        _department = new Department
                        {
                            Id = Convert.ToInt32(row["ID_ODDELENI"]),
                            Name = row["NAZEV_ODDELENI"].ToString()
                        },
                        _drug = new Drug
                        {
                            Name = row["NAZEV_LEKU"].ToString(),
                            Dosage = Convert.ToInt32(row["DAVKOVANI"])
                        },
                        _illness = new Illness
                        {
                            Name = row["NAZEV_NEMOCI"].ToString()
                        },
                        _patient = new Patient
                        {
                            FirstName = row["JMENO_PACIENTA"].ToString(),
                            LastName = row["PRIJMENI_PACIENTA"].ToString(),
                            BirthNumber = row["RODNE_CISLO"].ToString(),
                            Sex = SexEnumParser.GetEnumFromString(row["POHLAVI"].ToString())
                        }
                    };
                    drug._patient.BirthNumber = drug._patient.completeBirthNumber(drug._patient.BirthNumber);
                    drugs.Add(drug);
                }
            }
            return drugs;
        }
        
        //metoda která lékaři vypíše všechny léky které předepsal
        public async Task<ObservableCollection<DrugsPreceptedByDoctor>> GetPreceptedDrugByDoctor(User user)
        {
            ObservableCollection<DrugsPreceptedByDoctor> drugs = new ObservableCollection<DrugsPreceptedByDoctor>();
            string commandText = "get_drugs_issued_by_doctor";
            Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "p_doktor_id", user.Employee.Id }
                };
            DataTable result = await dbTools.ExecuteCommandAsync(commandText, parameters);


            if (result.Rows.Count > 0)
            {

                foreach (DataRow row in result.Rows)
                {
                    DrugsPreceptedByDoctor drug = new DrugsPreceptedByDoctor
                    {
                        _drug = new Drug
                        {
                            Name = row["NAZEV_LEKU"].ToString(),
                            Dosage = Convert.ToInt32(row["DAVKOVANI_LEKU"])
                        },
                        _illness = new Illness
                        {
                            Name = row["NAZEV_NEMOCI"].ToString()
                        },
                        _patient = new Patient
                        {
                            FirstName = row["JMENO_PACIENTA"].ToString(),
                            LastName = row["PRIJMENI_PACIENTA"].ToString(),
                            BirthNumber = row["RODNE_CISLO"].ToString(),
                            Sex = SexEnumParser.GetEnumFromString(row["POHLAVI"].ToString())
                        }
                    };
                    drug._patient.BirthNumber = drug._patient.completeBirthNumber(drug._patient.BirthNumber);
                    drugs.Add(drug);
                }
            }
            return drugs;
        }
    }
}
