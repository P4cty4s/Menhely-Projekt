using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menhely_Projekt.Models
{
    internal class Changelog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Msg { get; set; }
        public DateTime When { get; set; }
        public Changelog(int _id,int user, DateTime _when,string _msg)
        {
            Id = _id;
            UserId = user;
            When = _when;
            Msg = _msg;
        }
    }
}
