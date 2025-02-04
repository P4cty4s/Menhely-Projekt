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
    internal class KennelDAO
    {
        private static string connectionString = "datasource=localhost;port=3306;username=root;password=;database=zoldmenedek";

        public static List<Kennel> AllKennel()
        {
            List<Kennel> result = new List<Kennel>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM kennel";
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand(query,connection))
                {

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Kennel(reader));
                        }
                    }

                }

            }

            return result;
        } 
        public static void CreateKennel(Kennel target)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "INSERT INTO kennel (id,udvarid,kennelszam,kutyak) VALUES (@id,@udvarid,@kennelszam,@kutyak)";
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand(query,connection))
                {
                    cmd.Parameters.AddWithValue("@id",0);
                    cmd.Parameters.AddWithValue("@udvarid",target.UdvarId);
                    cmd.Parameters.AddWithValue("@kennelszam",target.KennelSzam);
                    cmd.Parameters.AddWithValue("@kutyak","");

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Kennel sikeresen létrehozva!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hiba oka: {ex.Message}");
                    }
                }
            }
        }
    }
}
