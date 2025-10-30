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
    public class ActivityBLL
    {
        ActivityDAL adal = new ActivityDAL();
        
        public string Create(Activity a, User u, Customer c, ActivityCategory ac)
        {
            return adal.Create(a,u,c,ac);
        }
        public DataTable Read()
        {
            return adal.Read();
        }
        public string Readinfo(int id)
        {
            return adal.Readinfo(id);
        }
        public Activity Readid(int id)
        {
            return adal.Readid(id);
        }
        public string Update(Activity a, int id)
        {
            return adal.Update(a, id);
        }
        public DataTable Search(string s, int index)
        {
            return adal.Search(s, index);
        }
        public string Delete(int id)
        {
            return adal.Delete(id);
        }
        public string CountActivity()
        {
            return adal.CountActivity();
        }
        public List<Activity> StockActivity()
        {
            return adal.StockActivity();
        }

    }
}
