using Database_Hospital_Application.Models.Entities;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Repositories
{
    public class DepartmentRepo
    {
        private DatabaseTools.DatabaseTools dbTools = new DatabaseTools.DatabaseTools();

        public ObservableCollection<Department> departments { get; set; }

        public DepartmentRepo()
        {
            departments = new ObservableCollection<Department>();
        }

        public async Task<ObservableCollection<Department>> GetAllDepartmentsAsync()
        {
            string commandText = "department.get_all_departments";
            DataTable result = await dbTools.ExecuteCommandAsync(commandText, null);

            departments.Clear(); 

            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    Department department = new Department
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        Name = row["NAZEV"].ToString()
                    };

                    departments.Add(department);
                }
            }
            return departments;
        }

        public async Task AddDepartment(Department department)
        {
            string commandText = "department.add_department";
            var parameters = new Dictionary<string, object>
            {
                { "p_nazev", department.Name }
            };

            await dbTools.ExecuteNonQueryAsync(commandText, parameters);

        }

        public async Task<int> DeleteDepartment(int id)
        {
            string commandText = "department.delete_department_by_id";
            var parameters = new Dictionary<string, object>
            {
                { "p_id", id }
            };

            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }
        public async Task<int> UpdateDepartment(Department department)
        {
            string commandText = "department.update_department";

            var parameters = new Dictionary<string, object>
            {
                {"p_id", department.Id },
                { "p_email", department.Name }
            };

            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }

        public void DeleteAllDepartments()
        {
            throw new NotImplementedException();
        }

        //metoda pro načtení výpisu oddělení i s počtem zaměstanců
        public async Task<string> GetNumberOfEmployeesOnDepartments()
        {
            string commandText = "department.spocitat_zamestnance_na_oddeleni";

            OracleParameter op = new OracleParameter("v_output", OracleDbType.Varchar2, ParameterDirection.Output);
            op.Size = 4000;

            var parameters = new List<OracleParameter>{op};

            await dbTools.ExecuteNonQueryAsync(commandText, parameters);


            string output = parameters.Last().Value.ToString();

            return output;
        }
    }
}
