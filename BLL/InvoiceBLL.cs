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
    public class InvoiceBLL
    {
        InvoiceDAL idll = new InvoiceDAL();
        public string Create(Invoice i, Customer c, List<Product> p, User u)
        {
            return idll.Create(i,c,p,u);
        }
       
        public DataTable Read()
        {
            return idll.Read();
        }
        public string ReadInvoiceNum()
        {
            return idll.ReadInvoiceNum();
        }
        public string CountInvoice()
        {

            return idll.CountInvoice();
        }
        public List<Invoice> StockInvoice(int id)
        {
            return idll.StockInvoice(id);
        }
        public DataTable Search(string s)
        {
            return idll.Search(s);
        }
    }
}
