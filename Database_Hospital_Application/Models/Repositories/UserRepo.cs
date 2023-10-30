using Database_Hospital_Application.Models.Entities;
using Oracle.ManagedDataAccess.Client;
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
    public class UserRepo
    {
        // IEnumerable / List ??
        public ObservableCollection<User> users { get; set; }

        public bool LoginUser(string password, string username)
        {
            DatabaseTools.DatabaseTools dbTools = new DatabaseTools.DatabaseTools();

            // Volání funkce
            string commandText = "get_user_salt_and_password";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "p_jmeno", username }
            };

            DataTable result = dbTools.ExecuteCommand(commandText, parameters);
            if (result.Rows.Count == 0)
            {
                return false;
            }

            string saltFromDb = result.Rows[0]["SALT"].ToString();
            string hashedPasswordFromDb = result.Rows[0]["HESLO"].ToString();

            string saltedInputPassword = saltFromDb + password;
            string hashedInputPassword = "abc" + password;

            MessageBox.Show("heslo: " + hashedPasswordFromDb + " salt: " + saltFromDb, "Info", MessageBoxButton.OK, MessageBoxImage.Information);

            return hashedInputPassword == hashedPasswordFromDb;

        }

        public void RegisterUser(User user) 
        {
            
        }
        public void GetAllUsers()
        {
            
        }

        public void UpdateUser(User user)
        {
            
        }
    }
}
