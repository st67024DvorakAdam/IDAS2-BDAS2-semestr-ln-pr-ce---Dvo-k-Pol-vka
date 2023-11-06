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
    public class MedicalCardsRepo
    {
        public ObservableCollection<MedicalCard> medicalCards { get; set; }

        public ObservableCollection<MedicalCard> GetAllMedicalCards()
        {
            DatabaseTools.DatabaseTools dbTools = new DatabaseTools.DatabaseTools();
            string commandText = "get_all_medical_cards";

            DataTable result = dbTools.ExecuteCommand(commandText, null);

            if (result.Rows.Count > 0)
            {
                if (medicalCards == null)
                {
                    medicalCards = new ObservableCollection<MedicalCard>();
                }

                foreach (DataRow row in result.Rows)
                {
                    MedicalCard medicalCard = new MedicalCard
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        BirthNumberOfPatient = Convert.ToInt64(row["RODNE_CISLO"]),
                        IdOfPatient = Convert.ToInt32(row["PACIENT_ID"]),
                    };
                    medicalCard.Illnesses = new ObservableCollection<Illness>();

                    string illnessesData = row["prubezna_nemoc_nazev"].ToString();

                    if (!string.IsNullOrEmpty(illnessesData))
                    {
                        // Rozdělte informace o nemocích na jednotlivé položky (předpokládáme oddělovač, např. čárku).
                        string[] illnessesArray = illnessesData.Split(',');

                        foreach (string illnessName in illnessesArray)
                        {
                            Illness illness = new Illness
                            {
                                Name = illnessName.Trim() // Můžete odstranit nadbytečné mezery
                            };
                            medicalCard.Illnesses.Add(illness);
                        }
                        medicalCard.MakeStringVersionOfIllnesses();
                    }

                    medicalCards.Add(medicalCard);
                }
            }

            return medicalCards;
        }

        public void AddMedicalCard(MedicalCard medicalCard)
        {

        }

        public void DeleteMedicalCard(int id)
        {

        }
        public void UpdateMedicalCard(MedicalCard medicalCard)
        {

        }

        public void DeleteAllMedicalCards()
        {

        }
    }
}
