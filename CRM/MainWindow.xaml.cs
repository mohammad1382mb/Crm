using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BE;
using BLL;

namespace CRM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }
        public User Loadwindow = new User();
        UserBLL ubll = new UserBLL();
        DashbordBLL dbll = new DashbordBLL();
        
        public void OpenwinForm(Form f)
        {
            Window w = (Window)this.FindName("Main");
            BlurBitmapEffect blur = new BlurBitmapEffect();
            blur.Radius = 10;
            w.BitmapEffect = blur;
            f.ShowDialog();
            blur.Radius = 0;
            w.BitmapEffect = blur;
            
        }
        MsgBox mb = new MsgBox();
        public void RefreshForm()
        {
            
            UserNameTxt.Content = Loadwindow.UserName;
            NameTxt.Content = Loadwindow.Name;
            CountRemindersTxt.Content = dbll.CountReminders(Loadwindow);
            CountCustomerTxt.Content = dbll.CountCustomer();
            CountSellsTxt.Content = dbll.CountSells();
            int a = 0;
            List<TableRemainders> userControlsToRemove = new List<TableRemainders>();
            foreach (var item in MainGride.Children)
            {
                if (item is TableRemainders Tr)
                {
                    // افزودن UserControlهایی که CheckReminderGreen دارند به لیست حذف
                    if (Tr.CheckReminderGreen != null)
                    {
                        userControlsToRemove.Add(Tr);
                    }
                }
            }
            foreach (var userControl in userControlsToRemove)
            {
                MainGride.Children.Remove(userControl);

            } 

            foreach (var item in dbll.GridReminders(Loadwindow))
            {
                if (a < 6)
                {
                    TableRemainders tr = new TableRemainders();
                    if (item.ReminderDate.Date == DateTime.Today)
                    {
                        if (item.IsDone == false)
                        {
                            if (item.DeleteStatus == false)
                            {
                                tr.TitleTxt.Content = item.Title;
                                tr.InfoTxt.Content = item.IonfoReminder;
                                Grid.SetRow(tr, 5 + a);
                                Grid.SetColumnSpan(tr, 8);
                                a++;
                            }
                            
                        }
                    }
                    
                    tr.CheckReminderWhite.MouseLeftButtonUp += (sender, e) =>
                    {
                        item.IsDone = true;
                        dbll.UpdateIsDoneInDatabase(item);
                        tr.CheckReminderWhite.Source = new BitmapImage(new Uri("/Image/icons8-tick-50 (2).png", UriKind.Relative));

                    };
                    MainGride.Children.Add(tr);
                    
                }
               
            }
            
        }





        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if (ubll.Access(Loadwindow, "بخش کالا", 1))
            {
                ProductForm p = new ProductForm();
                OpenwinForm(p);
                RefreshForm();
            }
            else
            {
                mb.MyShowDialog("محدودیت دسترسی", "شما نمیتوانید وارد بخش کالا بشوید", "", true, true);
            }
            
        }
        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ubll.Access(Loadwindow, "بخش مشتریان", 1))
            {
                CustomerForm c = new CustomerForm();
                OpenwinForm(c);
                RefreshForm();
            }
            else
            {
                mb.MyShowDialog("محدودیت دسترسی","شما نمیتوانید وارد بخش مشتریان بشوید","",true,true);
            }
            
        }
        private void TextBlock_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            if (ubll.Access(Loadwindow, "بخش فاکتور", 1))
            {
                InvoiceForm i = new InvoiceForm();
                OpenwinForm(i);
                RefreshForm();
            }
            else
            {
                mb.MyShowDialog("محدودیت دسترسی", "شما نمیتوانید وارد بخش فاکتور بشوید", "", true, true);
            }
            
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LoudingForm lf = new LoudingForm();
            lf.label5.Text = "در حال بارگذاری دوباره اطلاعات... ";
            Loadwindow = null;
            OpenwinForm(lf);
            RefreshForm();
        }

        private void TextBlock_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            if (ubll.Access(Loadwindow, "بخش فعالیت ها", 1))
            {
                ActivityForm a = new ActivityForm();
                OpenwinForm(a);
                RefreshForm();
            }
            else
            {
                mb.MyShowDialog("محدودیت دسترسی", "شما نمیتوانید وارد بخش فعالیت ها بشوید", "", true, true);
            }
            
        }

        private void TextBlock_MouseLeftButtonDown_4(object sender, MouseButtonEventArgs e)
        {
            if (ubll.Access(Loadwindow, "بخش یاد آور ها", 1))
            {
                Reminder r = new Reminder();
                OpenwinForm(r);
                RefreshForm();
                
            }
            else
            {
                mb.MyShowDialog("محدودیت دسترسی", "شما نمیتوانید وارد بخش یادآور ها بشوید", "", true, true);
            }
            
        }

        private void TextBlock_MouseLeftButtonDown_5(object sender, MouseButtonEventArgs e)
        {
            if (ubll.Access(Loadwindow, "پنل پیامکی", 1))
            {
                SmsPanel s = new SmsPanel();
                OpenwinForm(s);
                RefreshForm();
            }
            else
            {
                mb.MyShowDialog("محدودیت دسترسی", "شما نمیتوانید وارد پنل پیامکی بشوید", "", true, true);
            }
            
        }

        

        private void TextBlock_MouseLeftButtonDown_6(object sender, MouseButtonEventArgs e)
        {
            if (ubll.Access(Loadwindow, "بخش کاربران", 1))
            {
                UserForm u = new UserForm();
                OpenwinForm(u);
                RefreshForm();
            }
            else
            {
                mb.MyShowDialog("محدودیت دسترسی", "شما نمیتوانید وارد بخش کاربران بشوید", "", true, true);
            }
            
        }

        

        private void TextBlock_MouseLeftButtonDown_7(object sender, MouseButtonEventArgs e)
        {
            if (ubll.Access(Loadwindow, "بخش تنظیمات", 1))
            {
                settingapp sa = new settingapp();
                OpenwinForm(sa);
                RefreshForm();
            }
            else
            {
                mb.MyShowDialog("محدودیت دسترسی", "شما نمیتوانید وارد بخش تنظیمات بشوید", "", true, true);
            }
            
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            

        }

        private void Main_Loaded(object sender, RoutedEventArgs e)
        {
           
            LoudingForm lf = new LoudingForm();
            OpenwinForm(lf);
            
        }

        private void Clocktime_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void TextBlock_MouseLeftButtonDown_8(object sender, MouseButtonEventArgs e)
        {
            if (ubll.Access(Loadwindow, "بخش گزارشات",1))
            {
                FormReport fr = new FormReport();
                OpenwinForm(fr);
            }
            else
            {
                mb.MyShowDialog("محدودیت دسترسی", "شما نمیتوانید وارد بخش گزارشات بشوید", "", true, true);
            }
        }

        private void Image_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Image_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            // لیستی برای نگه‌داری UserControlها
            List<TableRemainders> userControlsToRemove = new List<TableRemainders>();
            TableRemainders ta = new TableRemainders();
            foreach (var item in MainGride.Children)
            {
                
                if (item is TableRemainders Tr)
                {
                    // افزودن UserControlهایی که CheckReminderGreen دارند به لیست حذف
                    if (Tr.CheckReminderGreen != null)
                    {
                        userControlsToRemove.Add(Tr);
                    }
                }
            }

            // حذف UserControlهای موجود در لیست حذف از MainGride
            foreach (var userControl in userControlsToRemove)
            {
                MainGride.Children.Remove(userControl);
            }
            RefreshForm();
            
            
            

        }
    }
}
