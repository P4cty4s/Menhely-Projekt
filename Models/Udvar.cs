using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Menhely_Projekt.Models
{
    internal class Udvar
    {
        public int Id { get; set; }
        public string nev { get; set; }
        public Udvar(int _id, string _nev)
        {
            Id = _id;
            nev = _nev;
        }
        public Udvar(MySqlDataReader _reader)
        {
            Id = Convert.ToInt32(_reader["id"]);
            nev = _reader["udvarnev"].ToString();
        }

        public override string ToString()
        {
            return nev;
        }
    }
}
