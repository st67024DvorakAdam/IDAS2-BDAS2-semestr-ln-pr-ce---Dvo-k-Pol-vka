using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.DatabaseTools;
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

        public User LoginUser(string username, string password)
        {
            DatabaseTools.DatabaseTools dbTools = new DatabaseTools.DatabaseTools();

            string commandText = "get_user";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "p_jmeno", username }
            };

            DataTable result = dbTools.ExecuteCommand(commandText, parameters);
            if (result.Rows.Count == 0)
            {
                return null;
            }

            string hashedPasswordFromDb = result.Rows[0]["HESLO"].ToString();

            if (password == hashedPasswordFromDb)
            {
                User loggedInUser = new User(username, password)
                {
                    // TODO Getnout ID, SALT, ROLE_ID
                    Id = Convert.ToInt32(result.Rows[0]["ID"]),
                    RoleID = Convert.ToInt32(result.Rows[0]["ROLE_ID"])
                };
                return loggedInUser;
            }
            return null;
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
