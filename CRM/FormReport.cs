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
using Stimulsoft.Report;

namespace CRM
{
    public partial class FormReport : Form
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
        public FormReport()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }
        MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
        UserBLL ubll = new UserBLL();
        List<Customer> customers = new List<Customer>();
        List<User> users = new List<User>();
        List<Activity> activities = new List<Activity>();
        List<Product> products = new List<Product>();
        List<Invoice> i = new List<Invoice>();
        ProductBLL pbll = new ProductBLL();
        InvoiceBLL ibll = new InvoiceBLL();
        MsgBox mb = new MsgBox();
        ActivityBLL abll = new ActivityBLL();
        private void FormReport_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (ubll.Access(w.Loadwindow, "بخش گزارشات",2))
            {
                if (RBListOldMonth.Checked)
                {
                    //DialogResult res = mb.MyShowDialog("اطلاعیه ثبت", ibll.Create(i, c, productslist) + " آیا قصد چاپ فاکتور را دارید؟", "", true, false);
                    //if (res == DialogResult.Yes)
                    //{
                        StiReport sti = new StiReport();
                        sti.Load(@"C:\Users\Pixel\source\repos\CRM\InvoiceReportMothe.mrt");
                        sti.Render();
                        sti.Show();
                    //}
                }
                else if (RbInvoicesOldWeek.Checked)
                {
                    StiReport sti = new StiReport();
                    sti.Load(@"C:\Users\Pixel\source\repos\CRM\InvoiceReportWeek.mrt");
                    sti.Render();
                    sti.Show();
                }
                else if (RBListOldYear.Checked)
                {
                    StiReport sti = new StiReport();
                    sti.Load(@"C:\Users\Pixel\source\repos\CRM\Invoice-Report-Year.mrt");
                    sti.Render();
                    sti.Show();
                }
                else if (RBCustomerList.Checked)
                {
                    StiReport sti = new StiReport();
                    sti.Load(@"C:\Users\Pixel\source\repos\CRM\Report-Record-Customer.mrt");
                    sti.Render();
                    sti.Show();
                }
                else if(RBActivitys.Checked)
                {
                    StiReport sti = new StiReport();
                    sti.Load(@"C:\Users\Pixel\source\repos\CRM\Report-Record-Activity.mrt");
                    sti.Render();
                    sti.Show();
                    
                }
            }
            else
            {
               mb.MyShowDialog("محدودیت دسترسی", "شما نمیتوانید گزارشی را چاپ کنید", "", false, true);
            }
            RBListOldMonth.Checked = false;
            RbInvoicesOldWeek.Checked = false;
            RBListOldYear.Checked = false;
            RBCustomerList.Checked = false;
            RBActivitys.Checked = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        int teadad1 = 0;
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            chart1.Series["گزارشات"].Points.Clear();
            if (RBSellsUser.Checked)
            {
               
                foreach (var item in ubll.sellsuser())
                {
                    int teadad = 0;
                    foreach (var q in item.invoices)
                    {
                        if (q.RegInvoice.Date > dateTimeInput2.Value.Date && q.RegInvoice.Date < dateTimeInput1.Value.Date)
                        {
                            teadad++;
                        }
                    }
                    chart1.Series["گزارشات"].Points.AddXY(item.UserName,teadad);
                }
                
            }
            else if (radioButton6.Checked)
            {
                
                
                foreach (var item in ubll.CountCustomer())
                {
                    if (item.RegCustomer.Date > dateTimeInput2.Value.Date && item.RegCustomer.Date < dateTimeInput1.Value.Date)
                    {
                        teadad1++;
                    }
                }
                chart1.Series["گزارشات"].Points.AddXY(w.Loadwindow.UserName,teadad1);
            }
            else if (radioButton7.Checked)
            {
                int teadad2 = 0;
                foreach (var item in ubll.CountActivity())
                {
                    foreach (var i in item.activities)
                    {
                        if (i.RegActivity.Date > dateTimeInput2.Value.Date && i.RegActivity.Date < dateTimeInput1.Value.Date)
                        {
                            teadad2++;
                        }
                    }
                    chart1.Series["گزارشات"].Points.AddXY(item.UserName, teadad2);
                }
                
            }
            else if (radioButton9.Checked)
            {
                //int teadad2 = 0;
                foreach (var item in pbll.CountPS())
                {
                    //foreach (var i in item.activities)
                    //{
                    //    if (i.RegActivity.Date > dateTimeInput2.Value.Date && i.RegActivity.Date < dateTimeInput1.Value.Date)
                    //    {
                    //        teadad2++;
                    //    }
                    //}
                    chart1.Series["گزارشات"].Points.AddXY(item.Name,item.Stock);
                }
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            chart1.Series["گزارشات"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            chart1.Series["گزارشات"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            chart1.Series["گزارشات"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (ubll.Access(w.Loadwindow, "بخش گزارشات",2))
            {
                if (radioButton6.Checked)
                {
                    foreach (var item in ubll.CountCustomer())
                    {
                        if (item.RegCustomer.Date > dateTimeInput2.Value.Date && item.RegCustomer.Date < dateTimeInput1.Value.Date)
                        {
                            customers.Add(item);

                        }
                    }
                    StiReport sti = new StiReport();
                    sti.Load(@"C:\Users\Pixel\source\repos\CRM\ReportCountCustomer.mrt");
                    sti.RegBusinessObject("Customer", customers);
                    sti.Render();
                    sti.Show();
                    customers.Clear();
                }
                else if (radioButton7.Checked)
                {

                    foreach (var item in abll.StockActivity())
                    {
                        if (item.RegActivity.Date > dateTimeInput2.Value.Date && item.RegActivity.Date < dateTimeInput1.Value.Date)
                        {
                            activities.Add(item);
                        }
                    }
                    StiReport sti = new StiReport();
                    sti.Load(@"C:\Users\Pixel\source\repos\CRM\ReportCountActivity.mrt");
                    sti.RegBusinessObject("Activity", activities);
                    sti.Render();
                    sti.Show();
                    activities.Clear();
                }
                else if (RBSellsUser.Checked)
                {

                    foreach (var item in ibll.StockInvoice(w.Loadwindow.id))
                    {
                        if (item.RegInvoice.Date > dateTimeInput2.Value.Date && item.RegInvoice.Date < dateTimeInput1.Value.Date)
                        {
                            i.Add(item);
                        }
                    }
                    StiReport sti = new StiReport();
                    sti.Load(@"C:\Users\Pixel\source\repos\CRM\ReportCountInvoice.mrt");
                    sti.RegBusinessObject("Invoice", i);
                    sti.Render();
                    sti.Show();
                    i.Clear();

                }
                else if (radioButton9.Checked)
                {
                    foreach (var item in pbll.CountPS())
                    {
                        products.Add(item);
                    }
                    StiReport sti = new StiReport();
                    sti.Load(@"C:\Users\Pixel\source\repos\CRM\ReportCountProduct.mrt");
                    sti.RegBusinessObject("Product", products);
                    sti.Render();
                    sti.Show();
                    products.Clear();
                }
                
            }
            else
            {
                mb.MyShowDialog("محدودیت دسترسی", "شما نمیتوانید گزارشی را چاپ کنید", "", false, true);
            }


        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void dateTimeInput2_Click(object sender, EventArgs e)
        {

        }

        private void dateTimeInput1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
