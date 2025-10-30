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
    public class ProductBLL
    {
        ProductDAL pdal = new ProductDAL();
        public string Create(Product p)
        {
            return pdal.Create(p);
        }
        public DataTable Read()
        {
            return pdal.Read();
        }
        public Product ReadBkid(int id)
        {
            return pdal.ReadBkid(id);
        }
        public List<string> ReadName()
        {
            return pdal.ReadName();
        }
        public Product ReadN(string s)
        {
            return pdal.ReadN(s);
        }
        public string Update(Product p , int id)
        {
            return pdal.Update(p,id);
        }
        public string Delete (int id)
        {
            return pdal.Delete(id);
        }
        public DataTable Search(string s, int index)
        {
            return pdal.Search(s,index);
        }
        //public string Readp()
        //{
        //    return pdal.Readp();
        //}
        //public string counts()
        //{
        //    return pdal.counts();
        //}
        public List<Product> CountPS()
        {
            return pdal.CountPS();
        }
        public string CountProduct()
        {
            return pdal.CountProduct();
        }
        public Product CountPN()
        {
            return pdal.CountPN();
        }


    }
}
