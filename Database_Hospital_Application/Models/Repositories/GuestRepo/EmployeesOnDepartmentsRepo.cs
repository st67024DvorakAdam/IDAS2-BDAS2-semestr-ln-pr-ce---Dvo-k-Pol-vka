using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Repositories.GuestRepo
{
    public class EmployeesOnDepartmentsRepo
    {
        private DatabaseTools.DatabaseTools dbTools;

        public EmployeesOnDepartmentsRepo()
        {
            dbTools = new DatabaseTools.DatabaseTools();
        }

        public async Task<ObservableCollection<string>> GetListOfEmployeesOnDepartments()
        {
                ObservableCollection<string> employeesOnDepartments = new ObservableCollection<string>();
                string commandText = "get_doktori_na_oddeleni";

                DataTable result = await dbTools.ExecuteCommandAsync(commandText, null);
                foreach (DataRow row in result.Rows)
                {
                    string s = row["ODDELENI_NAZEV"].ToString();
                    s += "  ";
                    s += row["JMENO"].ToString();
                    s += "  ";
                    s += row["PRIJMENI"].ToString();
                    s += "  ";
                    s += row["EMAIL"].ToString();
                    s += "  ";
                    s += row["TELEFON"].ToString();
                    //s += "  ";
                    //s += row["ROLE_NAZEV"].ToString();
                employeesOnDepartments.Add(s);
                }
                return employeesOnDepartments;
  
        }
    }
}
