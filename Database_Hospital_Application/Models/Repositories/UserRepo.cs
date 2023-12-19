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
using System.Collections;

namespace Database_Hospital_Application.Models.Repositories
{
    public class UserRepo
    {

        private DatabaseTools.DatabaseTools dbTools = new DatabaseTools.DatabaseTools();
        private TwoWayAuth emailSender = new TwoWayAuth();
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

            string commandText3 = "get_department_by_employees_department_id";

            

            DataTable result3 = null;
            if ((username == "admin" || username == "Admin")) { } 
            else 
            {
                Dictionary<string, object> parameters3 = new Dictionary<string, object>
                {
                    { "p_id", Convert.ToInt32(result.Rows[0]["ODDELENI_ID"]) }
                };
                result3 = await dbTools.ExecuteCommandAsync(commandText3, parameters3); 
            }

            string commandText4 = "photo.GetEmployeeImage";

            Dictionary<string, object> parameters4 = new Dictionary<string, object>
            {
                { "p_foto_id", Convert.ToInt32(result.Rows[0]["FOTO_ID"]) }
            };

            DataTable result4 = await dbTools.ExecuteCommandAsync(commandText4, parameters4);         

            string hashedPasswordFromDb = result.Rows[0]["HESLO"].ToString();
            string saltFromDb = result.Rows[0]["SALT"].ToString();
            if (PasswordHasher.VerifyPassword(password, hashedPasswordFromDb, saltFromDb)) 
            {
                
                User loggedInUser = new User(username, password)
                {
                    Id = Convert.ToInt32(result.Rows[0]["ID"]),
                    RoleID = Convert.ToInt32(result.Rows[0]["ROLE_ID"].ToString()),
                    Salt = saltFromDb,
                    UserRole = RoleExtensions.GetRoleEnumFromId(Convert.ToInt32(result.Rows[0]["ROLE_ID"].ToString())),
                    Name = result.Rows[0]["UZIVATELSKE_JMENO"].ToString(),
                    Employee = new Employee(Convert.ToInt32(result.Rows[0]["ID"]))
                    {
                        FirstName = (string)result.Rows[0]["JMENO"],
                        LastName = (string)result.Rows[0]["PRIJMENI"],
                        BirthNumber = (result.Rows[0]["RODNE_CISLO"]).ToString(),
                        UserName = result.Rows[0]["UZIVATELSKE_JMENO"].ToString(),
                        Sex = SexEnumParser.GetEnumFromString((string)result.Rows[0]["POHLAVI"]),
                        RoleID = Convert.ToInt32(result.Rows[0]["ROLE_ID"]),
                        _role = new Role
                        {
                            Id = Convert.ToInt32(result.Rows[0]["ROLE_ID"]),
                            Name = RoleExtensions.GetRoleDescription(Convert.ToInt32(result.Rows[0]["ROLE_ID"]))
                        }
                    }
                };

                if (result.Rows[0]["EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(result.Rows[0]["EMAIL"].ToString())
                    && result.Rows[0]["TELEFON"] != DBNull.Value && !string.IsNullOrEmpty(result.Rows[0]["TELEFON"].ToString())) 
                {

                    Contact c = new Contact
                    {
                        Email = result.Rows[0]["EMAIL"].ToString(),
                        PhoneNumber = Convert.ToInt32(result.Rows[0]["TELEFON"])
                    };
                    loggedInUser.Employee._contact = c;

                    if (!string.IsNullOrEmpty(c.Email))
                    {
                        int verificationCode = CodeGenerator.Generate4DigitCode();

                       
                        string subject = "Ověřovací kód pro přihlášení";
                        string body = $"Váš ověřovací kód je: {verificationCode}";

                        //await emailSender.SendEmailAsync(c.Email, subject, body);

                    }
                }
                else
                {
                    Contact c = new Contact
                    {
                        Email = "---",
                        PhoneNumber = 000000000
                    };
                    loggedInUser.Employee._contact = c;
                }

                if (result3 != null)
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
                        Name = "Nepracuji na žádném oddělení."
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

                return loggedInUser;
            }
            return null;
        }

        public async Task<Department> GetUsersDepartment(User user)
        {
            string commandText3 = "get_department_by_employees_department_id";
            DataTable result3 = null;
            if ((user.Name == "admin" || user.Name == "Admin")) { }
            else
            {
                Dictionary<string, object> parameters3 = new Dictionary<string, object>
            {
                { "p_id", user.Employee._department.Id }
            };
                result3 = await dbTools.ExecuteCommandAsync(commandText3, parameters3);
            }

            Department d;

            if (result3 != null)
            {
                 d = new Department
                {
                    Id = Convert.ToInt32(result3.Rows[0]["ID"]),
                    Name = (string)result3.Rows[0]["NAZEV"]
                };  
            }
            else
            {
                d = new Department
                {
                    Name = "Nepracuji na žádném oddělení."
                };
            }
            return d;
        }

        public async Task<Foto> GetUsersPhoto(User user)
        {
            string commandText4 = "photo.GetEmployeeImage";

            Dictionary<string, object> parameters4 = new Dictionary<string, object>
            {
                { "p_foto_id", Convert.ToInt32(user.Employee._foto.Id) }
            };

            DataTable result4 = await dbTools.ExecuteCommandAsync(commandText4, parameters4);
            Foto f = null;
            if (result4.Rows.Count != 0)
            {
                f = new Foto
                {
                    Id = Convert.ToInt32(result4.Rows[0]["ID"]),
                };

                byte[] imageBytes = (byte[])result4.Rows[0]["obrazek"];
                BitmapImage bmimg = FotoExtension.ConvertBytesToBitmapImage(imageBytes);
                if (bmimg != null)
                {
                    f.Image = bmimg;
                }

            }
            return f;
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
            string commandText = "employee.get_all_employees";
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
                        Name = row["UZIVATELSKE_JMENO"].ToString(),
                        //Password = row["HESLO"].ToString(),
                        RoleID = Convert.ToInt32(row["ROLE_ID"]),
                        Salt = row["SALT"].ToString(),
                        Employee = new Employee
                        {
                            Id = Convert.ToInt32(row["ID"]),
                            FirstName = row["JMENO"].ToString(),
                            LastName = row["PRIJMENI"].ToString(),
                            BirthNumber = (result.Rows[0]["RODNE_CISLO"]).ToString(),
                            UserName = result.Rows[0]["UZIVATELSKE_JMENO"].ToString(),
                            Sex = SexEnumParser.GetEnumFromString((string)result.Rows[0]["POHLAVI"]),
                            IdOfSuperiorEmployee = row.IsNull("ZAMESTNANEC_ID") ? 0 : Convert.ToInt32(row["ZAMESTNANEC_ID"]),
                            RoleID = Convert.ToInt32(result.Rows[0]["ROLE_ID"]),
                            _role = new Role
                            {
                                Id = Convert.ToInt32(result.Rows[0]["ROLE_ID"]),
                                Name = RoleExtensions.GetRoleDescription(Convert.ToInt32(result.Rows[0]["ROLE_ID"]))
                            },
                            _contact = new Contact
                            {
                                Email = row["EMAIL"].ToString(),
                                PhoneNumber = Convert.ToInt32(result.Rows[0]["TELEFON"])
                            }
                        }
                    };
                    user.UserRole = RoleExtensions.GetRoleEnumFromId(user.RoleID);
                    user.Employee._department = new Department
                    {
                        Id = row.IsNull("ODDELENI_ID") ? 0 : Convert.ToInt32(row["ODDELENI_ID"])
                    };
                    user.Employee._foto = new Foto
                    {
                        Id = row.IsNull("FOTO_ID") ? 0 : Convert.ToInt32(row["FOTO_ID"])
                    };

                    //přidání nul pro RČ když začíná nulama
                    if (user.Employee.BirthNumber.Length < 8) user.Employee.BirthNumber = "0" + user.Employee.BirthNumber;
                    if (user.Employee.BirthNumber.Length < 9) user.Employee.BirthNumber = "0" + user.Employee.BirthNumber;
                    if (user.Employee.BirthNumber.Length < 10) user.Employee.BirthNumber = "0" + user.Employee.BirthNumber;

                    users.Add(user);
                }
            }
            return users;
        }

        public bool IsAdmin(User user)
        {
            return user.RoleID == 1;
        }

        public async Task<int> UploadPhotoAsync(int employeeId, byte[] photoBytes, string filename, string suffix)
        {
            try
            {
                string storedProcedure = "photo.update_employee_photo";

                
                OracleParameter pEmployeeId = new OracleParameter("p_employee_id", OracleDbType.Int32)
                {
                    Value = employeeId,
                    Direction = ParameterDirection.Input
                };

                OracleParameter pPhotoBlob = new OracleParameter("p_photo_blob", OracleDbType.Blob)
                {
                    Value = photoBytes,
                    Direction = ParameterDirection.Input
                };

                OracleParameter pFileName = new OracleParameter("p_nazev_souboru", OracleDbType.Varchar2)
                {
                    Value = filename,
                    Direction = ParameterDirection.Input
                };

                OracleParameter pSuffix = new OracleParameter("p_pripona", OracleDbType.Varchar2)
                {
                    Value = suffix,
                    Direction = ParameterDirection.Input
                };


                var parameters = new List<OracleParameter> { pEmployeeId, pPhotoBlob, pFileName, pSuffix };

                
                await dbTools.ExecuteNonQueryAsync(storedProcedure, parameters);




                string commandText2 = "employee.get_employee_by_id";
                Dictionary<string, object> parameters2 = new Dictionary<string, object> { { "p_id", employeeId } };
                DataTable result = await dbTools.ExecuteCommandAsync(commandText2, parameters2);


                if (result.Rows.Count == 0)
                {
                    return 1;
                }

                int idOfNewPhoto = Convert.ToInt32(result.Rows[0]["FOTO_ID"]);

                return idOfNewPhoto;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při nahrávání fotografie: {ex.Message}", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return 1;
            }
        }

        public async Task<string> GetUserSaltByUsername(string username)
        {
            string commandText = "get_user";
            Dictionary<string, object> parameters = new Dictionary<string, object> { { "p_jmeno", username } };
            DataTable result = await dbTools.ExecuteCommandAsync(commandText, parameters);


            if (result.Rows.Count == 0)
            {
                return null;
            }

            string saltFromDb = result.Rows[0]["SALT"].ToString();

            return saltFromDb;
        }

        public async Task<int> UpdateUser(User user)
        {
            string commandText2 = "employee.get_employee_by_id";
            Dictionary<string, object> parameters2 = new Dictionary<string, object> { { "p_id", user.Id } };
            DataTable result = await dbTools.ExecuteCommandAsync(commandText2, parameters2);


            if (result.Rows.Count == 0)
            {
                return 0;
            }
            string oldPassword = user.Employee.Password;
            if (PasswordHasher.VerifyPassword(user.Employee.Password, result.Rows[0]["HESLO"].ToString(), result.Rows[0]["SALT"].ToString()))

            {
                string passwordHash = PasswordHasher.HashPassword(user.Password, user.Salt);

                user.Password = passwordHash;

                string commandText = "update_user";

                var parameters = new Dictionary<string, object>
            {
                { "p_id", user.Id },
                { "p_jmeno", user.Employee.FirstName },
                { "p_prijmeni", user.Employee.LastName },
                { "p_heslo", user.Password },
            };

                return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
            }
            else return -123456789;
        }

        public async Task<int> DeleteUserPhoto(User user)
        {
                string commandText = "delete_user_photo";

                var parameters = new Dictionary<string, object>
            {
                { "p_id", user.Id }
            };

                return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
            
        }

        public async Task<Foto> GetUpdatedEmployeePhoto(int userId)
        {
            Foto foto = new Foto();
            string commandText = "get_employee_image_by_employeeId";
            var parameters = new Dictionary<string, object>
            {
                {"p_id", userId} 
            };
            DataTable result = await dbTools.ExecuteCommandAsync(commandText, parameters);

            if (result.Rows.Count == 0)
            {
                return null;
            }

            foto.Id = Convert.ToInt32(result.Rows[0]["ID"]);
            byte[] imageBytes = (byte[])result.Rows[0]["obrazek"];
            BitmapImage bmimg = FotoExtension.ConvertBytesToBitmapImage(imageBytes);
            if (bmimg != null)
            {
                foto.Image = bmimg;        
            }

            return foto;
        }

    }
}
