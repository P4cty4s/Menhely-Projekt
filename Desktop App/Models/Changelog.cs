﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Menhely_Projekt.Models
{
    internal class Changelog
    {
        //Módosítások
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Category { get; set; }
        public string Msg { get; set; }
        public DateTime When { get; set; }
        public Changelog(int _id,int user, DateTime _when,string _msg)
        {
            Id = 0;
            UserId = user;
            When = DateTime.Now;
            Msg = _msg;
        }
        public Changelog(MySqlDataReader reader)
        {
            Id = Convert.ToInt32(reader["id"]);
            UserId = Convert.ToInt32(reader["userid"]);
            Category = reader["category"].ToString();
            Msg = reader["msg"].ToString();
            When = Convert.ToDateTime(reader["date"]);
        }
    }
}
