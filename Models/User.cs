using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menhely_Projekt.Models
{
    internal class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public User(int _id, string _name, string _password)
        {
            ID = _id;
            Name = _name;
            Password = _password;
        }
    }
}
