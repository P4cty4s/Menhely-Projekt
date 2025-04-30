using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;

namespace Menhely_Projekt.Models
{
    public class KepInfo
    {
        //Kép információi
        public int ID { get; set; }
        public int KutyaID { get; set; }
        public string nev { get; set; }

        public KepInfo(MySqlDataReader reader)
        {
            ID = Convert.ToInt32(reader["id"]);
            KutyaID = Convert.ToInt32(reader["kutyaid"]);
            nev = reader["nev"].ToString();
        }

        public KepInfo(int _ID,int _KutyaID, string _nev)
        {
            ID = _ID;
            KutyaID = _KutyaID;
            nev = _nev;
        }
    }
}
