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
    public class UserDAL
    {
        DB db = new DB();
        public string Create(User u,UserGroup ug)
        {
            try
            {
                //if (ReadCheck(u))
                //{
                    u.userGroup = db.userGroups.Find(ug.id);
                    db.users.Add(u);
                    db.SaveChanges();
                    return "ثبت کاربر با موفقیت انجام شد";
                //}
                //else
                //{
                //    return ".نام کاربری شما تکراری میباشد";
                //}
            }
            catch (Exception e)
            {
                return "در ثبت کاربر به مشکلی برخوردید" + e.Message;
            }
           
            

        }
        public bool ReadCheck(User u)
        {
            var q = db.users.Where(i => i.UserName == u.UserName);
            if (q.Count() == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable Read()
        {
            string cmd = "SELECT        id AS آیدی, Name AS [نام و نام خانوادگی], UserName AS [نام کاربری], status, Password AS [رمز عبور] FROM dbo.Users WHERE(status = 0)";
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CRM;Integrated Security=True");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }
        public User ReadU(string s)
        {
            return db.users.Where(i => i.UserName == s).FirstOrDefault();
        }
        public List<string> ReadUserName()
        {
            return db.users.Where(i => i.status == false).Select(i => i.UserName).ToList();
        }
        public bool Isregistered()
        {
            return db.users.Count() > 0;
            //return db.users.Any(i => i.id > 0);
        }
        public User Readid(int id)
        {
                return db.users.Find(id);
        }
        public string Update(User u , int id)
        {
            try
            {
                    User user = Readid(id);
                    user.Name = u.Name;
                    user.UserName = u.UserName;
                    user.Password = u.Password;
                    db.SaveChanges();
                    return "ویرایش کاربر با موفقیت انجام شد";
            }
            catch (Exception e)
            {
                return "در ویرایش کاربر به مشکلی برخوردید\n" + e.Message;
            }
        }
        public string Delete(int id)
        {
            try
            {
                User user = Readid(id);
                user.status = true;
                db.SaveChanges();
                return "حذف مشتری با موفقیت انجام شد";


            }
            catch (Exception e)
            {
                return "در حذف مشتری به مشکلی برخوردید\n" + e.Message;

            }
        }
        public User EnterU(string u,string p)
        {
            return db.users.Include("userGroup").Where(i => i.UserName == u && i.Password == p && i.status == false).SingleOrDefault();
             
        }
        public bool Access(User u ,string s,int a)
        {
            UserGroup ug = db.userGroups.Include("userAccessRoles").Where(i => i.id == u.userGroup.id).FirstOrDefault();
            UserAccessRole uar = ug.userAccessRoles.Where(z => z.Section == s).FirstOrDefault();
            if (a == 1)
            {
                return uar.CanEnter;
            }
            else if (a == 2)
            {
                return uar.CanCreate;
            }
            else if (a == 3)
            {
                return uar.CanUpdate;
            }
            else
            {
                return uar.CanDelete;
                
            }
        }
        public List<User> sellsuser()
        {
            return db.users.Include("invoices").Where(i => i.status == false).ToList();
        }
        public List<Customer> CountCustomer()
        {
            return db.customers.Where(i => i.DeleteStatus == false).ToList();
        }
        public List<User> CountActivity()
        {
            return db.users.Include("activities").Where(i => i.status == false).ToList();
        }
        public string CountUser()
        {
            return db.users.Where(i => i.status == false).Count().ToString();

        }
        

    }
}
