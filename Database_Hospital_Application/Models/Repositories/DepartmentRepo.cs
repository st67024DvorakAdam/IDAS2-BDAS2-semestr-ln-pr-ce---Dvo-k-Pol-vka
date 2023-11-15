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
            string commandText = "get_all_departments";
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
            string commandText = "add_department";
            var parameters = new Dictionary<string, object>
            {
                { "p_nazev", department.Name }
            };

            await dbTools.ExecuteNonQueryAsync(commandText, parameters);

        }

        public async Task<int> DeleteDepartment(int id)
        {
            string commandText = "delete_department_by_id";
            var parameters = new Dictionary<string, object>
            {
                { "p_id", id }
            };

            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }
        public async Task<int> UpdateDepartment(Department department)
        {
            string commandText = "update_department";

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
    }
}
