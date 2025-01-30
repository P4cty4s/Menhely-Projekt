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
using Mysqlx.Crud;

namespace Menhely_Projekt.Controls
{
    /// <summary>
    /// Interaction logic for NavControl.xaml
    /// </summary>
    public partial class NavControl : UserControl
    {
        private DispatcherTimer _timer;
        public NavControl()
        {
            InitializeComponent();

            Username_label.Content = UserDAO.getName(MainWindow.ID);

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            _timer.Tick += updateTime;
            _timer.Start();
        }
        private void updateTime(object sender, EventArgs e)
        {
            Time_label.Content = DateTime.Now.ToString("HH:mm");
        }


        private void Back_btn_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
