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
using Database_Hospital_Application.Models.Enums;

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


            string commandText2 = "get_employee_by_id";

            Dictionary<string, object> parameters2 = new Dictionary<string, object>
            {
                { "p_id", Convert.ToInt32(result.Rows[0]["ZAMESTNANEC_ID"]) }
            };

            DataTable result2 = dbTools.ExecuteCommand(commandText2, parameters2);
            if (result2.Rows.Count == 0)
            {
                return null;
            }


            string commandText3 = "get_department_by_employee_id";

            Dictionary<string, object> parameters3 = new Dictionary<string, object>
            {
                { "p_id", Convert.ToInt32(result2.Rows[0]["ID"]) }
            };

            DataTable result3 = dbTools.ExecuteCommand(commandText3, parameters3);
            if (result3.Rows.Count == 0)
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
                    RoleID = Convert.ToInt32(result.Rows[0]["ROLE_ID"].ToString()),
                    Salt = (string)result.Rows[0]["SALT"],
                    Employee = new Employee
                    {
                        FirstName = (string)result2.Rows[0]["JMENO"],
                        LastName = (string)result2.Rows[0]["PRIJMENI"],
                        BirthNumber = Convert.ToInt64(result2.Rows[0]["RODNE_CISLO"]),
                        Sex = SexEnumParser.GetEnumFromString((string)result2.Rows[0]["POHLAVI"]),
                        _department = new Department
                        {
                            Name = (string)result3.Rows[0]["NAZEV"]
                        }
                    }
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
