using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRM
{
    public class MsgBox
    {
        
         
        public DialogResult MyShowDialog(string Title,string FrsInfo,string EngInfo,bool Buttons,bool Type)
        {
            MsgBoxText mbt = new MsgBoxText();
            mbt.label1.Text = Title;
            mbt.label2.Text = FrsInfo;
            mbt.label3.Text = EngInfo;
            if (Buttons == true)
            {
                mbt.buttonX2.Text = "خیر";
            }
            else
            {
                mbt.buttonX1.Visible = false;
            }
            if (Type == true)
            {
                mbt.BackColor = Color.FromArgb(255, 0, 0);
                mbt.pictureBox1.Image = Properties.Resources.icons8_warning_50;
            }
            mbt.ShowDialog();
            return mbt.DialogResult;
        }

        
    }
}
