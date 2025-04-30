using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Menhely_Projekt.Models;
using MySql.Data.MySqlClient;

namespace Menhely_Projekt
{
    //Udvar - Adatbázis
    internal class UdvarDAO
    {

        //Connection string
        private static string connectionString = MainWindow._ConnectionString;

        //Egy udvar lekérdezése
        public static Udvar GetUdvar(string _id)
        {
            Udvar result = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM udvar WHERE id = @value";

                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand(query,connection))
                {
                    cmd.Parameters.AddWithValue("@value",_id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result.Id = int.Parse(reader[0].ToString());
                            result.nev = reader[1].ToString();

                        }
                    }

                }
            }

            return result;
        }

        //Összes udvar lekérdezése
        public static List<Udvar> AllUdvar()
        {
            List<Udvar> result = new List<Udvar>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM udvar";

                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query,connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Udvar(reader));
                        }
                    }
                }
            }
            return result;
        }
    }
}
