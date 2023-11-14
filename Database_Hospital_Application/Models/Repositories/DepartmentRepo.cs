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

            departments.Clear(); // Vyčistíme stávající kolekci před načtením nových dat

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

        public void AddDepartment(Department department)
        {

        }

        public void DeleteDepartment(int id)
        {

        }
        public void UpdateDepartment(Department department)
        {

        }

        public void DeleteAllDepartments()
        {

        }
    }
}
