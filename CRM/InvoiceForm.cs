using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Stimulsoft.Report;
using BE;
using BLL;
using System.Windows;

namespace CRM
{
    public partial class InvoiceForm : Form
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
        public InvoiceForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }
        User u = new User();
        MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();


        UserBLL ubll = new UserBLL();
        InvoiceBLL ibll = new InvoiceBLL();
        CustomerBLL cbll = new CustomerBLL();
        Customer c = new Customer();
        ProductBLL pbll = new ProductBLL();
        List<Product> productslist = new List<Product>();
        List<Product> productslist2 = new List<Product>();
        Product p = new Product();
        double Sum = 0;
        MsgBox mb = new MsgBox();
        void datagrid2()
        {
            dataGridViewX2.DataSource = null;
            dataGridViewX2.DataSource = ibll.Read();
        }
        void datagrid1()
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = productslist.ToList();
            dataGridViewX1.Columns["Name"].HeaderText = "نام کالا";
            dataGridViewX1.Columns["Price"].HeaderText = "قیمت کالا";
            dataGridViewX1.Columns["Stock"].HeaderText = "تعداد";
            dataGridViewX1.Columns["id"].Visible = false;
            dataGridViewX1.Columns["DeleteStatus"].Visible = false;

        }
        void Nulldatagrid()
        {
            dataGridViewX1.DataSource = null;
            listBox1.Items.Clear();
            textBoxX4.Enabled = true;
            textBoxX1.Text = "";
            textBoxX4.Text = "";
            textBoxX2.Text = "0";
            checkBox1.Checked = false;
            label1.Text = "";
            label3.Text = "";
            label6.Text = "";
            label9.Text = "";
            label12.Text = "";


        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Invoice_Load(object sender, EventArgs e)
        {
            
            label6.Text = DateTime.Now.Date.ToString("yyyy/MM/dd");
            datagrid2();
            label15.Text = ibll.CountInvoice();

            AutoCompleteStringCollection CPhone = new AutoCompleteStringCollection();
            foreach (var item in cbll.Readphone())
            {
                CPhone.Add(item);
            }
            textBoxX4.AutoCompleteCustomSource = CPhone;

            AutoCompleteStringCollection Pnames = new AutoCompleteStringCollection();
            foreach (var item in pbll.ReadName())
            {
                Pnames.Add(item);
            }
            textBoxX1.AutoCompleteCustomSource = Pnames;
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Invoice i = new Invoice();
            i.RegInvoice = DateTime.Now;
            if (checkBox1.Checked)
            {

                i.IsCheckedout = true;
                i.CheckoutData = DateTime.Now;
            }
            else
            {
                i.IsCheckedout = false;
            }
           
            if (c.id == null || p.Name == null)
            {
                mb.MyShowDialog("اخطار", "لطفا مقدار ها رو وارد کنید و باکس ها رو خالی نزارید", "", false, true);
            }
            else
            {
                if (ubll.Access(w.Loadwindow, "بخش فاکتور",2))
                {
                    DialogResult res = mb.MyShowDialog("ثبت فاکتور", ibll.Create(i, c, productslist,w.Loadwindow) + " آیا قصد چاپ فاکتور را دارید؟", "", true, false);
                    if (res == DialogResult.Yes)
                    {
                        StiReport sti = new StiReport();
                        sti.Load(@"C:\Users\Pixel\source\repos\CRM\Report.mrt");
                        sti.Dictionary.Variables["InvoicNum"].Value = ibll.ReadInvoiceNum();
                        sti.Dictionary.Variables["CustomerName"].Value = label1.Text;
                        sti.Dictionary.Variables["CustomerPhone"].Value = label3.Text;
                        sti.Dictionary.Variables["Date"].Value = label6.Text;
                        sti.RegBusinessObject("Product", productslist2);
                        sti.Render();
                        sti.Show();
                    }
                }
                else
                {
                    mb.MyShowDialog("محدودیت دسترسی", "شما نمیتوانید فاکتوری را ثبت کنید", "",false, true);
                }
                datagrid2();
                Nulldatagrid();
                
            }
        }

        private void label22_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            c = cbll.Readp(textBoxX4.Text);
            textBoxX4.Enabled = false;
            label1.Text = c.Name;
            label3.Text = c.PhoneNumber;
           
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            double Adad = 0;
            p = pbll.ReadN(textBoxX1.Text);
            productslist2.Add(p);
            productslist.Add(p);
            string s = p.Name + " به ارزش" + p.Price.ToString("N0") + "تومان";
            listBox1.Items.Add(s);
            foreach (var i in productslist.ToList())
            {
              
               Sum += i.Price;
               
            }
            label9.Text = Sum.ToString("N0");
            label12.Text = Sum.ToString("N0");
            datagrid1();
            productslist.Clear();

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            label12.Text = (Sum - Convert.ToDouble(textBoxX2.Text)).ToString("N0");
        }

        private void textBoxX3_TextChanged(object sender, EventArgs e)
        {
            dataGridViewX2.DataSource = null;
            dataGridViewX2.DataSource = ibll.Search(textBoxX3.Text);
            dataGridViewX2.Columns["DeleteStatus"].Visible = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
