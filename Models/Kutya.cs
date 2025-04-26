using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Media.Imaging;

namespace Menhely_Projekt.Models
{
    public class Kutya
    {
        public int ID { get; set; }
        public int regSzam { get; set; }
        public string nev { get; set; }
        public string chipSzam { get; set; }
        public string ivar { get; set; }
        public string meret { get; set; }
        public DateTime szuletes { get; set; }
        public DateTime bekerules { get; set; }
        public string ivaros { get; set; }
        public string telephely { get; set; }
        public bool foglalt { get; set; }
        public int kennel { get; set; }
        public int indexkepID { get; set; }
        public bool visible { get; set; }
        public string status { get; set; }
        public List<KutyaKep> kepek {  get; set; }

        public static List<Kutya> kutyak = new List<Kutya>();

        public Kutya(MySqlDataReader adat)
        {
            ID = Convert.ToInt32(adat["id"]);
            regSzam = Convert.ToInt32(adat["regszam"]);
            nev = adat["nev"].ToString();
            chipSzam = adat["chipszam"].ToString();
            ivar = adat["ivar"].ToString();
            meret = adat["meret"].ToString();
            szuletes = Convert.ToDateTime(adat["szuletes"]);
            bekerules = Convert.ToDateTime(adat["bekerules"]);
            ivaros = adat["ivaros"].ToString();
            telephely = adat["telephely"].ToString();
            foglalt = Convert.ToInt32(adat["foglalt"]) == 1 ? true : false;
            kennel = Convert.ToInt32(adat["kennel"]);
            indexkepID = Convert.ToInt32(adat["indexkepid"]);
            visible = Convert.ToInt32(adat["visible"]) == 1 ? true : false;
            status = adat["status"].ToString();
            kepek = new List<KutyaKep>();

            //--------Kepek betoltese proba-------

            List<KepInfo> seged = KutyaDAO.GetImageDetails(ID);

            foreach (var infok in seged)
            {
                BitmapImage _kep = KutyaDAO.GetModelImage(infok.nev);
                kepek.Add(new KutyaKep(infok,_kep));
            }
        }

        public Kutya()
        {
            
        }

        public override string ToString()
        {
            return ID + "-" + nev + "-" + status;
        }

    }
}
