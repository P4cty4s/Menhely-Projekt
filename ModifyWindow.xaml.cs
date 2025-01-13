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
    /// Interaction logic for ModifyWindow.xaml
    /// </summary>
    public partial class ModifyWindow : Window
    {
        public ModifyWindow(int ID)
        {
            InitializeComponent();

            betoltes(KutyaDAO.egyKutya(ID));
        }

        private void betoltes(Kutya target)
        {
            id_tb.Text = target.ID.ToString();

            regisztraciosSzam_tb.Text = target.regSzam.ToString();

            nev_tb.Text = target.nev;

            chipSzam_tb.Text = target.chipSzam.ToString();

            ivar_cb.Items.Clear();
            ivar_cb.Items.Add("Kan");
            ivar_cb.Items.Add("Szuka");
            ivar_cb.SelectedItem = target.ivar == true ? "Kan" : "Szuka";

            meret_tb.Text = target.meret.ToString();

            szuletes_dp.Text = target.szuletes.ToString();

            bekerules_dp.Text = target.bekerules.ToString();

            telephely_cb.Items.Clear();
            foreach (var item in Kutya.kutyak.Select(q => q.telephely).Distinct())
            {
                telephely_cb.Items.Add(item.ToString());
            }
            telephely_cb.SelectedItem = target.telephely.ToString();

            profilePicture.Source = new BitmapImage(new Uri("Images/sampleProfile.jpg",UriKind.Relative));

            if (target.foglalt)
            {
                foglalt_rb.IsChecked = true;
            } else
            {
                szabad_rb.IsChecked = true;
            }

            ivaros_cb.Items.Clear();
            ivaros_cb.Items.Add("Ivaros");
            ivaros_cb.Items.Add("Ivartalan");
            if (target.ivaros)
            {
                ivaros_cb.SelectedItem = "Ivaros";
            } else
            {
                ivaros_cb.SelectedItem = "Ivartalan";
            }

            kennel_cb.Items.Clear();
            foreach (var item in Kutya.kutyak.Select(q => q.kennel).Distinct())
            {
                kennel_cb.Items.Add(item.ToString());
            }
            kennel_cb.SelectedItem = target.kennel.ToString();

            indexkepID_tb.Text = target.indexkepID.ToString();

            if (target.visible)
            {
                invisible_rb.IsChecked = true;
            } else
            {
                visible_rb.IsChecked = true;
            }

        }
    }
}
