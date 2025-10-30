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
    public class ActivityCategoryDAL
    {
        DB db = new DB();

        public string Create(ActivityCategory ac)
        {
            try
            {
                db.activityCategories.Add(ac);
                db.SaveChanges();
                return "دسته بندی با موفقیت ثبت شده است";
            }
            catch (Exception e)
            {
                return "در ثبت دسته بندی مشکلی پیش آمده " + e.Message;
            }
        }
        public DataTable Read()
        {
            string cmd = "SELECT        id AS آیدی, CategoryName AS [نام دسته بندی], Deletestatus FROM dbo.ActivityCategories WHERE(Deletestatus = 0)";
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CRM;Integrated Security=True");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }
        public ActivityCategory Readid(int id)
        {
            return db.activityCategories.Find(id);
        }
        public string Update(ActivityCategory ac, int id)
        {
            try
            {
                ActivityCategory activityc = Readid(id);
                activityc.CategoryName = ac.CategoryName;                
                db.SaveChanges();
                return "ویرایش دسته بندی با موفقیت انجام شد ";

            }
            catch (Exception e)
            {
                return "در ویرایش دسته بندی به مشکل خوردید\n" + e.Message;
            }
        }
        public string Delete(int id)
        {
            try
            {
                ActivityCategory ac = Readid(id);
                ac.Deletestatus = true;
                db.SaveChanges();
                return "دسته بندی با موفقیت حذف شد ";
            }
            catch (Exception e)
            {
                return "در حذف دسته بندی مشکلی پیش آمده\n" + e.Message;
            }
        }
        public ActivityCategory ReadN(string s)
        {
            return db.activityCategories.Where(i => i.CategoryName == s).FirstOrDefault();
        }
        public List<string> ReadCategoryName()
        {
            return db.activityCategories.Select(i => i.CategoryName).ToList();
        }
    }
}
