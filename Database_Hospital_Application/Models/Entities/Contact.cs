using Database_Hospital_Application.ViewModels.ViewsVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        public string Email {  get; set; }
        public int PhoneNumber {  get; set; }
        public ObservableCollection<int> IdsOfPatients { get; set; }
        public string StringVersionOfIdsOfPatients { get; private set; }
        public ObservableCollection<int> IdsOfEmployees { get; set; }
        public string StringVersionOfIdsOfEmployees { get; private set; }
    

    public void MakeStringVersionOfIdsOfPacients()
    {
        if (IdsOfPatients != null && IdsOfPatients.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            foreach (int i in IdsOfPatients)
            {
                if (sb.Length > 0)
                {
                    sb.Append(", ");
                }
                sb.Append(i);
            }
            StringVersionOfIdsOfPatients = sb.ToString();
        }
        else
        {
            StringVersionOfIdsOfPatients = string.Empty;
        }
    }


        public void MakeStringVersionOfIdsOfEmployees()
        {
            if (IdsOfEmployees != null && IdsOfEmployees.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (int i in IdsOfEmployees)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(", ");
                    }
                    sb.Append(i);
                }
                StringVersionOfIdsOfEmployees = sb.ToString();
            }
            else
            {
                StringVersionOfIdsOfEmployees = string.Empty;
            }
        }

    }
}
