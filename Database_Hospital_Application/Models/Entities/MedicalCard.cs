using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Entities
{
    public class MedicalCard
    {
        public int Id { get; set; }
        public long BirthNumberOfPatient { get; set; }
        public ObservableCollection<Illness> Illnesses { get; set; }
        public string IllnessesInString {  get; set; }
        public int IdOfPatient {  get; set; }

        public void MakeStringVersionOfIllnesses()
        {
            string text = "";
            foreach(var i in Illnesses)
            {
                text += $"{i.Name}, ";
            }
            IllnessesInString = text;
        }
    }
}
