using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Entities
{
    public class Illness
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public ObservableCollection<Drug> drugs { get; set; } //léky, které se užívají na léčení této nemoci
    }
}
