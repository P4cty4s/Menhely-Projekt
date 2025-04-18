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
        private static string connectionString = "datasource=localhost;port=3306;username=root;password=;database=pawdmin";

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
        public static void CreateChangelog(Changelog target)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
        INSERT INTO changelog (id, userid, category, msg, date)
        VALUES (@id, @userid, @category, @msg, @date)";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", target.Id);
                    cmd.Parameters.AddWithValue("@regszam", target.UserId);
                    cmd.Parameters.AddWithValue("@nev", target.Category);
                    cmd.Parameters.AddWithValue("@chipszam", target.Msg);
                    cmd.Parameters.AddWithValue("@ivar", target.When);

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
