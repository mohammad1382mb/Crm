using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BE;

namespace DAL
{
    public class InvoiceDAL
    {
        DB db = new DB();
        
        public string Create(Invoice i,Customer c , List<Product> p,User u)
        {
            try
            {
                i.User = db.users.Find(u.id);
                i.customer = db.customers.Find(c.id);
                foreach (var item in p)
                {
                    i.products.Add(db.products.Find(item.id));
                }
                Random r = new Random();
                string s = r.Next(1000000).ToString("N0");
                var z = db.invoices.Where(x => x.InvoiceNumber == s);
                while (z.Count() > 0)
                {
                    s = r.Next(1000000).ToString("N0");
                }
                
                i.InvoiceNumber = s;
                db.invoices.Add(i);
                db.SaveChanges();
                return "ثبت فاکتور با موفقیت انجام شد";
            }
            catch (Exception e)
            {
                return "درثبت فاکتور به مشکلی برخوردید " + e.Message;
            }
        }
        
        public DataTable Read()
        {
            string cmd = "SELECT dbo.Customers.Name AS[نام مشتری], dbo.Invoices.InvoiceNumber AS[شماره فاکتور], dbo.Invoices.RegInvoice AS[تاریخ ثبت], dbo.Invoices.IsCheckedout AS وضعیت FROM            dbo.Invoices INNER JOIN dbo.Customers ON dbo.Invoices.customer_id = dbo.Customers.id INNER JOIN dbo.Users ON dbo.Invoices.user_id = dbo.Users.id";
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
            SqlCommand cmd = new SqlCommand("dbo.Search_Invoice");
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
        public string ReadInvoiceNum()
        {
            var q = db.invoices.OrderByDescending(i => i.InvoiceNumber).FirstOrDefault();
            return q.InvoiceNumber;
        }
        public string CountInvoice()
        {
            return db.invoices.Count().ToString();
        }
        public List<Invoice> StockInvoice(int id)
        {
            return db.invoices.Where(i => i.User.id == id).ToList();
        }
    }
}
