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
using BLL;
using BE;

namespace CRM
{
    public partial class LoudingForm : Form
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
        public LoudingForm()
        {
            this.Controls.Add(rf);
            this.Controls["RegisterForm"].Location = new Point(347, 1110);
            this.Controls.Add(en);
            this.Controls["EnterUser"].Location = new Point(347, 1110);
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }
        UserBLL ubll = new UserBLL();
        #region Timer
        Timer t1 = new Timer();
        Timer t2 = new Timer();
        Timer t3 = new Timer();
        #endregion
        RegisterForm rf = new RegisterForm();
        EnterUser en = new EnterUser();
        bool _isregistered;
        int y = 324;
        int y1 = 1110;
        int y2 = 1110;
        public void Login()
        {
            t3.Enabled = true;
            t3.Interval = 1;
            t3.Tick += Timer2_Tick;
            t3.Start();
        }
        private void LoudingForm_Load(object sender, EventArgs e)
        {
            label5.Visible = true;
            t1.Enabled = true;
            t1.Interval = 15;
            t1.Tick += Timer_Tick;
            t1.Start();
            
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (progressBarX1.Value >= 100)
            {
                t1.Stop();
                progressBarX1.Visible = false;
                label5.Visible = false;
                label6.Visible = true;
                if (label5.Text == "در حال بارگذاری دوباره اطلاعات... ")
                {
                    pictureBox3.Visible = false;
                    label7.Visible = false;
                }
                else
                {
                    pictureBox3.Visible = true;
                    label7.Visible = true;
                }
                t2.Enabled = true;
                t2.Interval = 1;
                t2.Tick += Timer1_Tick;
                t1.Start();
            }
            else if (progressBarX1.Value == 50)
            {
                _isregistered = ubll.Isregistered();
                progressBarX1.Value++;
            }
            else
            {
                progressBarX1.Value++;
            }
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (label6.Location.Y >= 40)
            {
                y = y - 10;
                y1 = y1 - 35;
                label6.Location = new Point(400, y);
                if (_isregistered)
                {
                    this.Controls["EnterUser"].Location = new Point(347, y1);
                }
                else
                {
                    this.Controls["RegisterForm"].Location = new Point(347, y1);
                }
            }
            else
            {
                t2.Stop();
                rf.panel2.Visible = true;
            }
        }
        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (this.Controls["EnterUser"].Location.Y >= 100)
            {
                y2 = y2 - 35;
                this.Controls["EnterUser"].Location = new Point(347, y2);
            }
            else
            {
                t3.Stop();
                
            }

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
            if (label7.Text == "بازگشت به منو اصلی")
            {
                this.Close();
                
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
