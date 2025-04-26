using Google.Protobuf.WellKnownTypes;
using Menhely_Projekt.Models;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Menhely_Projekt
{
    //Kutya - Adatbázis
    internal class KutyaDAO
    {
        //Connection string
        private static string connectionString = "datasource=localhost;port=3306;username=root;password=;database=pawdmin";

        //Minden Kutya lekérdezése
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

        //Kutya kereső
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

        //Egy kutya
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

        //Kutya változtatása
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
                        ChangelogDAO.CreateChangelog($"megváltoztatta {infok.nev}({infok.ID}) kutyát", new string[] {"kutya", "módosítva"});
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

        //Kutya létrehozása
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
                        ChangelogDAO.CreateChangelog($"létrehozta {newKutya.nev}({newKutya.ID}) kutyát", new string[] { "kutya", "létrehozva" });
                        MessageBox.Show("Sikeres hozzáadás!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hiba oka: {ex.Message}");
                    }
                }
            }

        }

        //Nálunk tartózkodó kutyák lekérése
        public static List<Kutya> getMyKutya()
        {
            List<Kutya> result = new List<Kutya>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM kutyak WHERE status = @value1 OR status = @value2";
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand(query,connection))
                {
                    cmd.Parameters.AddWithValue("@value1","Sérült");
                    cmd.Parameters.AddWithValue("@value2","Nálunk van");

                    using (MySqlDataReader reader = cmd.ExecuteReader())
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

        //A legkésöbbi ID kikeresése
        public static int LatestID()
        {
            int result = 0;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT MAX(id) AS LatestId FROM kutyak";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    object item = cmd.ExecuteScalar();
                    result = item != DBNull.Value ? Convert.ToInt32(item) : 0;

                }
            }
            return result;
        }

        //Kep tulajdonsagok lekerdezese

        public static List<KepInfo> GetImageDetails(int _ID)
        {
            List<KepInfo> result = new List<KepInfo>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM kutyakep WHERE kutyaid = @value";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@value", _ID);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new KepInfo(reader));
                        }
                    }
                }
                connection.Close();
            }
            return result;
        }

        //Kutyakep adatainak feltoltese

        public static KepInfo SetKutyaImages(int _ID, string _nev)
        {
            KepInfo _kepInfo = null;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = @"INSERT INTO kutyakep (kutyaid, nev) VALUES (@kutyaid, @nev); SELECT LAST_INSERT_ID();";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@kutyaid", _ID);
                    cmd.Parameters.AddWithValue("@nev", _nev);

                    try
                    {
                        // Insert + get the new ID
                        long newId = Convert.ToInt64(cmd.ExecuteScalar());

                        // Now select the newly inserted record
                        string selectQuery = "SELECT id, kutyaid, nev FROM kutyakep WHERE id = @id";

                        using (MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn))
                        {
                            selectCmd.Parameters.AddWithValue("@id", newId);

                            using (MySqlDataReader reader = selectCmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    _kepInfo = new KepInfo(reader);
                                }
                            }
                        }

                        MessageBox.Show("Sikeres képfeltöltés!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hiba oka: {ex.Message}");
                    }
                }
            }

            return _kepInfo;
        }


        //Kutyahoz kep rendelese (ftp)

        public static BitmapImage GetModelImage(string imageName)
        {
            string host = "127.0.0.1";
            string username = "Menhely_Projekt";
            string password = "admin";

            try
            {
                string ftpUrl = $"ftp://{host}/uploads/{imageName}";
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(username, password);

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream == null)
                        return null;

                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = new MemoryStream();
                    responseStream.CopyTo(bitmap.StreamSource);
                    bitmap.StreamSource.Position = 0;
                    bitmap.EndInit();
                    bitmap.Freeze();
                    return bitmap;
                }
            }
            catch
            {
                return null;
            }
        }

        //Kep torlese adatbázisból

        public static void DelDbImage(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "DELETE FROM kutyakep WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

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

        //Törlés az ftp szerverről

        public static void DelFTPImage(string _nev)
        {
            string host = "127.0.0.1";
            string username = "Menhely_Projekt";
            string password = "admin";

            try
            {
                string ftpFullPath = $"ftp://{host}/uploads/{_nev}";

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpFullPath);
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                request.Credentials = new NetworkCredential(username, password);

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    MessageBox.Show($"Törlés sikeres: {response.StatusDescription}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba az FTP törlés közben: {ex.Message}");
            }
        }


    }
}
