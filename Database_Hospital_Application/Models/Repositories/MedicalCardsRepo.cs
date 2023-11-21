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
            string commandText = "get_all_medical_cards"; 
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
                        Illnesses = new ObservableCollection<Illness>(),
                        Smoking = Convert.ToInt32(row["KURAK"]) == 1? true:false,
                        Alergic = Convert.ToInt32(row["ALERGIK"]) == 1 ? true : false
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

                    medicalCard.MakeStringVersionOfIllnesses(); 
                    medicalCards.Add(medicalCard);
                }
            }
            return medicalCards;
        }

        public async Task AddMedicalCard(MedicalCard medicalCard, Illness newIllness)
        {
            string commandText = "add_medical_card";
            var parameters = new Dictionary<string, object>
            {
                { "p_pacient_id", medicalCard.IdOfPatient },
                { "p_kurak", medicalCard.Smoking == true?1:0},
                { "p_alergik", medicalCard.Alergic == true?1:0},
                { "p_nemoc_id", newIllness.Id },
            };

            await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }

        public async Task<int> DeleteMedicalCard(int id)
        {
            string commandText = "delete_contact_by_id";
            var parameters = new Dictionary<string, object>
            {
                { "p_id", id }
            };

            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }
        public async Task<int> UpdateMedicalCard(MedicalCard medicalCard)
        {
            string commandText = "update_medical_card";

            var parameters = new Dictionary<string, object>
            {
                {"p_id", medicalCard.Id },
                { "p_pacient_id", medicalCard.IdOfPatient },
                { "p_kurak", medicalCard.Smoking == true?1:0},
                { "p_alergik", medicalCard.Alergic == true?1:0}
            };

            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }

        public void DeleteAllMedicalCards()
        {

        }
    }
}
