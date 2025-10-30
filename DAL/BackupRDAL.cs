using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Data.SqlClient;
   

namespace DAL
{
    public class BackupRDAL
    {
        public string BackUp(string Path)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CRM;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = @"backup database CRM to disk = '"+Path+@"\BackUp.bak'";
            cmd.ExecuteNonQuery();
            con.Close();
            return "اطلاعات تون در جایی که انتخاب کردید ذخیره شد";

        }
        public string Restore(string Path)
        {
            using (SqlConnection Connection = new SqlConnection(@"Data Source=.;Initial Catalog=master; Integrated Security=true"))
            {
                string Query = @"ALTER DATABASE [HazelnutDB] SET SINGLE_USER WITH ROLLBACK IMMEDIATE  RESTORE DATABASE [HazelnutDB] FROM DISK = @Path with replace";
                SqlCommand Command = new SqlCommand(Query, Connection);
                try
                {
                    Command.Parameters.AddWithValue("@Path", Path);

                    Connection.Open();
                    Command.ExecuteNonQuery();
                    Connection.Close();
                    return "اطلاعات ذخیره شده بازگردانی و جایکزین اطلاعات فعلی شد";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            
        }
    }
}
