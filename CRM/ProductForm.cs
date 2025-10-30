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
    
    public partial class ProductForm : Form
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
        public ProductForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }
        ProductBLL pbll = new ProductBLL();
        MsgBox mb = new MsgBox();
        UserBLL ubll = new UserBLL();
        MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
       
        void datadrid()
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = pbll.Read();
            dataGridViewX1.Columns["آیدی"].Visible = false;
        }
        void FillTxt()
        {
            textBoxX1.Text = "";
            textBoxX2.Text = "";
            textBoxX4.Text = "";
            numericUpDown1.Value = Convert.ToDecimal("0");
        }
        int id;
        int index;
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Product_Load(object sender, EventArgs e)
        {
            datadrid();
            label15.Text = pbll.CountProduct();
        }

       
        private void dataGridViewX1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells["آیدی"].Value);
        }

        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Product p = pbll.ReadBkid(id);
            textBoxX4.Text = p.Name;
            textBoxX1.Text = Convert.ToString(p.Price);
            numericUpDown1.Value = p.Stock;
            label3.Text = "ویرایش کالا";
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ubll.Access(w.Loadwindow, "بخش کالا", 4))
            {
                DialogResult dr = mb.MyShowDialog("اخطار","شما اطمینان دارید از اینکه کالا را حذف کنید؟","",true,true);
                if (dr == DialogResult.Yes)
                {
                    mb.MyShowDialog("حذف کالا", pbll.Delete(id), "", false, false);
                    
                }
                datadrid();
            }
            else
            {
                mb.MyShowDialog("محدودیت دسترسی", "شما نمیتوانید کالایی را حدف کنید", "", true, true);
            }
            
        }

        private void textBoxX2_TextChanged(object sender, EventArgs e)
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
            dataGridViewX1.DataSource = pbll.Search(textBoxX2.Text, index);
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Product p = new Product();
            try
            {
                p.Name = textBoxX4.Text;
                p.Price = Convert.ToInt32(textBoxX1.Text);
                p.Stock = Convert.ToInt32(numericUpDown1.Value);
            }
            catch (Exception x)
            {
                mb.MyShowDialog("هشدار","اطلاعات را به درستی وارد کنید",x.Message,false,true);
            }
           
            if (textBoxX1.Text != "" && textBoxX4.Text != ""  && numericUpDown1.Value != 0)
            {
                if (label3.Text == "ثبت کالا")
                {
                    if (ubll.Access(w.Loadwindow, "بخش کالا", 2))
                    {
                        mb.MyShowDialog("ثبت کالا", pbll.Create(p), "", false, false);
                    }
                    else
                    {
                        mb.MyShowDialog("محدودیت دسترسی", "شما نمیتوانید کالایی را ثبت کنید", "", false, true);
                    }
                }
                else
                {
                    if (ubll.Access(w.Loadwindow, "بخش کالا", 3))
                    {
                        mb.MyShowDialog("ویرایش اطلاعات", pbll.Update(p, id), "", false, false);
                    }
                    else
                    {
                        mb.MyShowDialog("محدودیت دسترسی", "شما نمیتوانید کالایی را ویرایش کنید", "", false, true);
                    }

                }
            }
            else
            {
                mb.MyShowDialog("اخطار", "اطلاعات وارد نشده لطفا با دقت اطلاعات را وارد کنید", "",false,true);
            }
            
            FillTxt();
            datadrid();
        }

        private void textBoxX1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
