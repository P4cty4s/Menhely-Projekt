using Menhely_Projekt.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menhely_Projekt
{
    public class telephelyDAO
    {
        //Connection string
        private static string connectionString = MainWindow._ConnectionString;

        //Összes telephely lekérdezése
        public static List<string> AllTelephely()
        {
            List<string> result = new List<string>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM telephely";

                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(reader["nev"].ToString());
                        }
                    }
                }
            }
            return result;
        }
    }
}
