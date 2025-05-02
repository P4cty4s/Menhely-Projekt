using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Menhely_Projekt.Models
{
    //Udvar
    public class Udvar
    {
        public int Id { get; set; }
        public int TelephelyId { get; set; }
        public string nev { get; set; }
        public Udvar(MySqlDataReader _reader)
        {
            Id = Convert.ToInt32(_reader["id"]);
            nev = _reader["udvarnev"].ToString();
            TelephelyId = Convert.ToInt32(_reader["telephelyid"]);

        }

        public Udvar()
        {
            
        }

        public override string ToString()
        {
            return nev;
        }
    }
}
