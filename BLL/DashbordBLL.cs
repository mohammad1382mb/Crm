using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;


namespace BLL
{
    public class DashbordBLL
    {
        DashbordDAL dal = new DashbordDAL();
        public string CountReminders(User u)
        {
            return dal.CountReminders(u);
        }
        public string CountCustomer()
        {
            return dal.CountCustomer();
        }
        public string CountSells()
        {
            return dal.CountSells();
        }
        public List<Reminders> GridReminders(User u)
        {
            return dal.GridReminders(u);
        }
        public Reminders Read()
        {
            return dal.Read();
        }
        public void UpdateIsDoneInDatabase(Reminders reminder)
        {
             dal.UpdateIsDoneInDatabase(reminder);
        }
    }
}
