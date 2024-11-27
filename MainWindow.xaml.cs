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

namespace Menhely_Projekt
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            int ID = UserDAO.login(tb_name.Text, tb_password.Text);
            if (ID == -1)
            {
                MessageBox.Show("Hibás felhasználónév vagy jelszó");
            } else
            {
                MessageBox.Show("Sikeres bejelentkezés");
                var FoAblak = new FoAblak(ID);
                FoAblak.Show();
                this.Close();
            }
        }
    }
}
