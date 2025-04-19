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
using Menhely_Projekt.Controls;
using MySqlX.XDevAPI.Common;

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

        public static ListBox dragSource = null;

        public static List<kennelShow> showKennel = new List<kennelShow>();
        public KennelControl()
        {
            InitializeComponent();
            showKennel.Clear();
            betoltes();
        }

        private void ujratoltes()
        {
            Kennelek.Clear();
            showKennel.Clear();

            Kennelek = KennelDAO.AllKennel();

            kennelBetoltes();
            kennelMegjelenit();
        }
        private void betoltes()
        {
            FoAblak.currentContent = "Kennel";
            Kennelek = KennelDAO.AllKennel();
            kennelBetoltes();
            kutyaBetoltes();
            udvarBetoltes();
        }

        //Betölti a nálunk lévő kutyákat (Sérült,Nálunk van)
        private void kutyaBetoltes()
        {
            myKutyak = KutyaDAO.getMyKutya();

            Kutyak_panel.Items.Clear();

            foreach (var ikutya in myKutyak)
            {
                if (valogato(ikutya))
                {
                    Kutyak_panel.Items.Add(ikutya);
                }
            }
        }

        //Megnézi hogy melyik kennelben van benne a kutya
        private bool valogato(Kutya ikutya)
        {
            foreach (Kennel ikennel in Kennelek)
            {
                if (ikennel.Kutyak.Any(q=>q.ID == ikutya.ID))
                {
                    return false;
                }
            }

            return true;
        }


        private void kennelBetoltes()
        {   

                Udvar _udvar = Udvarok_cb.SelectedItem as Udvar;

                foreach (Kennel item in Kennelek)
                {
                    showKennel.Add(new kennelShow(item,Kutyak_panel));
                }

                kennelMegjelenit();
        }

        //Újra tölti a lekért kenneleket
        private void kennelMegjelenit()
        {
            Kennel_panel.Children.Clear();

            if (Udvarok_cb.SelectedItem != null)
            {
                Udvar _udvar = Udvarok_cb.SelectedItem as Udvar;

                foreach (kennelShow item in showKennel.Where(q=>q.alap.UdvarId == _udvar.Id))
                {
                    Kennel_panel.Children.Add(item);
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
                    ujratoltes();
                }
            } else
            {
                MessageBox.Show("Válasszon udvart a kennelhez.");
            }
        }

        private void Udvarok_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Udvarok_cb.SelectedItem != null)
            {
                kennelMegjelenit();
            }
        }

        private void Kutyak_panel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            dragSource = parent;
            object data = GetDataFromListBox(dragSource, e.GetPosition(parent));

            if (data != null)
            {
                DragDrop.DoDragDrop(parent, data, DragDropEffects.Move);
            }
        }
        #region GetDataFromListBox(ListBox,Point)
        private static object GetDataFromListBox(ListBox source, Point point)
        {
            UIElement element = source.InputHitTest(point) as UIElement;
            if (element != null)
            {
                object data = DependencyProperty.UnsetValue;
                while (data == DependencyProperty.UnsetValue)
                {
                    data = source.ItemContainerGenerator.ItemFromContainer(element);

                    if (data == DependencyProperty.UnsetValue)
                    {
                        element = VisualTreeHelper.GetParent(element) as UIElement;
                    }

                    if (element == source)
                    {
                        return null;
                    }
                }

                if (data != DependencyProperty.UnsetValue)
                {
                    return data;
                }
            }

            return null;
        }


        #endregion

        private void Save_btn_Click(object sender, RoutedEventArgs e)
        {
            List<Kennel> result = new List<Kennel>();

            Udvar _udvar = Udvarok_cb.SelectedItem as Udvar;

            foreach (var item in showKennel.Where(q => q.alap.UdvarId == _udvar.Id))
            {
                result.Add(item.alap);
            }

            KennelDAO.SetKennel(result);
        }

        //Az összes kennel kiürítése, kutyák újra gyüjtése
        private async void Reset_All(object sender, RoutedEventArgs e)
        {
            List<Kennel> target = new List<Kennel>();

            foreach (var item in showKennel)
            {
                target.Add(new Kennel(item.alap.Id,item.alap.UdvarId,item.alap.KennelSzam));
            }

            await KennelDAO.SetKennel(target);

            ujratoltes();

            kutyaBetoltes();
        }
    }
}
