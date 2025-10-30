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
using System.Windows.Media.Imaging;
using BE;
using BLL;

namespace CRM
{
    public partial class Reminder : Form
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
        public Reminder()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));

        }
        MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
        UserBLL ubll = new UserBLL();
        ReminderBLL rbll = new ReminderBLL();
        MsgBox mb = new MsgBox();
        User u = new User();
        int id;
        void DataGrid()
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = rbll.Read();
            dataGridViewX1.Columns["DeleteStatus"].Visible = false;
            dataGridViewX1.Columns["id"].Visible = false;
            richTextBox1.Text = "جزئیات یادآور";
        }
        void FillText()
        {
            textBoxX4.Enabled = true;
            textBoxX4.Text = "";
            textBoxX1.Text = "";
            richTextBox1.Text = "";
            dateTimeInput1.Text = "";
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void Reminder_Load(object sender, EventArgs e)
        {
            
            label15.Text = rbll.CountReminder();
            AutoCompleteStringCollection names = new AutoCompleteStringCollection();
            foreach (var item in ubll.ReadUserName())
            {
                names.Add(item);
            }
            textBoxX4.AutoCompleteCustomSource = names;
            DataGrid();
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (textBoxX4.Text != "" && textBoxX1.Text != "" && richTextBox1.Text != "" && richTextBox1.Text != "جزئیات یادآور" && dateTimeInput1.Text != "")
            {
                if (textBoxX4.Enabled == false)
                {
                    if (ubll.Access(w.Loadwindow, "بخش یاد آور ها", 2))
                    {
                        Reminders r = new Reminders();
                        r.Title = textBoxX1.Text;
                        r.IonfoReminder = richTextBox1.Text;
                        r.RegReminder = DateTime.Now;
                        r.ReminderDate = dateTimeInput1.Value;
                        mb.MyShowDialog("اطلاعیه", rbll.Create(r, u), "", false, false);
                    }
                }
                else
                {
                    mb.MyShowDialog("اخطار", "لطفا نام کاربر وارد شده را تایید کنید", "", false, true);
                }
                
            }
            else
            {
                mb.MyShowDialog("اخطار","لطفا تمامی اطلاعات یادآورتون را وارد کنید", "", false, true);
            }
            
            textBoxX4.Enabled = true;
            FillText();
            DataGrid();
            
            
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            textBoxX4.Enabled = false;
            u = ubll.ReadU(textBoxX4.Text);
        }

        private void textBoxX2_TextChanged(object sender, EventArgs e)
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = rbll.Search(textBoxX2.Text);
            dataGridViewX1.Columns["DeleteStatus"].Visible = false;
        }

        private void انجامشدهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mb.MyShowDialog("",rbll.IsDone(id),"",false,false);
            DataGrid();
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X,Cursor.Position.Y);
            id = Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells["id"].Value.ToString());
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res = mb.MyShowDialog("اخطار","از حذف یادآور اطمینان دارید؟","",true,true);
            if (res == DialogResult.Yes)
            {
                mb.MyShowDialog("حذف یادآور", rbll.Delete(id), "", false, false);
            }
            DataGrid();
        }
    }
}
