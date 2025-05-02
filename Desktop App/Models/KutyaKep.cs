using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using FluentFTP;
using MySql.Data.MySqlClient;

namespace Menhely_Projekt.Models
{
    public class KutyaKep
    {
        //Kép
        public KepInfo Info { get; set; }
        public BitmapImage Kep { get; set; }

        public KutyaKep(KepInfo _info, BitmapImage _kep)
        {
            Info = _info;

            try
            {
                Kep = _kep;
            }
            catch (Exception)
            {
                Kep = new BitmapImage();
                MessageBox.Show("Hiba a kép lekérésénél.");
            }
        }

        
    }
}
