using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menhely_Projekt.Models
{
    internal class Kutya
    {
        public int ID { get; set; }
        public int regSzam { get; set; }
        public string nev { get; set; }
        public string chipSzam { get; set; }
        public bool ivar { get; set; }
        public string meret { get; set; }
        public DateTime szuletes { get; set; }
        public DateTime bekerules { get; set; }
        public bool ivaros { get; set; }
        public string telephely { get; set; }
        public bool foglalt { get; set; }
        public int kennel { get; set; }
        public int indexkepID { get; set; }
        public bool visible { get; set; }
        public string status { get; set; }

        public static List<Kutya> kutyak = new List<Kutya>();

        public Kutya(MySqlDataReader adat)
        {
            ID = Convert.ToInt32(adat["id"]);
            regSzam = Convert.ToInt32(adat["regszam"]);
            nev = adat["nev"].ToString();
            chipSzam = adat["chipszam"].ToString();
            ivar = Convert.ToInt32(adat["ivar"]) == 1 ? true : false;
            meret = adat["meret"].ToString();
            szuletes = Convert.ToDateTime(adat["szuletes"]);
            bekerules = Convert.ToDateTime(adat["bekerules"]);
            ivaros = Convert.ToInt32(adat["ivaros"]) == 1 ? true : false;
            telephely = adat["telephely"].ToString();
            foglalt = Convert.ToInt32(adat["foglalt"]) == 1 ? true : false;
            kennel = Convert.ToInt32(adat["kennel"]);
            indexkepID = Convert.ToInt32(adat["indexkepid"]);
            visible = Convert.ToInt32(adat["visible"]) == 1 ? true : false;
            status = adat["status"].ToString();
        }

        public Kutya()
        {
            
        }

    }
}
