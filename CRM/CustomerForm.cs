using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using BE;
using BLL;

namespace CRM
{
    public partial class CustomerForm : Form
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
        public CustomerForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }
        CustomerBLL cbll = new CustomerBLL();
        UserBLL ubll = new UserBLL();
        MsgBox mb = new MsgBox();
        MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
        
        void FillTxt()
        {
            textBoxX1.Text = "";
            textBoxX2.Text = "";
            textBoxX4.Text = "";
        }
        void datadrid()
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = cbll.Read();
            dataGridViewX1.Columns["آیدی"].Visible = false;
        }
        int index;
        int id;

        private void pictureBox2_Click(object sender, EventArgs e)
        { 
            
            this.Close();
        }
        private void textBoxX4_TextChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked && radioButton2.Checked || (!radioButton1.Checked && !radioButton2.Checked))
            {
                index = 0;
            }
            else if (radioButton1.Checked && !radioButton2.Checked)
            {
                index = 2;
            }
            else if (radioButton2.Checked && !radioButton1.Checked)
            {
                index = 1;
            }
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = cbll.Search(textBoxX4.Text,index);

        }
        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X , Cursor.Position.Y);
            id =Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells["آیدی"].Value);
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            label13.Text = cbll.CountCustomer();
            FillTxt();
            datadrid();
        }

        private void ویرایشToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Customer c = cbll.Read(id);
            textBoxX1.Text = c.Name;
            textBoxX2.Text = c.PhoneNumber;
            label3.Text = "ویرایش اطلاعات";
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ubll.Access(w.Loadwindow, "بخش مشتریان", 4))
            {
                DialogResult dr = mb.MyShowDialog("اخطار", "آیا از حذف مشتری اطمینان دارید؟ \nدر صورت حذف تمام اطلاعات مشتری هم پاک میشود", "", true, true);
                if (dr == DialogResult.Yes)
                {
                    mb.MyShowDialog("حذف مشتریان", cbll.Delete(id),"",false,false);
                }
                datadrid();
            }
            else
            {
                mb.MyShowDialog("محدودیت دسترسی", "شما نمیتوانید مشتری ای را حذف کنید ", "", false, true);
            }
            
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Customer c = new Customer();
            c.Name = textBoxX1.Text;
            c.PhoneNumber = textBoxX2.Text;
            c.RegCustomer = DateTime.Now;
            if (textBoxX1.Text != "" && textBoxX2.Text != "")
            {
                if (label3.Text == "ثبت اطلاعات")
                {
                    if (ubll.Access(w.Loadwindow, "بخش مشتریان", 2))
                    {
                        mb.MyShowDialog("ثبت اطلاعات", cbll.Create(c), "", false, false);
                    }
                    else
                    {
                        mb.MyShowDialog("محدودیت دسترسی", "شما نمیتوانید مشتری ثبت کنید ", "", false, true);
                    }
                }
                else
                {
                    if (ubll.Access(w.Loadwindow, "بخش مشتریان", 3))
                    {
                        mb.MyShowDialog("ویرایش اطلاعات", (cbll.Update(c, id)), "", false, false);
                        label3.Text = "ثبت اطلاعات";
                    }
                    else
                    {
                        mb.MyShowDialog("محدودیت دسترسی", "شما نمیتوانید اطلاعات مشتری را ویرایش کنید ", "", false, true);
                    }

                }
            }
            else
            {
                mb.MyShowDialog("اخطار","اطلاعات وارد نشده لطفا با دقت اطلاعات را وارد کنید", "",false,true);
            }
            
            datadrid();
            FillTxt();
        }
    }
}
