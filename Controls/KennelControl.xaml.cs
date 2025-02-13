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
using Mysqlx;
using Org.BouncyCastle.Asn1.Cmp;

namespace Menhely_Projekt.Controls
{
    /// <summary>
    /// Interaction logic for KennelControl.xaml
    /// </summary>
    public partial class KennelControl : UserControl
    {
        List<Kutya> myKutyak = new List<Kutya>();
        List<Udvar> Udvarok = new List<Udvar>();
        List<Kennel> Kennelek = new List<Kennel>();

        Dictionary<Kennel,kennelShow> showKennel = new Dictionary<Kennel,kennelShow>();
        public KennelControl()
        {
            InitializeComponent();
            
            betoltes();
        }
        private void betoltes()
        {
            FoAblak.currentContent = "Kennel";
            kennelBetoltes();
            udvarBetoltes();
            kutyaBetoltes();
        }

        private void kutyaBetoltes()
        {
            myKutyak = KutyaDAO.getMyKutya();

            foreach (var item in myKutyak)
            {
                Kutyak_panel.Items.Add(item);
            }
        }

        private void kennelBetoltes()
        {
            Kennelek = KennelDAO.AllKennel();

            showKennel.Clear();

            if(Udvarok_cb.SelectedItem != null)
            {
                Kennel_panel.Children.Clear();

                Udvar _udvar = Udvarok_cb.SelectedItem as Udvar;

                foreach (var item in Kennelek.Where(q=>q.UdvarId == _udvar.Id))
                {
                    showKennel.Add(item,new kennelShow(item.KennelSzam.ToString()));
                    Kennel_panel.Children.Add(showKennel[item]);
                
                }
            }

        }

        private void udvarBetoltes()
        {
            Udvarok = UdvarDAO.AllUdvar();
            foreach (Udvar item in Udvarok)
            {
                Udvarok_cb.Items.Add(item);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if(Udvarok_cb.SelectedItem != null)
            {
                Udvar adott = Udvarok_cb.SelectedItem as Udvar;

                int szam = 0;
                while (Kennelek.Where(q=>q.UdvarId == adott.Id).Any(item=>item.KennelSzam == szam))
                {
                    szam++;
                }

                addKennelName addKennel = new addKennelName(adott,szam);

                addKennel.ShowDialog();

                if(addKennel.DialogResult == true)
                {
                    kennelBetoltes();
                }
            } else
            {
                MessageBox.Show("Válasszon udvart a kennelhez.");
            }
        }

        private void Udvarok_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            kennelBetoltes();
        }
    }
}
