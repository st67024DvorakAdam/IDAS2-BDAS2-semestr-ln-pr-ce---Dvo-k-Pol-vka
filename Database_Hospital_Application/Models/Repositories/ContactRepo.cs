using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.ViewModels.ViewsVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Repositories
{
    public class ContactRepo
    {

        private DatabaseTools.DatabaseTools dbTools = new DatabaseTools.DatabaseTools();

        public ObservableCollection<Contact> contacts { get; set; }

        public ContactRepo()
        {
            contacts = new ObservableCollection<Contact>();
        }

        public async Task<ObservableCollection<Contact>> GetAllContactsAsync()
        {
            string commandText = "get_all_contacts";
            DataTable result = await dbTools.ExecuteCommandAsync(commandText, null);

            contacts.Clear(); // Vyčistíme stávající kolekci před načtením nových dat

            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    Contact contact = new Contact
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        Email = (row["EMAIL"]).ToString(),
                        PhoneNumber = Convert.ToInt32(row["TELEFON"]),
                        IdsOfPatients = new ObservableCollection<int>(),
                        IdsOfEmployees = new ObservableCollection<int>()
                    };

                    string idsOfPatientsData = row["pacient_ids"].ToString();
                    if (!string.IsNullOrEmpty(idsOfPatientsData))
                    {
                        string[] idsOfPatientsArray = idsOfPatientsData.Split(',');
                        foreach(string id in idsOfPatientsArray)
                        {
                            contact.IdsOfPatients.Add(Convert.ToInt32(id));
                        }
                    }
                    contact.MakeStringVersionOfIdsOfPacients();


                    string idsOfEmployeesData = row["zamestnanec_ids"].ToString();
                    if (!string.IsNullOrEmpty(idsOfEmployeesData))
                    {
                        string[] idsOfEmployeesArray = idsOfEmployeesData.Split(',');
                        foreach (string id in idsOfEmployeesArray)
                        {
                            contact.IdsOfEmployees.Add(Convert.ToInt32(id));
                        }
                    }
                    contact.MakeStringVersionOfIdsOfEmployees();


                    contacts.Add(contact);
                }
            }
            return contacts;
        }

        public async Task AddContact(Contact contact)
        {
            string commandText = "add_contact";
            var parameters = new Dictionary<string, object>
            {
                { "p_email", contact.Email },
                { "p_phone", contact.PhoneNumber }
            };

            await dbTools.ExecuteNonQueryAsync(commandText, parameters);

        }

        public async Task<int> DeleteContact(int id)
        {
            string commandText = "add_contact_by_id";
            var parameters = new Dictionary<string, object>
            {
                { "p_id", id }
            };

            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }

        public async Task<int> UpdateContact(Contact contact)
        {
            string commandText = "update_contact";

            var parameters = new Dictionary<string, object>
            {
                { "p_email", contact.Email },
                { "p_phone", contact.PhoneNumber }
            };

            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }

        public void DeleteAllContacts()
        {

        }

        public async Task<ObservableCollection<Contact>> GetContactsByEmployeeIdAsync(int employeeId)
        {
            ObservableCollection<Contact> contacts = new ObservableCollection<Contact>();
            string commandText = "get_contacts_by_employee_id";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "p_employee_id", employeeId }
            };

            DataTable result = await dbTools.ExecuteCommandAsync(commandText, parameters);

            foreach (DataRow row in result.Rows)
            {
                Contact contact = new Contact
                {
                    Id = Convert.ToInt32(row["ID"]),
                    Email = row["EMAIL"].ToString(),
                    PhoneNumber = Convert.ToInt32(row["TELEFON"])
                };
                contacts.Add(contact);
            }

            return contacts;
        }
    }
}
