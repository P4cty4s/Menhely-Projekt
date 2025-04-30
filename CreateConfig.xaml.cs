using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace Menhely_Projekt
{
    /// <summary>
    /// Adatbázis elérésének módja
    /// </summary>
    public partial class CreateConfig : Window
    {
        public CreateConfig()
        {
            InitializeComponent();
        }

        private void CreateCfgFile(object sender, RoutedEventArgs e)
        {
            //datasource=localhost;port=3306;username=root;password=;database=pawdmin

            try
            {
                using (StreamWriter sw = new StreamWriter("config.txt"))
                {
                    sw.WriteLine($"datasource={DataSource_tb.Text};port={Port_tb.Text};username={Username_tb.Text};password={Password_tb.Text};database={DataBase_tb.Text}");
                    sw.Close();
                }

                this.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Valami hiba történt" + exc);
                this.Close();
            }
        }
    }
}
