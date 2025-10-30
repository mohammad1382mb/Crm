using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class UserGroupBLL
    {
        UserGroupDAL ugdal = new UserGroupDAL();
        public string Create(UserGroup ug)
        {
            return ugdal.Create(ug);

        }
        public DataTable ReadInfo()
        {
            return ugdal.ReadInfo();
        }
        public bool Read(string name)
        {
            return ugdal.Read(name);
        }
        public UserGroup ReadN(string n)
        {
            return ugdal.ReadN(n);
        }
        public List<string> ReadUGName()
        {
            return ugdal.ReadUGName();
        }

    }
}
