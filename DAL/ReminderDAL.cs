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
    public class ReminderDAL
    {
        DB db = new DB();
        public string Create(Reminders r, User u)
        {
            try
            {
                r.users = db.users.Find(u.id);
                db.reminders.Add(r);
                db.SaveChanges();
                return "ثبت یادآور با موفقیت انجام شد";


            }
            catch (Exception e)
            {
                return "در ثبت یادآور به مشکلی برخوردید:\n" + e.Message;
            }
        }
        public DataTable Read()
        {
            string cmd = "SELECT dbo.Reminders.Title AS[موضوع یادآور], dbo.Users.Name AS [نام کاربر], dbo.Reminders.IonfoReminder AS [جزئیات یادآور], dbo.Reminders.IsDone AS وضعیت, dbo.Reminders.RegReminder AS [تاریخ ثبت],                         dbo.Reminders.ReminderDate AS [تاریخ یادآوری], dbo.Reminders.DeleteStatus, dbo.Reminders.id FROM            dbo.Reminders INNER JOIN                         dbo.Users ON dbo.Reminders.users_id = dbo.Users.id WHERE        (dbo.Reminders.DeleteStatus = 0)";
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CRM;Integrated Security=True");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }
        public DataTable Search(string s)
        {            
            SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = CRM; Integrated Security = True");
            SqlCommand cmd = new SqlCommand("dbo.SearchReminder");
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
        public Reminders Readid (int id)
        {
            return db.reminders.Find(id);
        }
        public string Update(Reminders r,int id)
        {
            try
            {
                Reminders reminder = Readid(id);
                reminder.Title = r.Title;
                reminder.IonfoReminder = r.IonfoReminder;
                db.SaveChanges();
                return ".ویرایش یادآور با موفقیت انجام شد";
            }
            catch (Exception e)
            {
                return "در ویرایش یادآور به مشکلی برخوردید:\n" + e.Message;
            }
            

        }
        public string Delete(int id)
        {
            try
            {
                Reminders reminder = Readid(id);
                reminder.DeleteStatus = true;
                db.SaveChanges();
                return ".حذف یادآور با موفقیت انجام شد";


            }
            catch (Exception e)
            {
                return ".در حذف یادآور به مشکلی برخوردید:\n" + e.Message;

            }
        }
        public string IsDone(int id)
        {
            try
            {
                Reminders reminder = Readid(id);
                reminder.IsDone = true;
                db.SaveChanges();
                return ".یادآور انجام شد";


            }
            catch (Exception e)
            {
                return ".در تکمیل یادآور به مشکل برخوردیم:\n" + e.Message;

            }
        }
        public string CountReminder()
        {
            return db.reminders.Where(i => i.DeleteStatus == false).Count().ToString();
        }
        
        
    }
}

