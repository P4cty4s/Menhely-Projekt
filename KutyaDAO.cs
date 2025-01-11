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

namespace Menhely_Projekt
{
    internal class KutyaDAO
    {
        private static string connectionString = "datasource=localhost;port=3306;username=root;password=;database=zoldmenedek";

        public static void getKutyak()
        {
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

    }
}
