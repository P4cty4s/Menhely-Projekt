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
    /// <summary>
    /// Interaction logic for newKutya.xaml
    /// </summary>
    public partial class newKutya : Window
    {
        private static Kutya target = new Kutya();

        public newKutya()
        {
            InitializeComponent();
            betoltes();
            
        }

        private Kutya buildKutya()
        {
            try
            {
                target.ID = 0;
                target.regSzam = int.Parse(regisztraciosSzam_tb.Text);
                target.nev = nev_tb.Text;
                target.chipSzam = chipSzam_tb.Text;
                target.ivar = ivar_cb.SelectedItem.ToString() == "Kan";
                target.meret = meret_cb.SelectedItem.ToString();
                target.szuletes = DateTime.Parse(szuletes_dp.Text);
                target.bekerules = DateTime.Parse(bekerules_dp.Text);
                target.ivaros = ivaros_cb.SelectedItem.ToString() == "Ivaros";
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

        private void ImgUpload_btn_Click(object sender, RoutedEventArgs e)
        {
            string host = "127.0.0.1";
            string username = "Menhely_Projekt";
            string password = "admin";

            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog.ShowDialog() == true)
            {

                string customName = target.ID.ToString();
                string selectedFilePath = openFileDialog.FileName;
                string fileExtension = System.IO.Path.GetExtension(selectedFilePath);
                string remotePath = $"/uploads/{customName}{fileExtension}";

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
