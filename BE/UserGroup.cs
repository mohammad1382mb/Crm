using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class UserGroup
    {
        public int id { get; set; }
        public string Name { get; set; }
        public List<User> users { get; set; } = new List<User>();
        public List<UserAccessRole> userAccessRoles { get; set; } = new List<UserAccessRole>();
    }
}
