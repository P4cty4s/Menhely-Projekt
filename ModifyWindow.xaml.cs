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
using Menhely_Projekt.Controls;
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
        public event Action OnClose;
        Kutya alany = new Kutya();
        private static int currentPic = 0;
        public ModifyWindow(int ID)
        {
            InitializeComponent();

            

            alany = KutyaDAO.egyKutya(ID);

            if (alany.kepek == null)
            {
                alany.kepek = new List<KutyaKep>();
            }

            betoltes(KutyaDAO.egyKutya(ID));

            reloadImages(0);
        }

        private void reloadImages(int Szam)
        {
            if (alany.kepek.Count > 0)
            {
                profilePicture.Source = alany.kepek[Szam].Kep;
            }
            else
            {
                profilePicture.Source = new BitmapImage();
            }
        }

        private void betoltes(Kutya target)
        {
            id_tb.Text = target.ID.ToString();

            regisztraciosSzam_tb.Text = target.regSzam.ToString();

            nev_tb.Text = target.nev;

            chipSzam_tb.Text = target.chipSzam.ToString();

            ivar_cb.Items.Clear();
            ivar_cb.Items.Add("kan");
            ivar_cb.Items.Add("szuka");
            ivar_cb.SelectedItem = target.ivar;

            meret_cb.Items.Add("kisestű");
            meret_cb.Items.Add("közepes testű");
            meret_cb.Items.Add("nagytestű");
            meret_cb.Items.Add("kölyök");

            meret_cb.SelectedItem = target.meret.ToString();

            szuletes_dp.Text = target.szuletes.ToString();

            bekerules_dp.Text = target.bekerules.ToString();

            telephely_cb.Items.Clear();
            foreach (var item in Kutya.kutyak.Select(q => q.telephely).Distinct())
            {
                telephely_cb.Items.Add(item.ToString());
            }
            telephely_cb.SelectedItem = target.telephely.ToString();

            profilePicture.Source = new BitmapImage();

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

            foreach (var item in alany.kepek)
            {
                IndexKep_cb.Items.Add(item.Info.nev);
            }

            indexkepBetoltese();
        }

        private void indexkepBetoltese()
        {

            if (alany.kepek.Count() > 0)
            {
                try
                {
                    var x = alany.kepek.Find(q => q.Info.ID == alany.indexkepID);
                    IndexKep_cb.SelectedItem = x!=null?x.Info.nev:null;
                }
                catch (Exception)
                {
                    IndexKep_cb.SelectedIndex = -1;
                }
            } else
            {
                IndexKep_cb.SelectedIndex = -1;
            }

            
        }

        private void save_btn_Click(object sender, RoutedEventArgs e)
        {
            
            alany.ID = int.Parse(id_tb.Text);

            alany.regSzam = int.Parse(regisztraciosSzam_tb.Text);

            alany.nev = nev_tb.Text;

            alany.chipSzam = chipSzam_tb.Text;

            alany.ivar = ivar_cb.SelectedItem.ToString();

            alany.meret = meret_cb.SelectedItem.ToString();

            alany.szuletes = DateTime.Parse(szuletes_dp.Text);

            alany.bekerules = DateTime.Parse(bekerules_dp.Text);

            alany.ivaros = ivaros_cb.SelectedItem.ToString();

            alany.telephely = telephely_cb.Text;

            alany.foglalt = foglalt_rb.IsChecked == true;

            alany.kennel = int.Parse(kennel_cb.Text);

            alany.visible = visible_rb.IsChecked == true;

            alany.status = Status_cb.SelectedItem.ToString();

            if (IndexKep_cb.SelectedItem != null)
            {
                alany.indexkepID = alany.kepek.Find(q=>q.Info.nev == IndexKep_cb.SelectedItem.ToString()).Info.ID;
            } else
            {
                alany.indexkepID = 0;
            }


            if(alany.status == null || alany.status == "")
            {
                alany.status = "Nálunk van";
            }

            KutyaDAO.updateKutya(alany);
            OnClose?.Invoke();
            this.Close();
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

                string customName = alany.ID.ToString();
                string selectedFilePath = openFileDialog.FileName;
                string fileName = System.IO.Path.GetFileName(selectedFilePath);
                string fileExtension = System.IO.Path.GetExtension(selectedFilePath);
                string remotePath = $"/uploads/{customName}-{fileName}";

                try
                {
                    using (FtpClient client = new FtpClient(host, username, password))
                    {
                        client.Connect();
                        client.UploadFile(selectedFilePath, remotePath, FtpRemoteExists.Overwrite);
                        MessageBox.Show("Siker " + remotePath);
                        KepInfo _kepInfo = KutyaDAO.SetKutyaImages(alany.ID, $"{customName}-{fileName}");

                        alany.kepek.Add(new KutyaKep(_kepInfo, KutyaDAO.GetModelImage(_kepInfo.nev)));
                        currentPic = alany.kepek.Count - 1;
                        reloadImages(currentPic);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Valami hiba történt: " + ex);
                }
            }
            else
            {
                Console.WriteLine("Nincs fájl kiválasztva");
            }
        }

        //Kép törlő gomb
        private void DelImg(object sender, RoutedEventArgs e)
        {
            if (alany.kepek.Count() > 0)
            {
                KutyaDAO.DelDbImage(alany.kepek[currentPic].Info.ID);
                KutyaDAO.DelFTPImage(alany.kepek[currentPic].Info.nev);
                alany.kepek.Remove(alany.kepek[currentPic]);

                currentPic = 0;
                reloadImages(currentPic);
            }
        }

        private void PrevImg(object sender, RoutedEventArgs e)
        {
            if (currentPic > 0)
            {
                currentPic--;
                reloadImages(currentPic);
            }
        }

        private void NextImg(object sender, RoutedEventArgs e)
        {
            if (currentPic < alany.kepek.Count - 1)
            {
                currentPic++;
                reloadImages(currentPic);
            }
        }

    }
}
