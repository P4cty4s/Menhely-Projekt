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
using Mysqlx.Connection;

namespace Menhely_Projekt.Controls
{
    /// <summary>
    /// UserControl ami a program kezdőlapja bejelentkezés után,
    /// itt van az alap data grid, kereső és a navigációs gombok.
    /// </summary>
    public partial class MainBase : UserControl
    {
        private static List<Kutya> dataShow = new List<Kutya>();
        public MainBase(int id)
        {
            InitializeComponent();
            keresoSetup();
            KutyaDAO.getKutyak();
            feltoltes();
        }

        //Datagrid feltöltése kutyákkal
        public void feltoltes()
        {
            KutyaDataGrid.ItemsSource = null;
            KutyaDAO.getKutyak();
            dataShow = Kutya.kutyak;
            KutyaDataGrid.ItemsSource = dataShow;
        }

        //Kijelentkezés gomb
        private void Logout_btn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            var ez = Window.GetWindow(this);
            ez.Close();
        }

        //Frissítés gomb
        private void Reset_btn_Click(object sender, RoutedEventArgs e)
        {
            feltoltes();
        }

        //Kereséshez tartozó összes metódus
        #region Keresés

        private void enableOptions()
        {
            Options_cb.Items.Clear();
            Options_cb.Visibility = Visibility.Visible;
            Kereso_tb.Visibility = Visibility.Collapsed;
        }

        private void enableKereso()
        {
            Kereso_tb.Visibility = Visibility.Visible;
            Options_cb.Visibility = Visibility.Collapsed;
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
            Kereso_cb.Items.Add("Látható");

            //Nemhasznalatos
            //-Meret
            //-Szuletes
            //-Bekerules
            //-indexkepID
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
                case "regSzam":
                    enableKereso();
                    dataShow = KutyaDAO.searchKutya("regszam", Kereso_tb.Text);
                    break;
                case "nev":
                    enableKereso();
                    dataShow = KutyaDAO.searchKutya("nev", Kereso_tb.Text);
                    break;
                case "chipSzam":
                    enableKereso();
                    dataShow = KutyaDAO.searchKutya("chipszam", Kereso_tb.Text);
                    break;
                //--------
                case "ivar":
                    if (Options_cb.SelectedItem != null)
                    {
                        dataShow = KutyaDAO.searchKutya("ivar", Options_cb.SelectedItem.ToString());
                    }
                    break;
                case "ivaros":
                    if (Options_cb.SelectedItem != null)
                    {
                        dataShow = KutyaDAO.searchKutya("ivaros", Options_cb.SelectedItem.ToString());
                    }
                    break;
                case "telephely":
                    if (Options_cb.SelectedItem != null)
                    {
                        dataShow = KutyaDAO.searchKutya("telephely", Options_cb.SelectedItem.ToString());
                    }
                    break;
                case "foglalt":
                    if (Options_cb.SelectedItem != null)
                    {
                        dataShow = KutyaDAO.searchKutya("foglalt", Options_cb.SelectedItem.ToString() == "Foglalt" ? "1" : "0");
                    }
                    break;
                case "kennel":
                    if (Options_cb.SelectedItem != null)
                    {
                        dataShow = KutyaDAO.searchKutya("kennel", Options_cb.SelectedItem.ToString());
                    }
                    break;
                case "Látható":
                    if (Options_cb.SelectedItem != null)
                    {
                        dataShow = KutyaDAO.searchKutya("visible", Options_cb.SelectedItem.ToString() == "Látható" ? "1" : "0");
                    }
                    break;
            }
            KutyaDataGrid.ItemsSource = dataShow;
        }

        private void Kereso_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            optionsBetoltes();
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

        private void optionsBetoltes()
        {
            enableOptions();
            switch (Kereso_cb.SelectedItem)
            {
                default:
                    break;
                case "ivar":
                    enableOptions();
                    Options_cb.Items.Add("Kan");
                    Options_cb.Items.Add("Szuka");
                    break;
                case "ivaros":
                    enableOptions();
                    Options_cb.Items.Add("Ivaros");
                    Options_cb.Items.Add("Ivartalan");
                    break;
                case "telephely":
                    enableOptions();
                    if (Options_cb.Items.Count == 0)
                    {
                        foreach (var item in Kutya.kutyak.Select(q => q.telephely).Distinct())
                        {
                            Options_cb.Items.Add(item);
                        }
                    }
                    break;
                case "foglalt":
                    enableOptions();
                    Options_cb.Items.Add("Foglalt");
                    Options_cb.Items.Add("Szabad");
                    break;
                case "kennel":
                    enableOptions();
                    foreach (var item in Kutya.kutyak.Select(q => q.kennel).Distinct())
                    {
                        Options_cb.Items.Add(item);
                    }
                    break;
                case "Látható":
                    Options_cb.Items.Add("Látható");
                    Options_cb.Items.Add("Láthatatlan");
                    break;
            }
        }

        #endregion

        //Kutya módosításának megnyitása
        private void ModifyKutya_btn_Click(object sender, RoutedEventArgs e)
        {
            if (KutyaDataGrid.SelectedItem != null)
            {
                Kutya target = KutyaDataGrid.SelectedItem as Kutya;
                ModifyWindow modifyWindow = new ModifyWindow(target.ID);
                modifyWindow.Show();
            }
            else
            {
                MessageBox.Show("Válassz kutyát a módosításhoz.");
            }
        }

        //Datagrid legenerálásának módosítása. (Idő formátum,stb...)
        private void KutyaDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime))
            {
                var column = e.Column as DataGridTextColumn;
                if (column != null)
                {
                    column.Binding = new Binding(e.PropertyName)
                    {
                        StringFormat = "yyyy-MM-dd"
                    };
                }
            }

            if (e.PropertyName == "kepek")
            {
                e.Cancel = true;
            }
        }

        //Új kutya létrehozásának megnyitása
        private void AddKutya_btn_Click(object sender, RoutedEventArgs e)
        {
            newKutya create = new newKutya();
            create.Show();
        }

        //Kennelek kezelésének megnyitása
        private void Kennel_btn_Click(object sender, RoutedEventArgs e)
        {
            var ez = Window.GetWindow(this);
            KennelControl kennelControl = new KennelControl();
            ez.Content = kennelControl;
        }

        //Előzmények megnyitása
        private void History_btn_Click(object sender, RoutedEventArgs e)
        {
            var ez = Window.GetWindow(this);
            ChangelogBase changelog = new ChangelogBase();
            ez.Content = changelog;
        }
    }
}
