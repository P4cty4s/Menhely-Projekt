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
using System.Windows.Shapes;
using Menhely_Projekt.Models;

namespace Menhely_Projekt
{
    /// <summary>
    /// Interaction logic for addKennelName.xaml
    /// </summary>
    public partial class addKennelName : Window
    {
        Kennel kennel = new Kennel();
        public addKennelName(Kennel target)
        {
            InitializeComponent();
            kennel = target;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            KennelDAO.CreateKennel(kennel);
        }
    }
}
