using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menhely_Projekt.Models
{
    internal class Kennel
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
    }
}
