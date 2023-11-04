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
using System.Xml.Linq;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Controls;

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


            string commandText4 = "GetEmployeeImage";

            Dictionary<string, object> parameters4 = new Dictionary<string, object>
            {
                { "p_employee_id", Convert.ToInt32(result.Rows[0]["ZAMESTNANEC_ID"]) }
            };

            DataTable result4 = dbTools.ExecuteCommand(commandText4, parameters4);         

            string hashedPasswordFromDb = result.Rows[0]["HESLO"].ToString();

            if (password == hashedPasswordFromDb)
            {
                User loggedInUser = new User(username, password)
                { 
                    // TODO Getnout ID, SALT, ROLE_ID
                    Id = Convert.ToInt32(result.Rows[0]["ID"]),
                    RoleID = Convert.ToInt32(result.Rows[0]["ROLE_ID"].ToString()),
                    Salt = (string)result.Rows[0]["SALT"],
                    UserRole = RoleExtensions.GetRoleEnumFromId(Convert.ToInt32(result.Rows[0]["ROLE_ID"].ToString())),
                    Employee = new Employee(Convert.ToInt32(result2.Rows[0]["ID"]))
                    {
                        FirstName = (string)result2.Rows[0]["JMENO"],
                        LastName = (string)result2.Rows[0]["PRIJMENI"],
                        BirthNumber = Convert.ToInt64(result2.Rows[0]["RODNE_CISLO"]),
                        Sex = SexEnumParser.GetEnumFromString((string)result2.Rows[0]["POHLAVI"])
                    }
                };

                if (result3.Rows.Count != 0)
                {
                    Department d = new Department
                    {
                        Id = Convert.ToInt32(result3.Rows[0]["ID"]),    
                        Name = (string)result3.Rows[0]["NAZEV"]
                    };
                    loggedInUser.Employee._department = d;
                }
                else
                {
                    Department d = new Department
                    {
                        Name = "Žádné oddělení pod mým vedením."
                    };
                    loggedInUser.Employee._department = d;
                }


                if (result4.Rows.Count != 0)
                {
                    Foto f = new Foto
                    {
                        Id = Convert.ToInt32(result4.Rows[0]["ID"]),                        
                    };

                    byte[] imageBytes = (byte[])result4.Rows[0]["obrazek"];
                    BitmapImage bmimg = FotoExtension.ConvertBytesToBitmapImage(imageBytes);
                    if (bmimg != null)
                    {
                        f.Image = bmimg;
                        loggedInUser.Employee._foto = f;
                    }
                    
                }
                else
                {
                    Foto f = new Foto();
                    f.Image = new BitmapImage(new Uri("https://github.com/st67024DvorakAdam/IDAS2-BDAS2-semestralni_prace-Dvorak-Polivka/raw/main/Images/no-profile-photo-icon.png"));
                    loggedInUser.Employee._foto = f; 
                }

                return loggedInUser;
            }
            return null;
        }

        public void RegisterUser(User user) 
        {
            
        }
        public ObservableCollection<User> GetAllUsers()
        {
            DatabaseTools.DatabaseTools dbTools = new DatabaseTools.DatabaseTools();
            string commandText = "get_all_users";

            DataTable result = dbTools.ExecuteCommand(commandText, null);

            if (result.Rows.Count > 0)
            {
                if (users == null)
                {
                    users = new ObservableCollection<User>();
                }

                foreach (DataRow row in result.Rows)
                {
                    User user = new User
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        Name = row["JMENO"].ToString(),
                        Password = row["HESLO"].ToString(),
                        RoleID = Convert.ToInt32(row["ROLE_ID"]),
                        Salt = row["SALT"].ToString()                             
                    };
                    user.UserRole = RoleExtensions.GetRoleEnumFromId(user.RoleID);
                    users.Add(user);
                }
            }
            return users;
        }
        public void UpdateUser(User user)
        {
            
        }
    }
}
