using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BE;

namespace DAL
{
    public class CustomrtDAL
    {
        
        DB db = new DB();
        public string Create(Customer c)
        {
            try
            {
                db.customers.Add(c);
                db.SaveChanges();
                return "ثبت مشتری با موفقیت انجام شد";
            }
            catch (Exception e)
            {
                return "در ثبت مشتری به مشکلی برخوردید:\n" + e.Message;

            }
        }
        public bool ReadCheack(Customer c)
        {
            var q = db.customers.Where(i => i.PhoneNumber == c.PhoneNumber);
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
            string cmd = "SELECT id AS آیدی, Name AS نام, PhoneNumber AS[شماره تماس], RegCustomer AS[تاریخ ثبت] FROM dbo.Customers WHERE  (DeleteStatus = 0)";
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CRM;Integrated Security=True");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }
        public Customer Read(int id)
        {
            return db.customers.Find(id);
        }
        public Customer ReadN(string n)
        {
            return db.customers.Where(i => i.Name == n).FirstOrDefault();
        }
        public Customer Readp(string s)
        {
            return db.customers.Where(i => i.PhoneNumber == s).FirstOrDefault();
        }
        public List<string> Readphone()
        {
            return db.customers.Where(i => i.DeleteStatus == false).Select(i => i.PhoneNumber).ToList();
        }
        public DataTable Search(string s , int index)
        {
            SqlCommand cmd = new SqlCommand();
            if (index == 0)
            {
                cmd.CommandText = "dbo.SearchCustomer";
            }
            else if (index == 1)
            {
                cmd.CommandText = "dbo.SearchCustomerName";
            }
            else if (index == 2)
            {
                cmd.CommandText = "dbo.SearchCustomerPhoneNumber";
            }
            SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = CRM; Integrated Security = True");
            cmd.Parameters.Add("@Search",s);
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            var sqladapter = new SqlDataAdapter();
            sqladapter.SelectCommand = cmd;
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];

            
        }
        public string Update(Customer c , int id)
        {

            try
            {
                Customer customer = Read(id);
                customer.Name = c.Name;
                customer.PhoneNumber = c.PhoneNumber;
                db.SaveChanges();
                return "ویرایش مشتری با موفقیت انجام شد";

            }
            catch (Exception e)
            {
                return "در ویرایش مشتری به مشکلی برخوردید:\n" + e.Message;

            }
        }
        public string Delete( int id)
        {
            try
            {
                    Customer customer = Read(id);
                    customer.DeleteStatus = true;
                    db.SaveChanges();
                    return "حذف مشتری با موفقیت انجام شد ";
  
                
            }
            catch (Exception e)
            {
                return "در حذف مشتری به مشکلی برخوردید:\n" + e.Message;

            }
        }
        public string CountCustomer()
        {
            return db.customers.Where(i => i.DeleteStatus == false).Count().ToString();
        }
    }
}

