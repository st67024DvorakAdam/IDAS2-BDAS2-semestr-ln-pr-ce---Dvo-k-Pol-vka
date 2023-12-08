using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Database_Hospital_Application.Models.Entities
{
    public class MedicalCard
    {
        public int Id { get; set; }
        public string BirthNumberOfPatient { get; set; }
        public ObservableCollection<Illness> Illnesses { get; set; }
        public string StringVersionOfIllnesses { get; private set; }
        public int IdOfPatient {  get; set; }
        public bool IsSmoker { get; set; }
        public bool IsAllergic { get; set; }

        public void MakeStringVersionOfIllnesses()
        {
            if (Illnesses != null && Illnesses.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (Illness illness in Illnesses)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(", ");
                    }
                    sb.Append(illness.Name);
                }
                StringVersionOfIllnesses = sb.ToString();
            }
            else
            {
                StringVersionOfIllnesses = string.Empty;
            }
        }

        public MedicalCard()
        {

        }

        public MedicalCard(bool smoking, bool alergic, int idPatient)
        {
            IdOfPatient = idPatient;
            IsSmoker = smoking;
            IsAllergic = alergic;
        }
    }

    public static class MedicalCardValidator
    {
        public static bool IsPatientFilled(MedicalCard medicalCard)
        {
            return medicalCard != null
                && medicalCard.IdOfPatient != 0 && medicalCard.IdOfPatient != null;
        }
    }
}
