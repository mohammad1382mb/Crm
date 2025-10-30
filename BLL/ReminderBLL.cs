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
    public class ReminderBLL
    {
        ReminderDAL rdal = new ReminderDAL();
        public string Create(Reminders r,User u)
        {
            return rdal.Create(r,u);
        }
        public DataTable Read()
        {
            return rdal.Read();
        }
        public DataTable Search(string s)
        {
            return rdal.Search(s);
        }
        public Reminders Readid(int id)
        {
            return rdal.Readid(id);
        }
        public string Update(Reminders r, int id)
        {
            return rdal.Update(r,id);
        }
        public string Delete(int id)
        {
            return rdal.Delete(id);
        }
        public string IsDone(int id)
        {
            return rdal.IsDone(id);
        }
        public string CountReminder()
        {
            return rdal.CountReminder();
        }
    }
}
