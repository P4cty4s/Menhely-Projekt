using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FluentFTP;
using Menhely_Projekt.Models;
using Microsoft.Win32;
using Org.BouncyCastle.Asn1.X509;

namespace Menhely_Projekt
{
    /// <summary>
    /// Interaction logic for ModifyWindow.xaml
    /// </summary>
    public partial class ModifyWindow : Window
    {
        Kutya alany = new Kutya();
        public ModifyWindow(int ID)
        {
            InitializeComponent();

            alany = KutyaDAO.egyKutya(ID);

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
            ivar_cb.SelectedItem = target.ivar;

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
            ivaros_cb.Items.Add("ivaros");
            ivaros_cb.Items.Add("ivartalan");
            if (target.ivaros == "ivaros")
            {
                ivaros_cb.SelectedItem = "ivaros";
            } else
            {
                ivaros_cb.SelectedItem = "ivartalan";
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

            foreach (var item in FoAblak.statuses)
            {
                Status_cb.Items.Add(item);
            }

            Status_cb.SelectedItem = target.status;

        }

        private void save_btn_Click(object sender, RoutedEventArgs e)
        {
            
            alany.ID = int.Parse(id_tb.Text);

            alany.regSzam = int.Parse(regisztraciosSzam_tb.Text);

            alany.nev = nev_tb.Text;

            alany.chipSzam = chipSzam_tb.Text;

            alany.ivar = ivar_cb.SelectedItem.ToString();

            alany.meret = meret_tb.Text;

            alany.szuletes = DateTime.Parse(szuletes_dp.Text);

            alany.bekerules = DateTime.Parse(bekerules_dp.Text);

            alany.ivaros = ivaros_cb.SelectedItem.ToString();

            alany.telephely = telephely_cb.Text;

            alany.foglalt = foglalt_rb.IsChecked == true;

            alany.kennel = int.Parse(kennel_cb.Text);

            alany.indexkepID = int.Parse(indexkepID_tb.Text);

            alany.visible = visible_rb.IsChecked == true;

            alany.status = Status_cb.SelectedItem.ToString();

            if(alany.status == null || alany.status == "")
            {
                alany.status = "Nálunk van";
            }

            KutyaDAO.updateKutya(alany);
        }

        private void StreamImageFromFTP(string remoteFilePath)
        {
            string host = "127.0.0.1"; // FTP host
            string username = "Menhely_Projekt"; // FTP username
            string password = "admin"; // FTP password

            try
            {
                using (FtpClient client = new FtpClient(host, username, password))
                {
                    client.Connect();

                    // Create a temporary file path
                    string tempFilePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString() + ".tmp");

                    // Download the file into the temporary file
                    client.DownloadFile(tempFilePath, remoteFilePath);

                    // Load the temporary file into a MemoryStream
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        byte[] fileBytes = File.ReadAllBytes(tempFilePath);
                        memoryStream.Write(fileBytes, 0, fileBytes.Length);

                        // Reset the memory stream position to the start
                        memoryStream.Position = 0;

                        // Create a BitmapImage from the memory stream
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.StreamSource = memoryStream;
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.EndInit();

                        // Display the image in the WPF Image control
                        profilePicture.Source = bitmap;

                        // Optionally, delete the temporary file after usage
                        File.Delete(tempFilePath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading image: " + ex.Message);
            }
        }

        private void ImgUpload_btn_Click(object sender, RoutedEventArgs e)
        {
            string host = "127.0.0.1";
            string username = "Menhely_Projekt";
            string password = "admin";

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string customName = alany.ID.ToString(); // Ensure this is a valid string
                string selectedFilePath = openFileDialog.FileName;
                string fileName = System.IO.Path.GetFileNameWithoutExtension(selectedFilePath);
                string fileExtension = System.IO.Path.GetExtension(selectedFilePath);
                string remotePath = $"/uploads/{customName}-{fileName}{fileExtension}";

                try
                {
                    using (FtpClient client = new FtpClient(host, username, password))
                    {
                        client.Connect();

                        client.UploadFile(selectedFilePath, remotePath, FtpRemoteExists.Overwrite);
                        MessageBox.Show("Siker: " + remotePath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Valami hiba történt: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Nincs fájl kiválasztva");
            }
        }

    }
}
