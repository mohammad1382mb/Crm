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
    public class ProductDAL
    {
        DB db = new DB();
        public string Create(Product p)
        {
            try
            {
                db.products.Add(p);
                db.SaveChanges();
                return "ثبت کالا با موفقیت انجام شد";
            }
            catch (Exception e)
            {
                return "در ثبت کالا به مشکلی خوردید/n" + e.Message;

            }
        }
        public DataTable Read()
        {
            string cmd = "SELECT        id AS آیدی, Name AS [نام کالا], Price AS قیمت, Stock AS تعداد FROM dbo.Products WHERE(DeleteStatus = 0)";
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CRM;Integrated Security=True");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }
        public DataTable Search(string s, int index)
        {
            SqlCommand cmd = new SqlCommand();
            if (index == 0)
            {
                cmd.CommandText = "dbo.SearchProduct";
            }
            else if (index == 1)
            {
                cmd.CommandText = "dbo.SearchProductName";
            }
            else if (index == 2)
            {
                cmd.CommandText = "dbo.SearchProductPrice";
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
        public Product ReadBkid(int id)
        {
            return db.products.Find(id);
        }
        public List<string> ReadName()
        {
            return db.products.Where(i => i.DeleteStatus == false).Select(i => i.Name).ToList();
        }
        public Product ReadN(string s)
        {
            return db.products.Where(i => i.Name == s).FirstOrDefault();
        }
        public string Update (Product p , int id)
        {
            try
            {
               
                    Product ps = ReadBkid(id);
                    ps.Name = p.Name;
                    ps.Price = p.Price;
                    ps.Stock = p.Stock;
                    db.SaveChanges();
                    return "ویرایش کالا با موفقیت انجام شد";
               
               
            }
            catch (Exception e)
            {

                return "در ویرایش مشتری به مشکلی برخوردید/n" + e.Message;
            }
        }
        public string Delete(int id)
        {
            try
            {
                Product p = ReadBkid(id);
                p.DeleteStatus = true;
                db.SaveChanges();
                return "حذف کالا با موفقیت انجام شد";
            }
            catch (Exception e)
            {

                return "در حذف کالا به مشکلی برخوردید" + e.Message;
            }
            
        }
        //public string Readp()
        //{
        //    return db.products.Select(i => i.Name).FirstOrDefault();
        //}
        //public string counts()
        //{
        //    return db.products.Select(i => i.Stock.ToString()).FirstOrDefault();
        //}
        public List<Product> CountPS()
        {
            return db.products.Where(i => i.DeleteStatus == false).ToList();
        }
        public string CountProduct()
        {
            return db.products.Where(i => i.DeleteStatus == false).Count().ToString();
        }
        public Product CountPN()
        {
            return db.products.Where(i => i.DeleteStatus == false).FirstOrDefault();
        }


    }
}
