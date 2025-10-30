using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BE;
using DAL;

namespace BLL
{
    public class CustomerBLL
    {
        CustomrtDAL cdal = new CustomrtDAL();
        public string Create(Customer c)
        {
            if (cdal.ReadCheack(c))
            {
                return cdal.Create(c);
            }
            else
            {
                return "این شماره قبلا ثبت شده است";
            }
        }
        public DataTable Read()
        {
            return cdal.Read();
        }
        public Customer Read(int id)
        {
            return cdal.Read(id);
        }
        public DataTable Search(string s, int index)
        {
            return cdal.Search(s, index);
        }
        public string Update(Customer c, int id)
        {
            return cdal.Update(c, id);
        }
        public string Delete( int id)
        {
            return cdal.Delete(id);
        }
        public Customer ReadN(string n)
        {
            return cdal.ReadN(n);
        }
        public Customer Readp(string s)
        {
            return cdal.Readp(s);
        }
        public List<string> Readphone()
        {
            return cdal.Readphone();
        }
        public string CountCustomer()
        {
            return cdal.CountCustomer();
        }





    }
}
