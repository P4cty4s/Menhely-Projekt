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
    public static class UserDAO
    {
        private static string connectionString = "datasource=localhost;port=3306;username=root;password=;database=zoldmenedek";
        public static int login(string _name,string _password)
        {
            int _id = -1;


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT id FROM users WHERE username = @name AND password = @password";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", _name);
                    command.Parameters.AddWithValue("@password", _password);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            _id = reader.GetInt32(0);
                        }
                    }
                }
                connection.Close();
            }
            
            return _id;

        }
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
    }
}
