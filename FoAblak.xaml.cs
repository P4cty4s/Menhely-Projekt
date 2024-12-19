using Menhely_Projekt.Controls;
using Menhely_Projekt.Models;
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

namespace Menhely_Projekt
{
    /// <summary>
    /// Interaction logic for FoAblak.xaml
    /// </summary>
    public partial class FoAblak : Window
    {
        private NavControl navControl = new NavControl();
        private static List<Kutya> dataShow = new List<Kutya>();
        public FoAblak(int id)
        {
            InitializeComponent();
            keresoSetup();
            KutyaDAO.getKutyak();
            feltoltes();
        }

        private void keresoSetup()
        {
            //Egykutya
            Kereso_cb.Items.Add("ID");
            Kereso_cb.Items.Add("regSzam");
            Kereso_cb.Items.Add("nev");
            Kereso_cb.Items.Add("chipSzam");

            //Szures
            Kereso_cb.Items.Add("ivar");
            Kereso_cb.Items.Add("ivaros");
            Kereso_cb.Items.Add("telephely");
            Kereso_cb.Items.Add("foglalt");
            Kereso_cb.Items.Add("kennel");
            Kereso_cb.Items.Add("visible");

            //Nemhasznalatos
            //-Meret
            //-Szuletes
            //-Bekerules
            //-indexkepID
        }

        private void enableKereso()
        {
            Kereso_tb.Visibility = Visibility.Visible;
            Options_cb.Visibility = Visibility.Collapsed;
        }

        private void enableOptions()
        {
            Options_cb.Visibility= Visibility.Visible;
            Kereso_tb.Visibility= Visibility.Collapsed;
        }

        private void feltoltes()
        {
            KutyaDataGrid.ItemsSource = null;
            KutyaDAO.getKutyak();
            dataShow = Kutya.kutyak;
            KutyaDataGrid.ItemsSource = dataShow;
        }

        private void Logout_btn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Reset_btn_Click(object sender, RoutedEventArgs e)
        {
            KutyaDataGrid.ItemsSource = dataShow;
        }

        private void kereses()
        {
            switch (Kereso_cb.SelectedItem)
            {
                default:
                    break;
                case "ID":
                    enableKereso();
                    dataShow = KutyaDAO.searchKutya("ID", Kereso_tb.Text);
                    break;
                case "regszam":
                    enableKereso();
                    dataShow = KutyaDAO.searchKutya("reg", Kereso_tb.Text);
                    break;
                case "nev":
                    enableKereso();
                    dataShow = KutyaDAO.searchKutya("nev", Kereso_tb.Text);
                    break;
                case "chipszam":
                    enableKereso();
                    dataShow = KutyaDAO.searchKutya("chipszam", Kereso_tb.Text);
                    break;
                //--------
                case "ivar":
                    enableOptions();
                    Options_cb.Items.Clear();
                    Options_cb.Items.Add("Kan");
                    Options_cb.Items.Add("Szuka");
                    dataShow = KutyaDAO.searchKutya("ivar", Options_cb.SelectedItem == "Kan" ? "1" : "0");
                    break;
                case "ivaros":
                    enableOptions();
                    Options_cb.Items.Clear();
                    Options_cb.Items.Add("Ivaros");
                    Options_cb.Items.Add("Ivartalan");
                    dataShow = KutyaDAO.searchKutya("ivaros", Options_cb.SelectedItem == "ivaros" ? "1" : "0");
                    break;
                case "telephely":
                    enableOptions();
                    Options_cb.Items.Clear();
                    if (Options_cb.Items.Count == 0)
                    {
                        foreach (var item in Kutya.kutyak.Select(q=>q.telephely).Distinct())
                        {
                            Options_cb.Items.Add(item);
                        }
                    }
                    if (Options_cb.SelectedItem != null)
                    {
                    dataShow = KutyaDAO.searchKutya("telephely", Options_cb.SelectedItem.ToString());
                    }
                    break;
            }
            KutyaDataGrid.ItemsSource = dataShow;
        }

        private void Kereso_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            kereses();
        }

        private void Kereso_tb_KeyUp(object sender, KeyEventArgs e)
        {
            kereses();
        }

        private void Options_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            kereses();
        }
    }
}
