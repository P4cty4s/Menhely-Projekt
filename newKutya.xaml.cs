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
using FluentFTP;
using Menhely_Projekt.Models;
using Microsoft.Win32;

namespace Menhely_Projekt
{

    //Új kutya felvétele a redszerbe
    public partial class newKutya : Window
    {

        //Feltöltendő kutya
        private static Kutya target = new Kutya();

        public newKutya()
        {
            InitializeComponent();
            betoltes();
            
        }

        private Kutya buildKutya()
        {

            //Kuya kreálása
            try
            {
                target.ID = KutyaDAO.LatestID()+1;
                target.regSzam = int.Parse(regisztraciosSzam_tb.Text);
                target.nev = nev_tb.Text;
                target.chipSzam = chipSzam_tb.Text;
                target.ivar = ivar_cb.SelectedItem.ToString();
                target.meret = meret_cb.SelectedItem.ToString();
                target.szuletes = DateTime.Parse(szuletes_dp.Text);
                target.bekerules = DateTime.Parse(bekerules_dp.Text);
                target.ivaros = ivaros_cb.SelectedItem.ToString();
                target.telephely = telephely_cb.SelectedItem.ToString();
                target.foglalt = foglalt_rb.IsChecked == true;
                target.kennel = int.Parse(kennel_cb.SelectedItem.ToString());
                target.indexkepID = int.Parse(indexkepID_tb.Text);
                target.visible = visible_rb.IsChecked == true;
                target.status = Status_cb.SelectedItem.ToString();

                return target;
            }
            catch (Exception)
            {
                MessageBox.Show("Minden mezőt ki kell tölteni!");
                return null;
            }

        }

        //Kutya adatainak betöltése
        private void betoltes()
        {
            

            ivar_cb.Items.Clear();
            ivar_cb.Items.Add("kan");
            ivar_cb.Items.Add("szuka");

            telephely_cb.Items.Clear();
            foreach (var item in Kutya.kutyak.Select(q => q.telephely).Distinct())
            {
                telephely_cb.Items.Add(item.ToString());
            }

            ivaros_cb.Items.Clear();
            ivaros_cb.Items.Add("ivaros");
            ivaros_cb.Items.Add("ivartalan");

            meret_cb.Items.Add("kisestű");
            meret_cb.Items.Add("közepes testű");
            meret_cb.Items.Add("nagytestű");
            meret_cb.Items.Add("kölyök");

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

        //Véglegesítés
        private void save_btn_Click(object sender, RoutedEventArgs e)
        {
            Kutya target = buildKutya();

            if (target != null)
            {
                KutyaDAO.createKutya(target);
            }
        }

        //Kép feltöltése
        private void ImgUpload_btn_Click(object sender, RoutedEventArgs e)
        {

            //FTP szerver adatai

            string host = "127.0.0.1";
            string username = "Menhely_Projekt";
            string password = "admin";

            OpenFileDialog openFileDialog = new OpenFileDialog();

            //Elfogadott fileok
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            //Fájl feltöltése
            if (openFileDialog.ShowDialog() == true)
            {

                string customName = target.ID.ToString();
                string selectedFilePath = openFileDialog.FileName;
                string fileExtension = System.IO.Path.GetExtension(selectedFilePath);
                string remotePath = $"/temp/{customName}-{selectedFilePath}";

                try
                {
                    using (FtpClient client = new FtpClient(host, username, password))
                    {
                        client.Connect();
                        client.UploadFile(selectedFilePath, remotePath, FtpRemoteExists.Overwrite);
                        MessageBox.Show("Siker " + remotePath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Valami hiba történt: "+ex);
                }

                
            }
            else
            {
                Console.WriteLine("Nincs fájl kiválasztva");
            }
        }
    }
}
