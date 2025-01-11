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
        }
    }
}
