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
    public class UserGroupDAL
    {
        DB db = new DB();
        public string Create(UserGroup ug)
        {
            try
            {
                db.userGroups.Add(ug);
                db.SaveChanges();
                return"ثبت گروه کاربران با موفقیت انجام شد";
            }
            catch (Exception e)
            {
                return "در ثبت گروه کاربری به مشکل خوردید " + e.Message;
            }
        }
        public DataTable ReadInfo()
        {
            string cmd = "SELECT dbo.UserGroups.Name AS[نام کاربر], dbo.UserAccessRoles.Section AS بخش, dbo.UserAccessRoles.CanEnter AS ورود, dbo.UserAccessRoles.CanCreate AS [افزودن اطلاعات], dbo.UserAccessRoles.CanUpdate AS [ویرایش اطلاعات],                         dbo.UserAccessRoles.CanDelete AS [حذف اطلاعات] FROM            dbo.UserGroups INNER JOIN                         dbo.UserAccessRoles ON dbo.UserGroups.id = dbo.UserAccessRoles.userGroup_id";
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CRM;Integrated Security=True");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }
        public bool Read(string name)
        {
            return !db.userGroups.Any(i => i.Name == name);
        }
        public UserGroup ReadN(string n)
        {
            return db.userGroups.Where(i => i.Name == n).FirstOrDefault();
        }
        public List<string> ReadUGName()
        {
            return db.userGroups.Select(i => i.Name).ToList();
        }
    }
    
}
