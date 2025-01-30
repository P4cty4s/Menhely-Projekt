using Menhely_Projekt.Controls;
using Menhely_Projekt.Models;
using Mysqlx.Cursor;
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

namespace Menhely_Projekt
{
    /// <summary>
    /// Interaction logic for FoAblak.xaml
    /// </summary>
    public partial class FoAblak : Window
    {
        public static List<string> statuses = new List<string>();

        private static List<Kutya> dataShow = new List<Kutya>();
        public FoAblak(int _id)
        {
            InitializeComponent();

            statuses.Add("Kórházban");
            statuses.Add("Sérült");
            statuses.Add("Gazdásodott");
            statuses.Add("Eltávozott");
            statuses.Add("Nálunk van");


            MainBase Main = new MainBase(_id);
            this.Content = Main;
            Main.NavBar.Back_btn.IsEnabled = false;
        }
    }
}
