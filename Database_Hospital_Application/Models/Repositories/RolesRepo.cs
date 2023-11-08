using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Database_Hospital_Application.Models.Repositories
{
    public class RolesRepo
    {
        private DatabaseTools.DatabaseTools dbTools = new DatabaseTools.DatabaseTools();

        public ObservableCollection<string> roles { get; set; }

        public RolesRepo()
        {
            roles = new ObservableCollection<string>();
        }

        public async Task<ObservableCollection<string>> GetAllRoleDescriptionsAsync()
        {
            ObservableCollection<string> roles = new ObservableCollection<string>();
            string commandText = "get_all_roles";
            DataTable result = await dbTools.ExecuteCommandAsync(commandText, null);


            if (result.Rows.Count > 0)
            {
                if (roles == null)
                {
                    roles = new ObservableCollection<string>();
                }

                foreach (DataRow row in result.Rows)
                {
                    string role = row["NAZEV"].ToString();

                    roles.Add(role);
                }
            }
            return roles;
        }

        //následující asi není potřeba:

        //public void AddRoleeDescription(string role)
        //{

        //}

        //public void DeleteRoleeDescription(int id)
        //{

        //}
        //public void UpdateRoleeDescription(string role)
        //{

        //}

        //public void DeleteAllRoleeDescriptions()
        //{

        //}
    }
}
