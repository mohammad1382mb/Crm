using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;
using BLL;
using System.Windows;

namespace CRM
{
    public partial class EnterUser : UserControl
    {
        public EnterUser()
        {
            InitializeComponent();
        }
        UserBLL ubll = new UserBLL();
        User u = new User();
        MsgBox mb = new MsgBox();
        
        

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            u = ubll.EnterU(textBoxX1.Text, textBoxX2.Text);
            if (u != null)
            {
                mb.MyShowDialog("ورود", "شما با موفقیت وارد اکانت خودتون شدید", "", false, false);
                MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
                w.Loadwindow = u;
                w.RefreshForm();
                ((LoudingForm)System.Windows.Forms.Application.OpenForms["LoudingForm"]).Close();

            }
            else
            {
                label4.Visible = true;
                pictureBox2.Visible = true;
                textBoxX1.Text = "";
                textBoxX2.Text = "";

            }
        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {
            if (textBoxX1.Text != "")
            {
                label4.Visible = false;
                pictureBox2.Visible = false;
            }
        }

        private void textBoxX2_TextChanged(object sender, EventArgs e)
        {
            if (textBoxX2.Text != "")
            {
                label4.Visible = false;
                pictureBox2.Visible = false;
            }
        }
    }
}
