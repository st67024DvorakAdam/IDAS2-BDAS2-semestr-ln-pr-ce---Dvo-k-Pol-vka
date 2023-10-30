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
        public ObservableCollection<User> users { get; set; }

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
            DatabaseTools.DatabaseTools db = new DatabaseTools.DatabaseTools();
            
            // ?????????????????????????????????????????????????????????????????????????????????????????
            string commandText = "INSERT INTO USERS (name, password) VALUES (:username,:password)";

            //Dictionary<string, object> parameters = new Dictionary<string, object>
            //{
            //    { "username", user.Name }
            //    { "password", user.Password }
                
            //};

            //DataTable dataTable = db.ExecuteCommand(commandText, parameters);
        }
        public void GetAllUsers()
        {
            DatabaseTools.DatabaseTools db = new DatabaseTools.DatabaseTools();
        }

        public void UpdateUser(User user)
        {
            DatabaseTools.DatabaseTools db = new DatabaseTools.DatabaseTools();
        }
    }
}
