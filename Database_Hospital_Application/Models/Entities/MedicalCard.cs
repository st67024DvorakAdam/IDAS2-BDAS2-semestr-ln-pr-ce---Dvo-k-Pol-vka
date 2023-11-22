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
        public long BirthNumberOfPatient { get; set; }
        public ObservableCollection<Illness> Illnesses { get; set; }
        public string StringVersionOfIllnesses { get; private set; }
        public int IdOfPatient {  get; set; }
        public bool Smoking { get; set; }
        public bool Alergic { get; set; }

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
    }
}
