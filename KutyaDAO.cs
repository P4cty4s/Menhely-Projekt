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
                            Kutya.kutyak.Add(new Kutya()
                            {
                                ID = Convert.ToInt32(reader["id"]),
                                regSzam = Convert.ToInt32(reader["regszam"]),
                                nev = reader["nev"].ToString(),
                                chipSzam = reader["chipszam"].ToString(),
                                ivar = Convert.ToInt32(reader["ivar"]) == 1 ? true : false,
                                meret = Convert.ToInt32(reader["meret"]),
                                szuletes = Convert.ToDateTime(reader["szuletes"]),
                                bekerules = Convert.ToDateTime(reader["bekerules"]),
                                ivaros = Convert.ToInt32(reader["ivaros"]) == 1 ? true : false,
                                telephely = reader["telephely"].ToString(),
                                foglalt = Convert.ToInt32(reader["foglalt"]) == 1 ? true : false,
                                kennel = Convert.ToInt32(reader["kennel"]),
                                indexkepID = Convert.ToInt32(reader["indexkepid"]),
                                visible = Convert.ToInt32(reader["visible"]) == 1 ? true : false
                            });
                            
                        }
                    }
                }
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
                            result.Add(new Kutya()
                            {
                                ID = Convert.ToInt32(reader["id"]),
                                regSzam = Convert.ToInt32(reader["regszam"]),
                                nev = reader["nev"].ToString(),
                                chipSzam = reader["chipszam"].ToString(),
                                ivar = Convert.ToInt32(reader["ivar"]) == 1 ? true : false,
                                meret = Convert.ToInt32(reader["meret"]),
                                szuletes = Convert.ToDateTime(reader["szuletes"]),
                                bekerules = Convert.ToDateTime(reader["bekerules"]),
                                ivaros = Convert.ToInt32(reader["ivaros"]) == 1 ? true : false,
                                telephely = reader["telephely"].ToString(),
                                foglalt = Convert.ToInt32(reader["foglalt"]) == 1 ? true : false,
                                kennel = Convert.ToInt32(reader["kennel"]),
                                indexkepID = Convert.ToInt32(reader["indexkepid"]),
                                visible = Convert.ToInt32(reader["visible"]) == 1 ? true : false
                            });
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
                        target = new Kutya()
                        {
                            ID = Convert.ToInt32(reader["id"]),
                            regSzam = Convert.ToInt32(reader["regszam"]),
                            nev = reader["nev"].ToString(),
                            chipSzam = reader["chipszam"].ToString(),
                            ivar = Convert.ToInt32(reader["ivar"]) == 1 ? true : false,
                            meret = Convert.ToInt32(reader["meret"]),
                            szuletes = Convert.ToDateTime(reader["szuletes"]),
                            bekerules = Convert.ToDateTime(reader["bekerules"]),
                            ivaros = Convert.ToInt32(reader["ivaros"]) == 1 ? true : false,
                            telephely = reader["telephely"].ToString(),
                            foglalt = Convert.ToInt32(reader["foglalt"]) == 1 ? true : false,
                            kennel = Convert.ToInt32(reader["kennel"]),
                            indexkepID = Convert.ToInt32(reader["indexkepid"]),
                            visible = Convert.ToInt32(reader["visible"]) == 1 ? true : false
                        };

                    }

                }

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
                    visible = @visible
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

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Sikeres mentés!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Valami hiba történt: {ex.Message}");
                    }

                }
            }
        }

    }
}
