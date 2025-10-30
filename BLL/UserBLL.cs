using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;


namespace BLL
{
    public class UserBLL
    {
        UserDAL udal = new UserDAL();

        private string Encode(string pass)
        {
            byte[] encdata_byte = new byte[pass.Length];
            encdata_byte = System.Text.Encoding.UTF8.GetBytes(pass);
            string encodedData = Convert.ToBase64String(encdata_byte);
            return encodedData;
        }
        private string Decode(string EncodedPass)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(EncodedPass);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new string(decoded_char);
            return result;
        }
        public User ReadU(string s)
        {
            return udal.ReadU(s);
        }
        public List<string> ReadUserName()
        {
            return udal.ReadUserName();
        }
        public bool Isregistered()
        {
            return udal.Isregistered();
        }
        public string Create(User u,UserGroup ug)
        {
            u.Password = Encode(u.Password);
            return udal.Create(u,ug);
        }
        public bool ReadCheck(User u)
        {
            return udal.ReadCheck(u);
        }
        public DataTable Read()
        {
            return udal.Read();

        }
        public User Readid(int id)
        {
            return udal.Readid(id);
        }
        public string Update(User u, int id)
        {
            u.Password = Encode(u.Password);
            return udal.Update(u, id);
        }
        public string Delete(int id)
        {
            return udal.Delete(id);
        }
        public User EnterU(string u, string p)
        {
            return udal.EnterU(u, Encode(p));

        }
        public bool Access(User u, string s, int a)
        {
            return udal.Access(u,s,a);
        }
        public List<User> sellsuser()
        {
            return udal.sellsuser();
        }
        public List<Customer> CountCustomer()
        {
            return udal.CountCustomer();
        }
        public List<User> CountActivity()
        {
            return udal.CountActivity();
        }
        public string CountUser()
        {
            return udal.CountUser();
        }
    }
}
