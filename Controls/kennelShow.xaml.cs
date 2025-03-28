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
    /// <summary>
    /// Interaction logic for kennelShow.xaml
    /// </summary>
    public partial class kennelShow : UserControl
    {
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

        private void betoltes()
        {   
            foreach (var item in alap.Kutyak)
            {
                Kennelek_lb.Items.Add(item);
            }
            
        }

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

        private void Kennelek_lb_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Kutya objekt = Kennelek_lb.SelectedItem as Kutya;
            kutyakPanel.Items.Add((Kutya)objekt);
            alap.Kutyak.Remove(objekt);
            Kennelek_lb.Items.Remove(objekt);
        }
    }
}
