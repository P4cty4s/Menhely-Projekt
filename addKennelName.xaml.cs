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
using System.Windows.Shapes;
using Menhely_Projekt.Models;
using Mysqlx;

namespace Menhely_Projekt
{
    /// <summary>
    /// Interaction logic for addKennelName.xaml
    /// </summary>
    public partial class addKennelName : Window
    {
        Kennel kennel = new Kennel();
        public addKennelName(Udvar _udvar,int _kennelSzam)
        {
            InitializeComponent();
            kennel.UdvarId = _udvar.Id;
            kennel.Id = 0;
            kennel.KennelSzam = _kennelSzam;

            kennelName_tb.Text = "kennel "+_kennelSzam;
            kennelName_tb.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            KennelDAO.CreateKennel(kennel);
            this.DialogResult = true;
            this.Close();
        }
    }
}
