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
using System.Windows.Controls;
using System.Windows.Forms;
using BE;
using BLL;

namespace CRM
{
    public partial class ActivityForm : Form
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
        public ActivityForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }
        void datadrid()
        {
            richTextBox1.Text = "جزئیات فعالیت";
                dataGridViewX1.DataSource = null;
                dataGridViewX1.DataSource = abll.Read();
            dataGridViewX1.Columns["آیدی"].Visible = false;
            dataGridViewX1.Columns["DeleteStatus"].Visible = false;
            dataGridViewX1.Columns["جزئیات فعالیت"].Visible = false;
        }
        void filltext()
        {
            textBoxX1.Text = "";
            textBoxX4.Text = "";
            textBoxX5.Text = "";
            richTextBox1.Text = "";
            dateTimeInput1.Text = "";
            textBoxX3.Text = "";
            textBoxX1.Enabled = true;
            textBoxX4.Enabled = true;
            textBoxX5.Enabled = true;
            checkBox1.Checked = false;
            textBoxX2.Text = "";
            richTextBox1.Text = "جزئیات فعالیت";
            
        }
        User u = new User();
        int id;
        Customer c = new Customer();
        MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
        ActivityCategory ac = new ActivityCategory();
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        ActivityBLL abll = new ActivityBLL();
        ReminderBLL rbll = new ReminderBLL();
        UserBLL ubll = new UserBLL();
        MsgBox mb = new MsgBox();
        CustomerBLL cbll = new CustomerBLL();
        ActivityCategoryBLL acbll = new ActivityCategoryBLL();
        Activity a = new Activity();
        Reminders r = new Reminders();

        private void ActivityForm_Load(object sender, EventArgs e)
        {
            
            label15.Text = abll.CountActivity();
            datadrid();
            AutoCompleteStringCollection name = new AutoCompleteStringCollection();
            foreach (var item in ubll.ReadUserName())
            {
                name.Add(item);
            }
            textBoxX1.AutoCompleteCustomSource = name;

            AutoCompleteStringCollection CPhone = new AutoCompleteStringCollection();
            foreach (var item in cbll.Readphone())
            {
                CPhone.Add(item);
            }
            textBoxX4.AutoCompleteCustomSource = CPhone;

            AutoCompleteStringCollection acName = new AutoCompleteStringCollection();
            foreach (var item in acbll.ReadCategoryName())
            {
                acName.Add(item);
            }
            textBoxX5.AutoCompleteCustomSource = acName;
        }
        private void richTextBox1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells["آیدی"].Value);
        } 

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ubll.Access(w.Loadwindow, "بخش فعالیت ها", 4))
            {
                DialogResult dr = mb.MyShowDialog("اخطار", "آیا از حذف فعالیت اطمینان دارید؟", "", true, true);
                if (dr == DialogResult.Yes)
                {
                    mb.MyShowDialog("حذف فعالیت", abll.Delete(id), "", false, false);
                    rbll.Delete(id);
                    datadrid();
                }
            }
        }
        private void جزئیاتفعالیتToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            mb.MyShowDialog("جزئیات فعالیت",abll.Readinfo(id),"",false,false);
        }

        private void dateTimeInput1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (textBoxX4.Text != "" && textBoxX5.Text != "" && textBoxX1.Text != "" && textBoxX3.Text != "" && (richTextBox1.Text != "" && richTextBox1.Text != "جزئیات فعالیت"))
            {
                if (textBoxX4.Enabled == false && textBoxX5.Enabled == false && textBoxX1.Enabled == false)
                {
                    if (ubll.Access(w.Loadwindow, "بخش فعالیت ها", 2))
                    {
                            a.Title = textBoxX3.Text;
                            //c = cbll.ReadN(textBoxX4.Text);
                            a.InofActivity = richTextBox1.Text;
                            a.RegActivity = DateTime.Now;
                            mb.MyShowDialog("ثبت فعالیت", abll.Create(a, u, c, ac), "", false, false);
                            if (checkBox1.Checked)
                            {
                                
                                r.Title = textBoxX3.Text;
                                r.IonfoReminder = richTextBox1.Text;
                                r.RegReminder = DateTime.Now;
                                r.ReminderDate = dateTimeInput1.Value;
                                mb.MyShowDialog("ویرایش اطلاعات", rbll.Create(r, u), "", false, false);
                            }
                            datadrid();
                    }
                }
                else
                {
                    mb.MyShowDialog("اخطار", "لطفا اطلاعات مشتری و کاربر و دسته بندی فعالیت که وارد کردید را تایید کنید", "", false, true);
                }
            }
            else
            {
                mb.MyShowDialog("اخطار", "لطفا تمامی مقادیر را وارد کنید و جزئیات فعالیت را هم بنویسید","",false,true);
            }
            filltext();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            textBoxX4.Enabled = false;
            c = cbll.Readp(textBoxX4.Text);
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            textBoxX1.Enabled = false;
            u = ubll.ReadU(textBoxX1.Text);
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            textBoxX5.Enabled = false;
            ac = acbll.ReadN(textBoxX5.Text);
        }

        private void textBoxX2_TextChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked && radioButton2.Checked || !radioButton1.Checked && !radioButton2.Checked)
            {
                dataGridViewX1.DataSource = null;
                dataGridViewX1.DataSource = abll.Search(textBoxX2.Text, 0);
            }
            else if (radioButton2.Checked)
            {
                dataGridViewX1.DataSource = null;
                dataGridViewX1.DataSource = abll.Search(textBoxX2.Text, 1);
            }
            else if (radioButton1.Checked)
            {
                dataGridViewX1.DataSource = null;
                dataGridViewX1.DataSource = abll.Search(textBoxX2.Text, 2);
            }
            if (textBoxX2.Text == "")
            {
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                datadrid();
            }

        }

    }
}
