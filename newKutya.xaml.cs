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

namespace Menhely_Projekt
{
    /// <summary>
    /// Interaction logic for newKutya.xaml
    /// </summary>
    public partial class newKutya : Window
    {
        public newKutya()
        {
            InitializeComponent();
            betoltes();
            buildKutya();
        }

        private Kutya buildKutya()
        {
            Kutya result = new Kutya();

            

            return result;
        }

        private object checker(object obj)
        {
            if (obj != null)
            {
                return obj;

            } else 
            {
                return MessageBox.Show("Nem lehet egy mező sem üres.");
            
            }

        }

        private void betoltes()
        {

            ivar_cb.Items.Clear();
            ivar_cb.Items.Add("Kan");
            ivar_cb.Items.Add("Szuka");

            telephely_cb.Items.Clear();
            foreach (var item in Kutya.kutyak.Select(q => q.telephely).Distinct())
            {
                telephely_cb.Items.Add(item.ToString());
            }

            ivaros_cb.Items.Clear();
            ivaros_cb.Items.Add("Ivaros");
            ivaros_cb.Items.Add("Ivartalan");

            kennel_cb.Items.Clear();
            foreach (var item in Kutya.kutyak.Select(q => q.kennel).Distinct())
            {
                kennel_cb.Items.Add(item.ToString());
            }

        }

        private void save_btn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
