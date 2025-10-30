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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BE;
using BLL;

namespace CRM
{
    /// <summary>
    /// Interaction logic for TableRemainders.xaml
    /// </summary>
    public partial class TableRemainders : System.Windows.Controls.UserControl
    {
        public TableRemainders()
        {
            InitializeComponent();
        }
        
        MsgBox mb = new MsgBox();
        DashbordBLL dbll = new DashbordBLL();
        MainWindow mw = (MainWindow)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
        

    }
}
