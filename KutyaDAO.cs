using Google.Protobuf.WellKnownTypes;
using Menhely_Projekt.Models;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Menhely_Projekt
{
    internal class KutyaDAO
    {
        private static string connectionString = "datasource=localhost;port=3306;username=root;password=;database=zoldmenedek";

        public static void getKutyak()
        {
            Kutya.kutyak.Clear();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM kutyak";

                using (MySqlCommand command = new MySqlCommand(query,connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Kutya.kutyak.Add(new Kutya(reader));
                        }
                    }
                }
                connection.Close();
            }
        }
        public static List<Kutya> searchKutya(string prop,string value)
        {
            List<Kutya> result = new List<Kutya>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT * FROM kutyak WHERE {prop} LIKE @value";

                using (MySqlCommand command = new MySqlCommand(query,connection))
                {
                    command.Parameters.AddWithValue("@value", value + "%");
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Kutya(reader));
                        }
                    }

                }

            }

            return result;
        }
        public static Kutya egyKutya(int ID)
        {
            Kutya target = new Kutya();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM kutyak WHERE id = @value";

                using (MySqlCommand command = new MySqlCommand(query,connection))
                {
                    command.Parameters.AddWithValue("@value",ID);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        target = new Kutya(reader);

                    }

                }
                connection.Close();
            }
            
            return target;
        }

        public static void updateKutya(Kutya infok)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
                UPDATE kutyak
                SET 
                    regSzam = @regSzam,
                    nev = @nev,
                    chipSzam = @chipSzam,
                    ivar = @ivar,
                    meret = @meret,
                    szuletes = @szuletes,
                    bekerules = @bekerules,
                    ivaros = @ivaros,
                    telephely = @telephely,
                    foglalt = @foglalt,
                    kennel = @kennel,
                    indexkepID = @indexkepID,
                    visible = @visible,
                    status = @status
                WHERE ID = @ID;
            ";
                using (MySqlCommand cmd = new MySqlCommand(query,connection))
                {
                    cmd.Parameters.AddWithValue("@ID", infok.ID);
                    cmd.Parameters.AddWithValue("@regSzam", infok.regSzam);
                    cmd.Parameters.AddWithValue("@nev", infok.nev);
                    cmd.Parameters.AddWithValue("@chipSzam", infok.chipSzam);
                    cmd.Parameters.AddWithValue("@ivar", infok.ivar);
                    cmd.Parameters.AddWithValue("@meret", infok.meret);
                    cmd.Parameters.AddWithValue("@szuletes", infok  .szuletes);
                    cmd.Parameters.AddWithValue("@bekerules", infok.bekerules);
                    cmd.Parameters.AddWithValue("@ivaros", infok.ivaros);
                    cmd.Parameters.AddWithValue("@telephely", infok .telephely);
                    cmd.Parameters.AddWithValue("@foglalt", infok.foglalt);
                    cmd.Parameters.AddWithValue("@kennel", infok.kennel);
                    cmd.Parameters.AddWithValue("@indexkepID", infok.indexkepID);
                    cmd.Parameters.AddWithValue("@visible", infok.visible);
                    cmd.Parameters.AddWithValue("@status",infok.status);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Sikeres mentés!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Valami hiba történt: {ex.Message}");
                    }
                    connection.Close();
                }
            }
        }

        public static void createKutya(Kutya newKutya)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
        INSERT INTO kutyak (id, regszam, nev, chipszam, ivar, meret, szuletes, bekerules, ivaros, telephely, foglalt, kennel, indexkepID, visible, status)
        VALUES (@id, @regszam, @nev, @chipszam, @ivar, @meret, @szuletes, @bekerules, @ivaros, @telephely, @foglalt, @kennel, @indexkepID, @visible, @status)";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", newKutya.ID);
                    cmd.Parameters.AddWithValue("@regszam", newKutya.regSzam);
                    cmd.Parameters.AddWithValue("@nev", newKutya.nev);
                    cmd.Parameters.AddWithValue("@chipszam", newKutya.chipSzam);
                    cmd.Parameters.AddWithValue("@ivar", newKutya.ivar);
                    cmd.Parameters.AddWithValue("@meret", newKutya.meret);
                    cmd.Parameters.AddWithValue("@szuletes", newKutya.szuletes);
                    cmd.Parameters.AddWithValue("@bekerules", newKutya.bekerules);
                    cmd.Parameters.AddWithValue("@ivaros", newKutya.ivaros);
                    cmd.Parameters.AddWithValue("@telephely", newKutya.telephely);
                    cmd.Parameters.AddWithValue("@foglalt", newKutya.foglalt);
                    cmd.Parameters.AddWithValue("@kennel", newKutya.kennel);
                    cmd.Parameters.AddWithValue("@indexkepID", newKutya.indexkepID);
                    cmd.Parameters.AddWithValue("@visible", newKutya.visible);
                    cmd.Parameters.AddWithValue("@status", newKutya.status);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Sikeres hozzáadás!");
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
