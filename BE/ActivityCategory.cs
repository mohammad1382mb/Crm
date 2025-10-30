using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ActivityCategory
    {
        public ActivityCategory()
        {
            Deletestatus = false;
        }
        public int id { get; set; }
        public string CategoryName { get; set; }
        public bool Deletestatus { get; set; }
        public List<Activity> activities { get; set; } = new List<Activity>();
    }
}
