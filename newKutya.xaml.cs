using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            
        }

        private Kutya buildKutya()
        {
            Kutya result = new Kutya();

            try
            {
                result.ID = 0;
                result.regSzam = int.Parse(regisztraciosSzam_tb.Text);
                result.nev = nev_tb.Text;
                result.chipSzam = chipSzam_tb.Text;
                result.ivar = ivar_cb.SelectedItem.ToString() == "Kan";
                result.meret = meret_cb.SelectedItem.ToString();
                result.szuletes = DateTime.Parse(szuletes_dp.Text);
                result.bekerules = DateTime.Parse(bekerules_dp.Text);
                result.ivaros = ivaros_cb.SelectedItem.ToString() == "Ivaros";
                result.telephely = telephely_cb.SelectedItem.ToString();
                result.foglalt = foglalt_rb.IsChecked == true;
                result.kennel = int.Parse(kennel_cb.SelectedItem.ToString());
                result.indexkepID = int.Parse(indexkepID_tb.Text);
                result.visible = visible_rb.IsChecked == true;
                result.status = Status_cb.SelectedItem.ToString();
                return result;
            }
            catch (Exception)
            {
                MessageBox.Show("Minden mezőt ki kell tölteni!");
                return null;
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

            meret_cb.Items.Add("Kicsi");
            meret_cb.Items.Add("Közepes");
            meret_cb.Items.Add("Nagy");
            meret_cb.Items.Add("Kölyök");

            kennel_cb.Items.Clear();
            foreach (var item in Kutya.kutyak.Select(q => q.kennel).Distinct())
            {
                kennel_cb.Items.Add(item.ToString());
            }

            foreach (var item in FoAblak.statuses)
            {
                Status_cb.Items.Add(item);
            }

        }

        private void save_btn_Click(object sender, RoutedEventArgs e)
        {
            Kutya target = buildKutya();

            if (target != null)
            {
            KutyaDAO.createKutya(target);
               
            }
        }
    }
}
