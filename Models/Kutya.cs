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
        public int meret { get; set; }
        public DateTime szuletes { get; set; }
        public DateTime bekerules { get; set; }
        public bool ivaros { get; set; }
        public string telephely { get; set; }
        public bool foglalt { get; set; }
        public int kennel { get; set; }
        public int indexkepID { get; set; }
        public bool visible { get; set; }

        public static List<Kutya> kutyak = new List<Kutya>();
        public Kutya(MySqlAttributeCollection sor)
        {
            




        }

    }
}
