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
        public ObservableCollection<Illness> illnesses { get; set; }
    }
}
