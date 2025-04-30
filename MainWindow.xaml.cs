using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.IO;

namespace Menhely_Projekt
{
    public partial class MainWindow : Window
    {
        public static int ID;
        public static string _ConnectionString;
        public MainWindow()
        {
            
            InitializeComponent();

            ConfigCheck();

            ID = -1;
        }

        //ConnString létrehozás
        private void ConfigCheck()
        {

            if (File.Exists("config.txt"))
            {
                using (StreamReader sr = new StreamReader("config.txt"))
                {
                    _ConnectionString = sr.ReadLine();
                    sr.Close();
                }
                
            } else
            {
                CreateConfig _creator = new CreateConfig();
                _creator.ShowDialog();
            }
            this.ShowDialog();
        }

        //Elérhető e az adatbázis
        private static bool DBActive()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_ConnectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        //Bejelentkezés
        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            if (DBActive())
            {
#if DEBUG
                tb_name.Text = "admin";
                tb_password.Text = "0";
#endif
                ID = UserDAO.login(tb_name.Text, tb_password.Text);
                if (ID == -1)
                {
                    MessageBox.Show("Hibás felhasználónév vagy jelszó");
                }
                else
                {
                    MessageBox.Show("Sikeres bejelentkezés");
                    var FoAblak = new FoAblak(ID);
                    FoAblak.Show();
                    
                    this.Hide();
                }
            } else
            {
                MessageBox.Show("Az adatbázis nem elérhető");
            }

        }

        //Config manuális átállítása
        private void OpenConfig(object sender, RoutedEventArgs e)
        {
            try
            {
                if (File.Exists("config.txt"))
                {
                    CreateConfig _creator = new CreateConfig();
                    using (StreamReader sr = new StreamReader("config.txt"))
                    {
                        string sor = sr.ReadLine();

                        string[] reszek = sor.Split(';');

                        _creator.DataSource_tb.Text = reszek[0].Split('=')[1];

                        _creator.Port_tb.Text = reszek[1].Split('=')[1];

                        _creator.Username_tb.Text = reszek[2].Split('=')[1];

                        _creator.Password_tb.Text = reszek[3].Split('=')[1];

                        _creator.DataBase_tb.Text = reszek[4].Split('=')[1];

                        sr.Close();
                    }

                    _creator.ShowDialog();
                } else
                {
                    ConfigCheck();
                }
            }
            catch
            {
                MessageBox.Show("Nem volt megfelelő Connection String");
                CreateConfig _creator = new CreateConfig();
                _creator.ShowDialog();
            }

          
            
        }
    }
}
