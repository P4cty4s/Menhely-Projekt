using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Menhely_Projekt.Models
{
    public class Kennel
    {
        public int Id { get; set; }
        public int UdvarId { get; set; }
        public int KennelSzam { get; set; }
        public List<Kutya> Kutya { get; set; }


        public Kennel(int _id,int _udvarid, int szam)
        {
            Kutya = new List<Kutya>();

            Id = _id;
            UdvarId = _udvarid;
            KennelSzam = szam;

        }

        public Kennel()
        {
            
        }

        public Kennel(MySqlDataReader _reader)
        {
            Id = Convert.ToInt32(_reader["id"]);
            UdvarId = Convert.ToInt32(_reader["udvarid"]);
            KennelSzam = Convert.ToInt32(_reader["kennelszam"]);
            Kutya = new List<Kutya>();

            if (_reader["kutyak"].ToString() != "" && _reader["kutyak"].ToString() != null)
            {
                string[] tomb;
                tomb = _reader["kutyak"].ToString().Split(';');
                foreach (string item in tomb)
                {
                    Kutya.Add(KutyaDAO.egyKutya(int.Parse(item)));
                }

            }       
        }
    }
}
