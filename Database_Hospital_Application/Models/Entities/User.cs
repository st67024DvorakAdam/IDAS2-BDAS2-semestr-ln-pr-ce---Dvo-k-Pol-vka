using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Database_Hospital_Application.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Salt { get; set;  }
        public int RoleID { get; set; }

        public User(string name, string password) {
            Name = name;
            Password = password;
        }

        
    }
}
