using Database_Hospital_Application.Exceptions;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Enums;
using Database_Hospital_Application.Models.Tools;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace Database_Hospital_Application.Models.Repositories
{
    public class EmployeesRepo
    {
        private DatabaseTools.DatabaseTools dbTools = new DatabaseTools.DatabaseTools();

        public ObservableCollection<Employee> employees { get; set; }

        public EmployeesRepo()
        {
            employees = new ObservableCollection<Employee>();
        }

        public async Task<ObservableCollection<Employee>> GetAllEmployeesAsync()
        {
            string commandText = "employee.get_all_employees";
            DataTable result = await dbTools.ExecuteCommandAsync(commandText, null);

            employees.Clear(); // Vyčistíme stávající kolekci před načtením nových dat

            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    Employee employee = new Employee
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        FirstName = row["JMENO"].ToString(),
                        LastName = row["PRIJMENI"].ToString(),
                        BirthNumber = (row["RODNE_CISLO"]).ToString(),
                        IdOfSuperiorEmployee = row.IsNull("ZAMESTNANEC_ID") ? 0 : Convert.ToInt32(row["ZAMESTNANEC_ID"]),
                        RoleID = Convert.ToInt32(row["ROLE_ID"]),
                        UserName = row["UZIVATELSKE_JMENO"].ToString(),
                        Password = row["HESLO"].ToString(),
                        Salt = row["SALT"].ToString(),
                        _foto = new Foto
                        {
                            Id = row.IsNull("FOTO_ID") ? 0 : Convert.ToInt32(row["FOTO_ID"])

                        },

                        _department = new Department
                        {
                            Id = row.IsNull("ODDELENI_ID") ? 0 : Convert.ToInt32(row["ODDELENI_ID"])
                        },
                        _role = new Role
                        {
                            Id = Convert.ToInt32(row["ROLE_ID"]),
                            Name = RoleExtensions.GetRoleDescription(Convert.ToInt32(row["ROLE_ID"]))
                        }
                        
                        
                    };
                    
                    employee.Sex = SexEnumParser.GetEnumFromString(row["POHLAVI"].ToString());
                    
                    //přidání nul pro RČ když začíná nulama
                    if (employee.BirthNumber.Length < 8) employee.BirthNumber = "0" + employee.BirthNumber;
                    if (employee.BirthNumber.Length < 9) employee.BirthNumber = "0" + employee.BirthNumber;
                    if (employee.BirthNumber.Length < 10) employee.BirthNumber = "0" + employee.BirthNumber;

                    employees.Add(employee);
                }
            }
            return employees;
        }

        public async Task AddEmployee(Employee employee)
        {
                string commandText = "employee.add_employee";
                var parameters = new Dictionary<string, object>
            {
                { "p_jmeno", employee.FirstName },
                { "p_prijmeni", employee.LastName },
                { "p_rodne_cislo", employee.BirthNumber },
                { "p_pohlavi", SexEnumParser.GetStringFromEnumCzech(employee.Sex) },
                { "p_zamestnanec_id", employee.IdOfSuperiorEmployee },
                { "p_foto_id", employee._foto.Id },
                { "p_oddeleni_id", employee._department.Id },
                { "p_uzivatelske_jmeno", employee.UserName },
                { "p_heslo", employee.Password },
                { "p_salt", employee.Salt },
                { "p_role_id",employee.RoleID }
            };

                await dbTools.ExecuteNonQueryAsync(commandText, parameters);
            
        }



        public async Task<int> DeleteEmployee(int id)
        {
            string commandText = "employee.delete_employee_by_id";
            var parameters = new Dictionary<string, object>
            {
                { "p_id", id }
            };
            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }
        public async Task<int> UpdateEmployee(Employee employee, bool hashThePassword)
        {
            if (hashThePassword) employee.Password = PasswordHasher.HashPassword(employee.Password, employee.Salt);

            string commandText = "employee.update_employee";

            var parameters = new Dictionary<string, object>
            {
                {"p_id", employee.Id },
                { "p_jmeno", employee.FirstName },
                { "p_prijmeni", employee.LastName },
                { "p_rodne_cislo", employee.BirthNumber },
                { "p_pohlavi", SexEnumParser.GetStringFromEnumCzech(employee.Sex) },
                { "p_zamestnanec_id", employee.IdOfSuperiorEmployee },
                { "p_foto_id", employee._foto.Id },
                { "p_oddeleni_id", employee._department.Id },
                { "p_uzivatelske_jmeno", employee.UserName },
                { "p_heslo", employee.Password },
                { "p_salt", employee.Salt },
                { "p_role_id",employee.RoleID }
            };

            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }

        public void DeleteAllEmployees()
        {

        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            string commandText = "employee.get_employee_by_id";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "p_id", employeeId }
            };

            DataTable result = await dbTools.ExecuteCommandAsync(commandText, parameters);

            if (result.Rows.Count > 0)
            {
                DataRow row = result.Rows[0];

                Employee employee = new Employee
                {
                    Id = employeeId,
                    FirstName = row["JMENO"].ToString(),
                    LastName = row["PRIJMENI"].ToString(),
                    BirthNumber = row["RODNE_CISLO"].ToString(),
                    Sex = SexEnumParser.GetEnumFromString(row["POHLAVI"].ToString()),

                    // _department a _foto by měly být naplněný zvlášť
                };

                //přidání nul pro RČ když začíná nulama
                if (employee.BirthNumber.Length < 8) employee.BirthNumber = "0" + employee.BirthNumber;
                if (employee.BirthNumber.Length < 9) employee.BirthNumber = "0" + employee.BirthNumber;
                if (employee.BirthNumber.Length < 10) employee.BirthNumber = "0" + employee.BirthNumber;

                return employee;
            }

            return null;
        }



        //výpis podřízených dle id i kontaktními údaji
        public async Task<ObservableCollection<string>> GetListOfSubordinates(int IdOfEmployee)
        {
            ObservableCollection<string> subordinates = new ObservableCollection<string>();
            string commandText = "employee.get_subordinates";
            var parameters = new Dictionary<string, object>
            {
                { "p_id", IdOfEmployee }
            };

            DataTable result = await dbTools.ExecuteCommandAsync(commandText, parameters);
            foreach(DataRow row in result.Rows)
            {
                string s = row["JMENO"].ToString();
                s += "  ";
                s += row["PRIJMENI"].ToString();
                s += "  ";
                s += row["EMAIL"].ToString();
                s += "  ";
                s += row["TELEFON"].ToString();
                subordinates.Add(s);
            }
            return subordinates;
        }


    }
}
