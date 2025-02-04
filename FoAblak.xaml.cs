using Menhely_Projekt.Controls;
using Menhely_Projekt.Models;
using Mysqlx.Cursor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        private static int UserId = -1;
        public static List<string> statuses = new List<string>();

        public static string currentContent = "";

        public FoAblak(int _id)
        {
            InitializeComponent();

            UserId = _id;

            statuses.Add("Kórházban");
            statuses.Add("Sérült");
            statuses.Add("Gazdásodott");
            statuses.Add("Eltávozott");
            statuses.Add("Nálunk van");

            mainBetolt();
        }

        public static KennelControl Kennel = new KennelControl();
        public static MainBase Main = new MainBase(UserId);

        private void mainBetolt()
        {
            this.Content = Main;
            currentContent = "Main";
        }

    }

}
