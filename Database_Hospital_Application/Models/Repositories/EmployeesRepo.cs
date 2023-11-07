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

                        _foto = new Foto
                        {
                            Id = row.IsNull("FOTO_ID") ? 0 : Convert.ToInt32(row["FOTO_ID"])
                            
                        }
                    };
                    
                    employee.Sex = SexEnumParser.GetEnumFromString(row["POHLAVI"].ToString());
                    employee._employeeType = EmployeeTypeExtensions.GetEnumFromString(row["ZAMESTNANEC_TYPE"].ToString());
                    
                    employees.Add(employee);
                }
            }
            return employees;
        }

        public void AddEmployee(Employee employee)
        {

        }

        public void DeleteEmployee(int id)
        {

        }
        public void UpdateEmployee(Employee employee)
        {

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
