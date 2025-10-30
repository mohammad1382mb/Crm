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
using System.IO;
using BE;
using BLL;
using System.Windows;

namespace CRM
{
    public partial class UserForm : Form
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
        public UserForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }

        Image pic;
        OpenFileDialog ofd = new OpenFileDialog();
        UserBLL ubll = new UserBLL();
        MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
        UserGroupBLL ugbll = new UserGroupBLL();
        UserGroup ug = new UserGroup();
        int id;
        void datadrid()
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = ubll.Read();
            dataGridViewX2.DataSource = null;
            dataGridViewX2.DataSource = ugbll.ReadInfo();
            dataGridViewX1.Columns["آیدی"].Visible = false;
            dataGridViewX1.Columns["رمز عبور"].Visible = false;
            dataGridViewX1.Columns["status"].Visible = false;
            label13.Text = ubll.CountUser();
        }
        void FillText()
        {
            textBoxX1.Text = "";
            textBoxX4.Text = "";
            textBoxX5.Text = "";
            textBoxX3.Text = "";
            textBoxX2.Text = "";
        }
        void fillcheckbox()
        {
            checkBox21.Checked = false;
            checkBox24.Checked = false;
            checkBox25.Checked = false;
            checkBox26.Checked = false;
            checkBox27.Checked = false;
            checkBox28.Checked = false;
            checkBox29.Checked = false;
            checkBox30.Checked = false;
            checkBox31.Checked = false;
            checkBox10.Checked = false;
            checkBox9.Checked = false;
            checkBox8.Checked = false;
            checkBox7.Checked = false;
            checkBox6.Checked = false;
            checkBox5.Checked = false;
            checkBox13.Checked = false;
            checkBox12.Checked = false;
            checkBox11.Checked = false;
            checkBox14.Checked = false;
            checkBox15.Checked = false;
            checkBox16.Checked = false;
            checkBox17.Checked = false;
            checkBox18.Checked = false;
            checkBox19.Checked = false;
            checkBox20.Checked = false;
            checkBox22.Checked = false;
            checkBox23.Checked = false;
            checkBox32.Checked = false;
            checkBox33.Checked = false;
            checkBox34.Checked = false;
            checkBox35.Checked = false;
            checkBox36.Checked = false;
            checkBox37.Checked = false;
            checkBox38.Checked = false;
            checkBox39.Checked = false;
            checkBox40.Checked = false;
            checkBox4.Checked = false;
            checkBox2.Checked = false;
            checkBox1.Checked = false;
            checkBox3.Checked = false;
            textBoxX10.Text = "";
        }
        UserAccessRole filluseraccess(string section, bool CanCreate, bool CanEnter, bool CanUpdate, bool CanDelete)
        {
            UserAccessRole uar = new UserAccessRole();
            uar.Section = section;
            uar.CanEnter = CanEnter;
            uar.CanCreate = CanCreate;
            uar.CanDelete = CanDelete;
            uar.CanUpdate = CanUpdate;
            return uar;

        }
        string SavePic(string UserName)
        {
            string path = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\UserName\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string PicName = UserName + ".JPG";
            try
            {
                string picPath = ofd.FileName;
                File.Copy(picPath, path + PicName,true);
            }
            catch (Exception e)
            {
                mb.MyShowDialog("توجه", "کاربر بدون عکس سیو شد","",false,false);
               
            }
            return path + PicName;
        }
        MsgBox mb = new MsgBox();
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ofd.Filter = "JPG(*.JPG)|*.JPG";
            ofd.Title = ".تصویر کاربر خود را انتخاب کنید";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pic = Image.FromFile(ofd.FileName);
                pictureBox2.Image = pic;
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
            
        }
        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells["آیدی"].Value);
        }
        private void UserForm_Load(object sender, EventArgs e)
        {
           datadrid();
        }

        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User u = ubll.Readid(id);
            textBoxX4.Text = u.Name;
            textBoxX1.Text = u.UserName;
            //pictureBox2.Image = Image.FromFile(u.Pic);
            string imagePath = @"C:\Users\Pixel\Downloads\add_image.png";
            Image newImage = Image.FromFile(imagePath);
            pictureBox2.Image = newImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            label3.Text = "ویرایش اطلاعات";
            textBoxX5.WatermarkText = "کلمه عبور جدید";
            textBoxX3.WatermarkText = "تکرار کلمه عبور جدید";
        }

        private void dataGridViewX1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells["آیدی"].Value);
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ubll.Access(w.Loadwindow, "بخش کاربران", 4))
            {
                DialogResult dr = mb.MyShowDialog("اخطار", "آیا از حذف کاربر اطمینان دارید", "", true, true);
                if (dr == DialogResult.Yes)
                {
                    mb.MyShowDialog("حذف کاربر", ubll.Delete(id), "", false, false);
                }
                datadrid();
            }
                
        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox21.Checked = true;
                checkBox24.Checked = true;
                checkBox25.Checked = true;
                checkBox26.Checked = true;
                checkBox27.Checked = true;
                checkBox28.Checked = true;
                checkBox29.Checked = true;
                checkBox30.Checked = true;
                checkBox31.Checked = true;
            }
            else
            {
                checkBox21.Checked = false ;
                checkBox24.Checked = false ;
                checkBox25.Checked = false ;
                checkBox26.Checked = false ;
                checkBox27.Checked = false ;
                checkBox28.Checked = false ;
                checkBox29.Checked = false ;
                checkBox30.Checked = false ;
                checkBox31.Checked = false ;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox10.Checked = true;
                checkBox9.Checked = true;
                checkBox8.Checked = true;
                checkBox7.Checked = true;
                checkBox6.Checked = true;
                checkBox5.Checked = true;
                checkBox13.Checked = true;
                checkBox12.Checked = true;
                checkBox11.Checked = true;
            }
            else
            {
                checkBox10.Checked = false ;
                checkBox9.Checked = false ;
                checkBox8.Checked = false ;
                checkBox7.Checked = false ;
                checkBox6.Checked = false ;
                checkBox5.Checked = false ;
                checkBox13.Checked = false ;
                checkBox12.Checked = false ;
                checkBox11.Checked = false ;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox14.Checked = true;
                checkBox15.Checked = true;
                checkBox16.Checked = true;
                checkBox17.Checked = true;
                checkBox18.Checked = true;
                checkBox19.Checked = true;
                checkBox20.Checked = true;
                checkBox22.Checked = true;
                checkBox23.Checked = true;
            }
            else
            {
                checkBox14.Checked = false ;
                checkBox15.Checked = false ;
                checkBox16.Checked = false ;
                checkBox17.Checked = false ;
                checkBox18.Checked = false ;
                checkBox19.Checked = false ;
                checkBox20.Checked = false ;
                checkBox22.Checked = false ;
                checkBox23.Checked = false ; 
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                checkBox32.Checked = true;
                checkBox33.Checked = true;
                checkBox34.Checked = true;
                checkBox35.Checked = true;
                checkBox36.Checked = true;
                checkBox37.Checked = true;
                checkBox38.Checked = true;
                checkBox39.Checked = true;
                checkBox40.Checked = true;
            }
            else
            {
                checkBox32.Checked = false ;
                checkBox33.Checked = false ;
                checkBox34.Checked = false ;
                checkBox35.Checked = false ;
                checkBox36.Checked = false ;
                checkBox37.Checked = false ;
                checkBox38.Checked = false ;
                checkBox39.Checked = false ;
                checkBox40.Checked = false ;
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (textBoxX1.Text != "" && textBoxX4.Text != "" && textBoxX5.Text != "" && textBoxX3.Text != "" && textBoxX2.Text != "")
            {
                User u = new User();
                u.Name = textBoxX4.Text;
                u.UserName = textBoxX1.Text;
                u.RegUser = DateTime.Now;
                ug = ugbll.ReadN(textBoxX2.Text);
                if (textBoxX5.Text == textBoxX3.Text)
                {
                    u.Password = textBoxX5.Text;
                }
                else
                {
                    mb.MyShowDialog("اخطار", "تکرار کلمه عبور اشتباه میباشد", "", false, true);

                }
                if (label3.Text == "ثبت کاربر")
                {
                    if (ubll.Access(w.Loadwindow, "بخش کاربران", 2))
                    {
                        u.Pic = SavePic(textBoxX1.Text);
                        mb.MyShowDialog("ثبت کاربر", ubll.Create(u, ug), "", false, false);
                    }
                }
                else if (label3.Text == "ویرایش اطلاعات")
                {
                    if (ubll.Access(w.Loadwindow, "بخش کاربران", 3))
                    {
                        u.Pic = SavePic(textBoxX1.Text);
                        mb.MyShowDialog("ویرایش کاربر", ubll.Update(u, id), "", false, false);
                        string imagePath = @"C:\Users\Pixel\Downloads\add_image.png";
                        Image newImage = Image.FromFile(imagePath);
                        pictureBox2.Image = newImage;
                        pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
                        label3.Text = "ثبت کالا";
                        textBoxX5.WatermarkText = "کلمه عبور";
                        textBoxX3.WatermarkText = "تکرار کلمه عبور";
                    }
                }
            }
            else
            {
                mb.MyShowDialog("اخطار","لطفا اطلاعات کاربر جدید را وارد کنید و عکسی برایش انتخاب کنید","",false,true);
            }
            datadrid();
            FillText();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (textBoxX10.Text != "")
            {
                ug.Name = textBoxX10.Text;
                ug.userAccessRoles.Add(filluseraccess(label4.Text, checkBox20.Checked, checkBox10.Checked, checkBox31.Checked, checkBox39.Checked));
                ug.userAccessRoles.Add(filluseraccess(label6.Text, checkBox19.Checked, checkBox9.Checked, checkBox30.Checked, checkBox38.Checked));
                ug.userAccessRoles.Add(filluseraccess(label14.Text, checkBox18.Checked, checkBox8.Checked, checkBox29.Checked, checkBox37.Checked));
                ug.userAccessRoles.Add(filluseraccess(label15.Text, checkBox17.Checked, checkBox7.Checked, checkBox28.Checked, checkBox36.Checked));
                ug.userAccessRoles.Add(filluseraccess(label16.Text, checkBox16.Checked, checkBox6.Checked, checkBox27.Checked, checkBox35.Checked));
                ug.userAccessRoles.Add(filluseraccess(label17.Text, checkBox15.Checked, checkBox5.Checked, checkBox26.Checked, checkBox34.Checked));
                ug.userAccessRoles.Add(filluseraccess(label18.Text, checkBox14.Checked, checkBox13.Checked, checkBox25.Checked, checkBox33.Checked));
                ug.userAccessRoles.Add(filluseraccess(label20.Text, checkBox23.Checked, checkBox12.Checked, checkBox24.Checked, checkBox32.Checked));
                ug.userAccessRoles.Add(filluseraccess(label19.Text, checkBox22.Checked, checkBox11.Checked, checkBox21.Checked, checkBox40.Checked));
                mb.MyShowDialog("اطلاعیه ثبت", ugbll.Create(ug), "", true, false);
            }
            else
            {
                mb.MyShowDialog("اخطار","لطفا نام گروه کاربری را وارد کنید","",false,true);
            }
            datadrid();
            fillcheckbox();
            
        }

        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
