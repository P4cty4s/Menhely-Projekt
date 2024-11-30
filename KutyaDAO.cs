using Menhely_Projekt.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menhely_Projekt
{
    internal class KutyaDAO
    {
        private static string connectionString = "datasource=localhost;port=3306;username=root;password=;database=zoldmenedek";

        public static List<Kutya> getKutyak()
        {
            List<Kutya> result = new List<Kutya>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM kutyak";

                using (MySqlCommand command = new MySqlCommand(query,connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                    }
                }
            }
            return result;
        }

    }
}
