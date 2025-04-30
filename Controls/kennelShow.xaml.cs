using System;
using System.Collections;
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
using Menhely_Projekt.Controls;
using System.Windows.Markup;

namespace Menhely_Projekt.Controls
{
    public partial class kennelShow : UserControl
    {

        /// <summary>
        /// Kennelt megtestesítő objektum, lehet bele kutyákat pakolni (behúzás), kivenni belőle (jobb klikk).
        /// Törölni önmagát tudja, létrehozni a KennelControl tudja.
        /// </summary>

        ListBox kutyakPanel;

        public Kennel alap = new Kennel();
        public kennelShow(Kennel _kennel, ListBox _kutyakPanel)
        {
            InitializeComponent();
            alap = _kennel;
            kutyakPanel= _kutyakPanel;  
            kennelName_lb.Content = "Kennel "+_kennel.KennelSzam;
            Kennelek_lb.AllowDrop = true;
            betoltes();
        }

        //Kenelben lévő kutyák betöltése
        private void betoltes()
        {   
            foreach (var item in alap.Kutyak)
            {
                Kennelek_lb.Items.Add(item);
            }
        }

        //Drop event
        private void Kennelek_lb_Drop(object sender, DragEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            object data = e.Data.GetData(typeof(Kutya));
            if (data != null)
            {
                parent.Items.Add(data);
                alap.Kutyak.Add(data as Kutya);
                kutyakPanel.Items.Remove(data);
            }
        }

        //Kutya kivétele a kennelből
        private void Kennelek_lb_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Kutya objekt = Kennelek_lb.SelectedItem as Kutya;
            kutyakPanel.Items.Add((Kutya)objekt);
            alap.Kutyak.Remove(objekt);
            Kennelek_lb.Items.Remove(objekt);
        }

        //Kennel törlése
        private void DelKennel(object sender, RoutedEventArgs e)
        {
            KennelControl.showKennel.Remove(KennelControl.showKennel.Find(q=>q.alap.Id == this.alap.Id));

            KennelDAO.DelKennel(alap.Id);

            MessageBox.Show("Siker");
        }
    }
}
