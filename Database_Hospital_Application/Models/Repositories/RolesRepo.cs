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

        public ObservableCollection<RoleEnum> roles { get; set; }

        public RolesRepo()
        {
            roles = new ObservableCollection<RoleEnum>();
        }

        public async Task<ObservableCollection<RoleEnum>> GetAllRoleEnumsAsync()
        {
            ObservableCollection<RoleEnum> roles = new ObservableCollection<RoleEnum>();
            string commandText = "get_all_roles";
            DataTable result = await dbTools.ExecuteCommandAsync(commandText, null);


            if (result.Rows.Count > 0)
            {
                if (roles == null)
                {
                    roles = new ObservableCollection<RoleEnum>();
                }

                foreach (DataRow row in result.Rows)
                {
                    RoleEnum role = RoleExtensions.GetRoleEnumFromId(Convert.ToInt32(row["ID"]));

                    roles.Add(role);
                }
            }
            return roles;
        }

        public void AddRoleEnums(RoleEnum ŕole)
        {

        }

        public void DeleteRoleEnums(int id)
        {

        }
        public void UpdateRoleEnums(RoleEnum role)
        {

        }

        public void DeleteAllRoleEnums()
        {

        }
    }
}
