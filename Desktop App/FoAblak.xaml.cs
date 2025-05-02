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
    /// A program fő ablaka, ezen belül lehet több féle User Interfacet megjeleníteni.
    /// </summary>
    public partial class FoAblak : Window
    {
        //Felhasználó ID-ja
        public static int UserId = -1;

        //Felhasznalo nev
        public static string UserName = UserDAO.getName(MainWindow.ID);

        //Státuszok
        public static List<string> statuses = new List<string>();

        //Az ablakban megjelenített Content
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

        //Kennel User Interface létrehozása
        public static KennelControl Kennel = new KennelControl();

        //Main User Interface létrehozása
        public static MainBase Main = new MainBase(UserId);

        //Main User Interface betöltése
        private void mainBetolt()
        {
            this.Content = Main;
            currentContent = "Main";
        }

        private void TurnOff(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }

}
