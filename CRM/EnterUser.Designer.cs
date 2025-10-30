namespace CRM
{
    partial class EnterUser
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.symbolBox1 = new DevComponents.DotNetBar.Controls.SymbolBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxX1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxX2 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // symbolBox1
            // 
            // 
            // 
            // 
            this.symbolBox1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.symbolBox1.ForeColor = System.Drawing.Color.White;
            this.symbolBox1.Location = new System.Drawing.Point(123, 20);
            this.symbolBox1.Name = "symbolBox1";
            this.symbolBox1.Size = new System.Drawing.Size(144, 150);
            this.symbolBox1.Symbol = "";
            this.symbolBox1.TabIndex = 1;
            this.symbolBox1.Text = "symbolBox1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(77, 173);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(258, 18);
            this.label2.TabIndex = 19;
            this.label2.Text = "نرم افزار با موفقیت فعال سازی شد!";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label1.Location = new System.Drawing.Point(15, 202);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(381, 16);
            this.label1.TabIndex = 33;
            this.label1.Text = "برای ورود به حساب کاربری خود نام کاربری و رمز عبور خود را وارد کنید";
            // 
            // textBoxX1
            // 
            // 
            // 
            // 
            this.textBoxX1.Border.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxX1.Border.Class = "TextBoxBorder";
            this.textBoxX1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.textBoxX1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.textBoxX1.Location = new System.Drawing.Point(39, 248);
            this.textBoxX1.Name = "textBoxX1";
            this.textBoxX1.PreventEnterBeep = true;
            this.textBoxX1.Size = new System.Drawing.Size(321, 34);
            this.textBoxX1.TabIndex = 29;
            this.textBoxX1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxX1.WatermarkText = "نام کاربری خودتون را وارد کنید";
            this.textBoxX1.TextChanged += new System.EventHandler(this.textBoxX1_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(204, 337);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 18);
            this.label6.TabIndex = 32;
            this.label6.Text = "ورود ";
            // 
            // textBoxX2
            // 
            // 
            // 
            // 
            this.textBoxX2.Border.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxX2.Border.Class = "TextBoxBorder";
            this.textBoxX2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.textBoxX2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.textBoxX2.Location = new System.Drawing.Point(39, 289);
            this.textBoxX2.Name = "textBoxX2";
            this.textBoxX2.PreventEnterBeep = true;
            this.textBoxX2.Size = new System.Drawing.Size(321, 34);
            this.textBoxX2.TabIndex = 30;
            this.textBoxX2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxX2.UseSystemPasswordChar = true;
            this.textBoxX2.WatermarkText = "رمز عبور خود را وارد کنید";
            this.textBoxX2.TextChanged += new System.EventHandler(this.textBoxX2_TextChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::CRM.Properties.Resources.icons8_warning_25;
            this.pictureBox2.Location = new System.Drawing.Point(324, 221);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(25, 25);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 35;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(50, 225);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(269, 16);
            this.label4.TabIndex = 36;
            this.label4.Text = "نام کاربری یا رمز عبور خود را اشتباه وارد کرده اید";
            this.label4.Visible = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox3.Image = global::CRM.Properties.Resources.icons8_tick_40;
            this.pictureBox3.Location = new System.Drawing.Point(158, 329);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(40, 40);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 37;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // EnterUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(18)))), ((int)(((byte)(98)))));
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxX1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxX2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.symbolBox1);
            this.ForeColor = System.Drawing.Color.White;
            this.Location = new System.Drawing.Point(211, 47);
            this.Name = "EnterUser";
            this.Size = new System.Drawing.Size(399, 462);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.SymbolBox symbolBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX1;
        private System.Windows.Forms.Label label6;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}
