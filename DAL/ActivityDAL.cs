using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class ActivityDAL
    {
        DB db = new DB();
        
        public string Create(Activity a,User u,Customer c,ActivityCategory ac)
        {
            try
            {
                a.category = db.activityCategories.Find(ac.id);
                a.customer = db.customers.Find(c.id);
                a.user = db.users.Find(u.id);
                db.activities.Add(a);
                db.SaveChanges();
                return  "ثبت فعالیت با موفقیت انجام شد";
                
            }
            catch (Exception e)
            {
                return "در ثبت فعالیت به مشکلی برخوردید" + e.Message;
            }
        }
        public DataTable Read()
        {
            SqlCommand cmd = new SqlCommand("dbo.ReadActivity");          
            SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = CRM; Integrated Security = True");
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            var sqladapter = new SqlDataAdapter();
            sqladapter.SelectCommand = cmd;
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }
        public string Readinfo(int id)
        {
            Activity a = Readid(id);
            return db.activities.Select(i => a.InofActivity).FirstOrDefault();
        }
        public Activity Readid(int id)
        {
            return db.activities.Find(id);
        }
        public Reminders ReadReminders(int id)
        {
            return db.reminders.Find(id);
        }
        public string Update(Activity a,int id)
        {
            try
            {
                Activity activity = Readid(id);
                activity.Title = a.Title;
                activity.InofActivity = a.InofActivity;
                activity.category = a.category;
                activity.customer = a.customer;
                activity.user = a.user;
                db.SaveChanges();
                return "ویرایش فعالیت با موفقیت انجام شد";

            }
            catch (Exception e)
            {
                return "در ویرایش فعالیت به مشکلی برخوردید\n" + e.Message;
            }
        }
        public DataTable Search(string s, int index)
        {
            SqlCommand cmd = new SqlCommand();
            if (index == 0)
            {
                cmd.CommandText = "dbo.SearchActivity";
            }
            else if (index == 1)
            {
                cmd.CommandText = "dbo.SearchActivityNameCustomer";
            }
            else if (index == 2)
            {
                cmd.CommandText = "dbo.SearchActivityNameUser";
            }
            SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = CRM; Integrated Security = True");
            cmd.Parameters.Add("@Search", s);
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            var sqladapter = new SqlDataAdapter();
            sqladapter.SelectCommand = cmd;
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];


        }
        public string Delete(int id)
        {
            try
            {
                Reminders r = ReadReminders(id);
                Activity a = Readid(id);
                a.DeleteStatus = true;
                r.IsDone = true;
                db.SaveChanges();
                return "فعالیت با موفقیت حذف شد ";
            }
            catch (Exception e)
            {
                return "در حذف فعالیت به مشکلی برخوردید\n" + e.Message;
            }
        }
        public string CountActivity()
        {
            return db.activities.Where(i => i.DeleteStatus == false).Count().ToString();
        }
        public List<Activity> StockActivity()
        {
            return db.activities.Where(i => i.DeleteStatus == false).ToList();
        }
    }
}
