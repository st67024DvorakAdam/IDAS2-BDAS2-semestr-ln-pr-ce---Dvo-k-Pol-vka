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
using Database_Hospital_Application.Models.Tools;
using Database_Hospital_Application.ViewModels.ViewsVM;

namespace Database_Hospital_Application.Models.Repositories
{
    public class UserRepo
    {
        private DatabaseTools.DatabaseTools dbTools = new DatabaseTools.DatabaseTools();

        public ObservableCollection<User> users { get; set; }

        public async Task<User> LoginUserAsync(string username, string password)
        {
            string commandText = "get_user";
            Dictionary<string, object> parameters = new Dictionary<string, object> { { "p_jmeno", username } };
            DataTable result = await dbTools.ExecuteCommandAsync(commandText, parameters);

            
            if (result.Rows.Count == 0)
            {
                return null;
            }


            string commandText2 = "get_employee_by_id";

            Dictionary<string, object> parameters2 = new Dictionary<string, object>
            {
                { "p_id", Convert.ToInt32(result.Rows[0]["ZAMESTNANEC_ID"]) }
            };

            DataTable result2 = await dbTools.ExecuteCommandAsync(commandText2, parameters2);
            if (result2.Rows.Count == 0)
            {
                return null;
            }


            string commandText3 = "get_department_by_employee_id";

            Dictionary<string, object> parameters3 = new Dictionary<string, object>
            {
                { "p_id", Convert.ToInt32(result2.Rows[0]["ID"]) }
            };

            DataTable result3 = await dbTools.ExecuteCommandAsync(commandText3, parameters3);


            string commandText4 = "GetEmployeeImage";

            Dictionary<string, object> parameters4 = new Dictionary<string, object>
            {
                { "p_employee_id", Convert.ToInt32(result.Rows[0]["ZAMESTNANEC_ID"]) }
            };

            DataTable result4 = await dbTools.ExecuteCommandAsync(commandText4, parameters4);         

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


        public async Task RegisterUserAsync(User user) 
        {
            if(user == null)
            {
                throw new ArgumentNullException("user");
            }

            string salt = PasswordHasher.GenerateSalt();
            string passwordHash = PasswordHasher.HashPassword(user.Password, salt);

            user.Salt = salt;
            user.Password = passwordHash;

            var parameters = new Dictionary<string, object>
            {
                // USER
                {"p_jmeno", user.Name},
                {"p_heslo", user.Password},
                {"p_salt", user.Salt},
                {"p_role_id", user.RoleID},
                // EMPLOYEE
                { "p_first_name", user.Employee.FirstName },
                { "p_last_name", user.Employee.LastName },
                { "p_birth_number", user.Employee.BirthNumber },
                { "p_sex", user.Employee.Sex.ToString() },
                // DEPARTMENT
                { "p_department_name", user.Employee._department?.Name },
                // PHOTO
                //{ "p_foto", user.Employee._foto != null ? FotoExtension.ConvertBitmapImageToBytes(foto.Image) : null }
   
            };


            string commandText = "register_user";

            await dbTools.ExecuteCommandAsync(commandText, parameters);
        }

        public async Task<ObservableCollection<User>> GetAllUsersAsync()
        {
            ObservableCollection<User> users = new ObservableCollection<User>();
            string commandText = "get_all_users";
            DataTable result = await dbTools.ExecuteCommandAsync(commandText, null);


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

        public bool IsAdmin(User user)
        {
            return user.RoleID == 1;
        }

        public async Task UploadPhotoAsync(int employeeId, byte[] photoBytes)
        {
            try
            {
                // Uložená procedura pro aktualizaci fotografie zaměstnance
                string storedProcedure = "update_employee_photo";

                // Parametry pro uloženou proceduru
                var parameters = new Dictionary<string, object>
        {
            { "p_employee_id", employeeId },
            { "p_photo_blob", photoBytes }
        };

                // Zavolání uložené procedury
                await dbTools.ExecuteCommandAsync(storedProcedure, parameters);
            }
            catch (Exception ex)
            {
                // Zpracování výjimky při chybě
                MessageBox.Show($"Chyba při nahrávání fotografie: {ex.Message}", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
