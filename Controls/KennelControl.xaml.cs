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

        private Point _startPoint;

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

        private void Kutyak_panel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition(null);
        }

        // Detect drag movement
        private void Kutyak_panel_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (Kutyak_panel.SelectedItem == null) return;

                // Check if mouse moved enough to start a drag
                Point mousePos = e.GetPosition(null);
                Vector diff = _startPoint - mousePos;

                if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    // Start DragDrop
                    DragDrop.DoDragDrop(Kutyak_panel, Kutyak_panel.SelectedItem, DragDropEffects.Move);

                }
            }
        }

        // Visual feedback when dragging over allowed drop targets
        private void Kutyak_panel_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(Kutya))) // Adjust type if needed
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void Kutyak_panel_DragLeave(object sender, DragEventArgs e)
        {
            Kutyak_panel.Items.Remove(sender);
        }
    }
}
