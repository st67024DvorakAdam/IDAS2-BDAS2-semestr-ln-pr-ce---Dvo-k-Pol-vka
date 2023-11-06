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
        private DatabaseTools.DatabaseTools dbTools = new DatabaseTools.DatabaseTools();

        public ObservableCollection<MedicalCard> medicalCards { get; set; }

        public MedicalCardsRepo()
        {
            medicalCards = new ObservableCollection<MedicalCard>();
        }

        public async Task<ObservableCollection<MedicalCard>> GetAllMedicalCardsAsync()
        {
            string commandText = "get_all_medical_cards"; // Předpokládáme, že je to název uložené procedury
            DataTable result = await dbTools.ExecuteCommandAsync(commandText, null);

            medicalCards.Clear(); // Vyčistíme stávající kolekci před načtením nových dat

            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    MedicalCard medicalCard = new MedicalCard
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        BirthNumberOfPatient = Convert.ToInt64(row["RODNE_CISLO"]),
                        IdOfPatient = Convert.ToInt32(row["PACIENT_ID"]),
                        Illnesses = new ObservableCollection<Illness>()
                    };

                    string illnessesData = row["prubezna_nemoc_nazev"].ToString();
                    if (!string.IsNullOrEmpty(illnessesData))
                    {
                        string[] illnessesArray = illnessesData.Split(',');
                        foreach (string illnessName in illnessesArray)
                        {
                            Illness illness = new Illness { Name = illnessName.Trim() };
                            medicalCard.Illnesses.Add(illness);
                        }
                    }

                    medicalCard.MakeStringVersionOfIllnesses(); // Tuto metodu je třeba upravit, aby byla kompatibilní s asynchronním zpracováním
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
