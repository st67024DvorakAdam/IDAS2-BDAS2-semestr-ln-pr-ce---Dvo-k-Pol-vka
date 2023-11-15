using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            string commandText = "get_all_employees";
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
                        BirthNumber = Convert.ToInt64(row["RODNE_CISLO"]),
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
                        }
                        
                    };
                    
                    employee.Sex = SexEnumParser.GetEnumFromString(row["POHLAVI"].ToString());
                    
                    employees.Add(employee);
                }
            }
            return employees;
        }

        public async Task AddEmployee(Employee employee)
        {
            string commandText = "add_employee";
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
            string commandText = "delete_employee_by_id";
            var parameters = new Dictionary<string, object>
            {
                { "p_id", id }
            };
            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }
        public async Task<int> UpdateEmployee(Employee employee)
        {
            string commandText = "update_employee";

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
            string commandText = "get_employee_by_id";

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
                    BirthNumber = Convert.ToInt64(row["RODNE_CISLO"]),
                    Sex = SexEnumParser.GetEnumFromString(row["POHLAVI"].ToString()),

                    // _department a _foto by měly být naplněný zvlášť
                };

                return employee;
            }

            return null;
        }

    }
}
