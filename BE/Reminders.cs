using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Reminders
    {
        public Reminders()
        {
            IsDone = false;
        }
        public int id { get; set; }
        public string Title { get; set; }
        public string IonfoReminder { get; set; }
        public Nullable <bool> IsDone { get; set; }
        public DateTime RegReminder { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime ReminderDate { get; set; }
        public User users { get; set; }
       
    }
}
