using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BE;

namespace DAL
{
    public class DashbordDAL
    {
        DB db = new DB();     
        public string CountReminders(User u)
        {
            User q = db.users.Include("reminders").Where(i => i.id == u.id).FirstOrDefault();
            return q.reminders.Where(i => i.ReminderDate.Date == DateTime.Today && i.IsDone == false && i.DeleteStatus == false).Count().ToString();
        }
        public string CountCustomer()
        {
            return db.customers.Where(i => i.DeleteStatus == false).Count().ToString();
        }
        public string CountSells()
        {
            int sum = 0;
            foreach (var item in db.invoices)
            {
                if (item.RegInvoice.Date == DateTime.Today)
                {
                    sum = sum + 1;
                }
                
            }
            return sum.ToString();

        }
        public List<Reminders> GridReminders(User u)
        {
            return db.reminders.Include("users").Where(i => i.users.id == u.id).ToList();
        }
        public Reminders Read()
        {
            return db.reminders.Where(i => i.DeleteStatus == false).ToList().FirstOrDefault();
        }
        public void UpdateIsDoneInDatabase(Reminders reminder)
        {
            using (var dbContext = new DB())
            {
                // یافتن یک یادآور با استفاده از شناسه
                var existingReminder = dbContext.reminders.Find(reminder.id);

                if (existingReminder != null)
                {
                    // تغییر مقدار IsDone به true
                    existingReminder.IsDone = true;

                    // ذخیره تغییرات در دیتابیس
                    dbContext.SaveChanges();
                }
            }
        }
    }
}
