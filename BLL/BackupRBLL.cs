using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using System.Data.SqlClient;

namespace BLL
{
    public class BackupRBLL
    {
        BackupRDAL DAL = new BackupRDAL();
        public string BackUp(string Path)
        {
            return DAL.BackUp(Path);
        }
        public string Restore(string Path)
        {
            return DAL.Restore(Path);
        }
    }
}
