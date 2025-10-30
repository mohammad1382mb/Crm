using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class User
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool status { get; set; }
        public DateTime RegUser { get; set; }
        public List<Activity> activities { get; set; } = new List<Activity>();
        public List<Invoice> invoices { get; set; } = new List<Invoice>();
        public List<Reminders> reminders { get; set; } = new List<Reminders>();
        public UserGroup userGroup { get; set; }

    }
}
