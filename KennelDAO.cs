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
    //Kennel - Adatbázis
    internal class KennelDAO
    {
        //Connection string
        private static string connectionString = "datasource=localhost;port=3306;username=root;password=;database=pawdmin";

        //Kutya ID-k egybevarrása
        private static string createQuery(List<Kutya> _kutyak)
        {
            string _query = "";

            foreach (Kutya item in _kutyak)
            {
                _query += item.ID+";";
            }

            if (_query.EndsWith(";"))
            {
                _query = _query.Remove(_query.Length - 1,1);
            }

            return _query;
        }

        //Kennelek lekérdezése
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

        //Kennel létrehozása
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
                        ChangelogDAO.CreateChangelog($"létrehozott egy új kennelt({LatestID()})", new string[] { "kennel", "létrehozva" });
                        MessageBox.Show("Kennel sikeresen létrehozva!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hiba oka: {ex.Message}");
                    }
                }
            }
        }

        //Következő ID lekérése
        public static int LatestID()
        {
            int result = 0;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT MAX(Id) AS LatestId FROM kennel";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    object item = cmd.ExecuteScalar();
                    result = item != DBNull.Value ? Convert.ToInt32(item) : 0;

                }
            }
            return result;
        }

        //Kennel módosítása
        public static async Task SetKennel(List<Kennel> target)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (var transaction = await conn.BeginTransactionAsync())
                {
                    Console.WriteLine("Itt jár");
                    try
                    {
                        foreach (var item in target)
                        {
                            string szoveg = createQuery(item.Kutyak);
                            string query = "UPDATE kennel SET kutyak = @kutyak WHERE id = @id";

                            using (var command = new MySqlCommand(query, conn, transaction))
                            {
                                command.Parameters.AddWithValue("@kutyak", szoveg);
                                command.Parameters.AddWithValue("@id", item.Id);

                                await command.ExecuteNonQueryAsync();
                            }
                        }

                        await transaction.CommitAsync();
                        MessageBox.Show("LEGGOO");
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        MessageBox.Show($"Transaction failed: {ex.Message}");
                        throw;
                    }
                }
            }
        }

        //Kennel törlése
        public static void DelKennel(int _id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "DELETE FROM kennel WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", _id);

                    try
                    {
                        int affectedRows = cmd.ExecuteNonQuery();

                        if (affectedRows > 0)
                        {
                            MessageBox.Show("Sikeres törlés!");
                        }
                        else
                        {
                            MessageBox.Show("Nem található ilyen rekord.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hiba a törlés közben: {ex.Message}");
                    }
                }
            }

        }

    }
}
