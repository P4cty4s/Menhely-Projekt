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
    /// Interaction logic for KennelControl.xaml
    /// </summary>
    public partial class KennelControl : UserControl
    {
        List<Kutya> myKutyak = new List<Kutya>();
        List<Udvar> Udvarok = new List<Udvar>();
        List<Kennel> Kennelek = new List<Kennel>();
        public KennelControl()
        {
            InitializeComponent();

            betoltes();
        }

        private void betoltes()
        {
            FoAblak.currentContent = "Kennel";
            Udvarok = UdvarDAO.AllUdvar();
            Kennelek = KennelDAO.AllKennel();
            int i = 0;
            foreach (var item in Udvarok)
            {
                Udvarok_cb.Items.Add(item);
            }

            myKutyak = KutyaDAO.getMyKutya();

            foreach (var item in myKutyak)
            {
                Kutyak_panel.Items.Add(item.ID+" - "+item.nev+" - "+item.status);
            }

            

        }

        private void Kutyak_panel_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            addKennelName addKennel = new addKennelName(Udvarok_cb.SelectedItem.ToString());
            addKennel.ShowDialog();
        }
    }
}
