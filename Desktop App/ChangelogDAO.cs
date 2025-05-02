using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Menhely_Projekt.Models;
using MySql.Data.MySqlClient;

namespace Menhely_Projekt
{
    //Changelog - Adatbázis
    internal class ChangelogDAO
    {
        //Connectiong string
        private static string connectionString = MainWindow._ConnectionString;

        //Aktív felhasználónév lekérése
        public static string userName = UserDAO.getName(FoAblak.UserId);

        //Minden rekord
        public static List<Changelog> GetAllChangelog()
        {
            List<Changelog> result = new List<Changelog>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM changelog";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Changelog(reader));
                        }
                    }
                }
                connection.Close();
                return result;
            }
        }

        //Rekord létrehozása
        public static void CreateChangelog(string _msg, string[] category)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
        INSERT INTO changelog (id, userid, category, msg, date)
        VALUES (@id, @userid, @category, @msg, @date)";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", 0);
                    cmd.Parameters.AddWithValue("@userid", FoAblak.UserId);
                    cmd.Parameters.AddWithValue("@category", $"{category[0]} {category[1]}");
                    cmd.Parameters.AddWithValue("@msg", $"{userName} {_msg}");
                    cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-d H:m:s"));

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hiba a changeloggal. Oka: {ex.Message}");
                    }
                }
            }
        }
    }
}
