using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Invoice
    {
        public int id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime RegInvoice  { get; set; }
        public bool IsCheckedout { get; set; }
        public DateTime? CheckoutData  { get; set; }
        public Customer customer { get; set; }
        public User User { get; set; }
        public List<Product> products { get; set; } = new List<Product>();
    }
}
