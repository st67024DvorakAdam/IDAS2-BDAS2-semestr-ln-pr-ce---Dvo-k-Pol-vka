using Database_Hospital_Application.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Repositories
{
    public class UserRepo
    {
        // IEnumerable / List ??
        public List<User> users { get; set; }

        public bool LoginUser(string password, string username)
        {
            DatabaseTools.DatabaseTools db = new DatabaseTools.DatabaseTools();

            string commandText = "SELECT * FROM USERS WHERE username = :username";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "username", username }
            };

            DataTable dataTable = db.ExecuteCommand(commandText, parameters);

            if (dataTable.Rows.Count == 0)
            {
                return false;
            }

            DataRow row = dataTable.Rows[0];
            string storedPassword = row["Password"].ToString();

            return storedPassword == password;
        }

        public void RegisterUser(User user) 
        { 

        }
        public void GetAllUsers()
        {

        }

        public void UpdateUser(User user)
        {
            string commandText = "";
        }
    }
}
