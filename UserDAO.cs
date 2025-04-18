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

    //Felhasználók - Adatbázis
    public static class UserDAO
    {

        //Connection string
        private static string connectionString = "datasource=localhost;port=3306;username=root;password=;database=pawdmin";

        //Bejelentkezés
        public static int login(string _name,string _password)
        {
            int _id = -1;


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM users WHERE username = @name";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", _name);
                    command.Parameters.AddWithValue("@password", _password);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            string dbHash = reader["password"].ToString().Trim().Replace("$2y$", "$2b$");
                            if (BCrypt.Net.BCrypt.Verify(_password,dbHash))
                            {
                                _id = reader.GetInt32(reader.GetOrdinal("id"));
                            }
                        }
                    }
                }
                connection.Close();
            }
            
            return _id;

        }

        //Név lekérdezése
        public static string getName(int id)
        {
            string result = "";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT username FROM users WHERE id = @azonosito";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@azonosito",id);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        result = reader.GetString(0);
                    }
                }
            }

            return result;
        }

        //Összes felhasználónév lekérdezése
        public static Dictionary<int,string> GetNevek()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM users";
                conn.Open();

                using (MySqlCommand command = new MySqlCommand(query,conn))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(Convert.ToInt32(reader["id"]), reader["username"].ToString());
                        }
                    }
                }
            }

                return result;
        }
    }
}
