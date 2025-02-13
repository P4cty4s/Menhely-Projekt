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
using Menhely_Projekt.Models;

namespace Menhely_Projekt.Controls
{
    /// <summary>
    /// Interaction logic for kennelShow.xaml
    /// </summary>
    public partial class kennelShow : UserControl
    {
        public event EventHandler requestKutya;
        public kennelShow(string name)
        {
            InitializeComponent();
            kennelName_lb.Content = "Kennel "+name;
            Kennelek_lb.AllowDrop = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            requestKutya?.Invoke(this, EventArgs.Empty);
        }

    }
}
