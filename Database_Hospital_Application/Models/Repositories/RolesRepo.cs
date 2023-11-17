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

        public ObservableCollection<Role> roles { get; set; }

        public RolesRepo()
        {
            roles = new ObservableCollection<Role>();
        }

        public async Task<ObservableCollection<Role>> GetAllRoleDescriptionsAsync()
        {
            ObservableCollection<Role> roles = new ObservableCollection<Role>();
            string commandText = "get_all_roles";
            DataTable result = await dbTools.ExecuteCommandAsync(commandText, null);


            if (result.Rows.Count > 0)
            {
                if (roles == null)
                {
                    roles = new ObservableCollection<Role>();
                }

                foreach (DataRow row in result.Rows)
                {

                    Role role = new Role
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        Name = row["NAZEV"].ToString()
                    };

                    roles.Add(role);
                }
            }
            return roles;
        }


        public async Task AddRole(Role role)
        {
            string commandText = "add_role";
            var parameters = new Dictionary<string, object>
            {
                { "p_nazev", role.Name }
            };

            await dbTools.ExecuteNonQueryAsync(commandText, parameters);

        }

        public async Task<int> DeleteRole(int id)
        {
            if(id <= 5)
            {
                throw new InvalidOperationException();
            }
            string commandText = "delete_role_by_id";
            var parameters = new Dictionary<string, object>
            {
                { "p_id", id }
            };

            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }

        public async Task<int> UpdateRole(Role role)
        {
            if (role.Id <= 5)
            {
                throw new InvalidOperationException();
            }

            string commandText = "update_role";

            var parameters = new Dictionary<string, object>
            {
                {"p_id", role.Id },
                { "p_nazev", role.Name }
            };

            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }

        public void DeleteAllRoles()
        {
            //nelze mazat 1-5 aby v tom nebyl bordel
        }
    }
}
