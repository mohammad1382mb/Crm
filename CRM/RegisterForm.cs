using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using BE;
using BLL;
using System.Windows;
using FoxLearn.License;



namespace CRM
{
    public partial class RegisterForm : UserControl
    {
        public RegisterForm()
        {
            InitializeComponent();
        }
        MsgBox mb = new MsgBox();
        Timer t1 = new Timer();
        void _switchPanel()
        {
            t1.Enabled = true;
            t1.Interval = 15;
            t1.Tick += Timer1_Tick;
            t1.Start();
        }
        int y = 176;
        OpenFileDialog ofd = new OpenFileDialog();
        UserBLL ubll = new UserBLL();
        UserGroupBLL ugbll = new UserGroupBLL();
        User u = new User();
        MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
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
        public void CreateAdminGroup()
        {
            UserGroup ug = new UserGroup();
            ug.Name = "مدیریت";
            ug.userAccessRoles.Add(filluseraccess("بخش مشتریان", true, true, true, true));
            ug.userAccessRoles.Add(filluseraccess("بخش کالا", true, true, true, true));
            ug.userAccessRoles.Add(filluseraccess("بخش فاکتور", true, true, true, true));
            ug.userAccessRoles.Add(filluseraccess("بخش فعالیت ها", true, true, true, true));
            ug.userAccessRoles.Add(filluseraccess("بخش یاد آور ها", true, true, true, true));
            ug.userAccessRoles.Add(filluseraccess("بخش کاربران", true, true, true, true));
            ug.userAccessRoles.Add(filluseraccess("پنل پیامکی", true, true, true, true));
            ug.userAccessRoles.Add(filluseraccess("بخش گزارشات", true, true, true, true));
            ug.userAccessRoles.Add(filluseraccess("بخش تنظیمات", true, true, true, true));
            ugbll.Create(ug);
        }
        
        private void symbolBox2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(textBoxX1.Text);
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (panel1.Location.Y < 462)
            {
                y = y + 15;
                panel1.Location = new System.Drawing.Point(3, y);

            }
            else
            {
                t1.Stop();
                panel2.Visible = true;
            }
        }
        private void RegisterForm_Load_1(object sender, EventArgs e)
        {
            textBoxX1.Text = ComputerInfo.GetComputerId();
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (textBoxX2.Text != "")
            {
                KeyManager km = new KeyManager(textBoxX1.Text);
                string productKey = textBoxX2.Text;

                if (km.ValidKey(ref productKey))
                {
                    KeyValuesClass kv = new KeyValuesClass();
                    if (km.DisassembleKey(productKey, ref kv))
                    {
                        LicenseInfo lic = new LicenseInfo();
                        lic.ProductKey = productKey;
                        lic.FullName = "FoxLearn";
                        if (kv.Type == LicenseType.TRIAL)
                        {
                            lic.Day = kv.Expiration.Day;
                            lic.Month = kv.Expiration.Month;
                            lic.Year = kv.Expiration.Year;
                        }
                        km.SaveSuretyFile(string.Format(@"{0}\Key.lic", System.Windows.Forms.Application.StartupPath), lic);
                        mb.MyShowDialog("تبریک", "شما با موفقیت وارد برنامه شدید", "", false, false);
                        _switchPanel();
                    }
                }
                else
                {
                    label2.Visible = true;
                    pictureBox4.Visible = true;
                    textBoxX2.Text = "";
                }
            }
            else
            {
                mb.MyShowDialog("اخطار","کد لایسنس را وارد بکنید","",false,true);
            }
            
        }
       
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (textBoxX3.Text != "" || textBoxX4.Text != "" || textBoxX5.Text != "" || textBoxX6.Text != "" )
            {
                UserGroup ug = new UserGroup();
                u.Name = textBoxX4.Text;
                u.UserName = textBoxX3.Text;
                u.RegUser = DateTime.Now;
                CreateAdminGroup();
                if (textBoxX5.Text == textBoxX6.Text)
                {
                    u.Password = textBoxX5.Text;
                    if (ubll.ReadCheck(u) == true)
                    {
                        mb.MyShowDialog("ثبت", ubll.Create(u, ugbll.ReadN("مدیریت")), "", false, false);
                        this.Visible = false;
                        ((LoudingForm)System.Windows.Forms.Application.OpenForms["LoudingForm"]).Login();
                    }
                    else
                    {
                        mb.MyShowDialog("اخطار", "نام کاربری شما تکراری میباشد", "", true, false);
                        
                    }
                    
                }
                else
                {
                    label6.Visible = true;
                    pictureBox2.Visible = true;
                    textBoxX5.Text = "";
                    textBoxX6.Text = "";
                }
                
            }
            else
            {
                mb.MyShowDialog("اخطار","لطفا تمام اطلاعات وارد کنید و عکسی انتخاب کنید","",false,true);
            }
            
        }

       

        private void textBoxX2_TextChanged(object sender, EventArgs e)
        {
            if (textBoxX2.Text != "")
            {
                label2.Visible = false;
                pictureBox4.Visible = false;
            }
        }

        private void textBoxX4_TextChanged(object sender, EventArgs e)
        {
            if (textBoxX4.Text != "")
            {
                label6.Visible = false;
                pictureBox2.Visible = false;
            }
        }

        private void textBoxX3_TextChanged(object sender, EventArgs e)
        {
            if (textBoxX3.Text != "")
            {
                label6.Visible = false;
                pictureBox2.Visible = false;
            }
        }

        private void textBoxX5_TextChanged(object sender, EventArgs e)
        {
            if (textBoxX5.Text != "")
            {
                label6.Visible = false;
                pictureBox2.Visible = false;
            }
        }

        private void textBoxX6_TextChanged(object sender, EventArgs e)
        {
            if (textBoxX6.Text != "")
            {
                label6.Visible = false;
                pictureBox2.Visible = false;
            }
        }
        BackupRBLL burBLL = new BackupRBLL();
        
        
    }
}
