using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Customer
    {
        public Customer()
        {
            DeleteStatus = false;
        }
        public int id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public bool DeleteStatus { get; set; }
      //  [Column(TypeName = "datatime2")]
        public DateTime RegCustomer { get; set; }
        public List<Invoice> invoices { get; set; } = new List<Invoice>();
        public List<Activity> activities { get; set; } = new List<Activity>();
    }
}
