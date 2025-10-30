using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CRM
{
    /// <summary>
    /// Interaction logic for Clocktime.xaml
    /// </summary>
    public partial class Clocktime : UserControl
    {
        public Clocktime()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += new EventHandler(Timer_tick);
            timer.Start();
        }

        private void Timer_tick(Object sender , EventArgs e)
        {
            ClockTimer.Text = DateTime.Now.ToString();

        }
    }
}
