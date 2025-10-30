using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class ActivityCategoryBLL
    {
        ActivityCategoryDAL acdal = new ActivityCategoryDAL();
        public string Create(ActivityCategory ac)
        {
            return acdal.Create(ac);
        }
        public DataTable Read()
        {
            return acdal.Read();
        }
        public ActivityCategory Readid(int id)
        {
            return acdal.Readid(id);
        }
        public string Update(ActivityCategory ac, int id)
        {
            return acdal.Update(ac, id);
        }
        public string Delete(int id)
        {
            return acdal.Delete(id);
        }
        public ActivityCategory ReadN(string s)
        {
            return acdal.ReadN(s);
        }
        public List<string> ReadCategoryName()
        {
            return acdal.ReadCategoryName();
        }


    }
}
