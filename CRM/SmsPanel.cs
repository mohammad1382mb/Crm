using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;
using BLL;

namespace CRM
{
    public partial class SmsPanel : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
     (
         int nLeftRect,     // x-coordinate of upper-left corner
         int nTopRect,      // y-coordinate of upper-left corner
         int nRightRect,    // x-coordinate of lower-right corner
         int nBottomRect,   // y-coordinate of lower-right corner
         int nWidthEllipse, // width of ellipse
         int nHeightEllipse // height of ellipse
     );
        public SmsPanel()
        {
            
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }
        List<string> customerlist = new List<string>();
        MsgBox mb = new MsgBox();
        CustomerBLL cbll = new CustomerBLL();
        public class BulkSendModel
        {
            public long LineNumber { get; set; }
            public string MessageText { get; set; }
            public string[] Mobiles { get; set; }
            public int? SendDateTime { get; set; }
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label22_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void SmsPanel_Load(object sender, EventArgs e)
        {
            richTextBox2.Text = "پیام مورد نظر خود را در اینجا بنویسید";
            richTextBox1.Text = "پیام مورد نظر خود را در اینجا بنویسید";
            AutoCompleteStringCollection PhoneNumber = new AutoCompleteStringCollection();
            foreach (var item in cbll.Readphone())
            {
                PhoneNumber.Add(item);
            }
            textBoxX1.AutoCompleteCustomSource = PhoneNumber;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (textBoxX1.Text!= "")
            {
                Customer c = new Customer();
                c = cbll.Readp(textBoxX1.Text);
                customerlist.Add(c.ToString());
                string ph = c.PhoneNumber + "به نام " + c.Name;
                listBox3.Items.Add(ph);
            }
            else
            {
                mb.MyShowDialog("اخطار", "لطفا شماره مورد نظر را انتخاب و وارد کنید", "", false, true);
            }
            textBoxX1.Text = "";
        }

        private async void pictureBox6_Click(object sender, EventArgs e)
        {
            if (richTextBox2.Text != "" && richTextBox2.Text != "پیام مورد نظر خود را در اینجا بنویسید")
            {
                HttpClient httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Add("x-api-key", "zUb1eonffckgDlL9zfqcPDd0qgkmZAmU1WIm1MTSOTruqvhZOSK3FDpfgN7G63SP");

                BulkSendModel model = new BulkSendModel()
                {
                    LineNumber = 30007732906335,
                    MessageText = richTextBox2.Text,
                    Mobiles = customerlist.ToArray()
                };

                string payload = JsonConvert.SerializeObject(model);

                StringContent stringContent = new StringContent(payload, Encoding.UTF8, "application/json");
                try
                {
                    HttpResponseMessage response = await httpClient.PostAsync("https://api.sms.ir/v1/send/bulk", stringContent);

                    string sendResult = await response.Content.ReadAsStringAsync();
                    mb.MyShowDialog("موفقیت", "پیامک ها با موفقیت ارسال شد", sendResult, false, false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("خطا در ارسال پیامک" + ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    mb.MyShowDialog("خطا", "خطا در ارسال پیامک ها", ex.Message, false, true);
                }
            }
            else
            {
                mb.MyShowDialog("اخطار", "شماره ها و پیامکی که میخواید ارسال بشه رو بنویسید", "", false, true);
            }
            listBox3.Items.Clear();
            richTextBox2.Text = "پیام مورد نظر خود را در اینجا بنویسید";
        }

        private async void pictureBox7_Click(object sender, EventArgs e)
        {
            if (textBoxX4.Text != "" && richTextBox1.Text != "" && richTextBox1.Text != "پیام مورد نظر خود را در اینجا بنویسید")
            {
                HttpClient httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Add("x-api-key", "zUb1eonffckgDlL9zfqcPDd0qgkmZAmU1WIm1MTSOTruqvhZOSK3FDpfgN7G63SP");

                BulkSendModel model = new BulkSendModel()
                {
                    LineNumber = 30007732906335,
                    MessageText = richTextBox1.Text,
                    Mobiles = new string[] { textBoxX4.Text }
                };

                string payload = JsonConvert.SerializeObject(model);

                StringContent stringContent = new StringContent(payload, Encoding.UTF8, "application/json");
                try
                {
                    HttpResponseMessage response = await httpClient.PostAsync("https://api.sms.ir/v1/send/bulk", stringContent);

                    string sendResult = await response.Content.ReadAsStringAsync();

                    mb.MyShowDialog("موفقیت", "پیامک با موفقیت ارسال شد", sendResult, false, false);

                }
                catch (Exception ex)
                {

                    //mb.MyShowDialog("خطا", "خطا در ارسال پیامک", ex.Message, false, true);
                }
            }
            else
            {
                mb.MyShowDialog("اخطار", "شماره و پیامکی که میخواید ارسال بشه رو بنویسید","", false, true);
            }
           
            richTextBox1.Text = "پیام مورد نظر خود را در اینجا بنویسید";
            textBoxX4.Text = "";
        }
        private void textBoxX1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBoxX4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void richTextBox2_Click(object sender, EventArgs e)
        {
           richTextBox2.Text = "";
        }

        private void richTextBox1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }
    }
}
