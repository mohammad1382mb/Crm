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
    public partial class settingapp : Form
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
        public settingapp()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }
        ActivityCategoryBLL acbll = new ActivityCategoryBLL();
        void DataGrid()
        {
            
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = acbll.Read();
            dataGridViewX1.Columns["آیدی"].Visible = false;
            dataGridViewX1.Columns["Deletestatus"].Visible = false;
        }
        MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
        UserBLL ubll = new UserBLL();
        
        int id;
        MsgBox mb = new MsgBox();
        private void SettingApp_Load(object sender, EventArgs e)
        {
            DataGrid();
        }
        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActivityCategory ac = acbll.Readid(id);
            label2.Text = "ویرایش دسته بندی";
            textBoxX4.Text = ac.CategoryName;
            
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells["آیدی"].Value);
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ubll.Access(w.Loadwindow, "بخش تنظیمات",4))
            {
                DialogResult dr = mb.MyShowDialog("اخطار", "آیا از حذف دسته بندی اطمینان دارید؟", "", true, true);
                if (dr == DialogResult.Yes)
                {
                    mb.MyShowDialog("اطلاعیه", acbll.Delete(id), "", false, false);
                }
                DataGrid();
            }
            else
            {
                mb.MyShowDialog("محدودیت دسترسی", "شما نمیتوانید دسته بندی ای را حدف کنید", "", true, true);
            }

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label22_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            ActivityCategory ac = new ActivityCategory();
            ac.CategoryName = textBoxX4.Text;
            if (label2.Text == "ثبت دسته بندی جدید")
            {
                if (ubll.Access(w.Loadwindow, "بخش تنظیمات", 2))
                {
                    mb.MyShowDialog("ثبت اطلاعات", acbll.Create(ac), "", false, false);
                }
                else
                {
                    mb.MyShowDialog("محدودیت دسترسی", "شما نمیتوانید دسته بندی ای را ثبت کنید", "", false, true);
                }
            }
            else
            {
                if (ubll.Access(w.Loadwindow, "بخش تنظیمات", 3))
                {
                    mb.MyShowDialog("ویرایش اطلاعات", acbll.Update(ac, id), "", false, false);
                }
                else
                {
                    mb.MyShowDialog("محدودیت دسترسی", "شما نمیتوانید دسته بندی ای را ویرایش کنید", "", false, true);
                }
            }
            DataGrid();
            textBoxX4.Text = "";
        }
        BackupRBLL burBLL = new BackupRBLL();
        FolderBrowserDialog fbd = new FolderBrowserDialog();
        OpenFileDialog ofd = new OpenFileDialog();
      
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (ubll.Access(w.Loadwindow, "بخش تنظیمات", 2))
            {
                DialogResult res = mb.MyShowDialog("توجه", "آیا میخواید اطلاعاتی که تا الان دارید کپی گرفته بشه ازش؟", "", true, true);
                if (res == DialogResult.Yes)
                {
                    fbd.ShowDialog();
                    mb.MyShowDialog("ذخیره اطلاعات", burBLL.BackUp(fbd.SelectedPath), "", false, false);
                }
            }
            else
            {
                mb.MyShowDialog("محدودیت دسترسی", "شما نمیتوانید اطلاعات را ذخیره کنید", "", false, true);
            }



        }
    }
}
