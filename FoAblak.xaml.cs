using Menhely_Projekt.Controls;
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

namespace Menhely_Projekt
{
    /// <summary>
    /// Interaction logic for FoAblak.xaml
    /// </summary>
    public partial class FoAblak : Window
    {
        private NavControl navControl = new NavControl();
        public FoAblak(int id)
        {
            InitializeComponent();
            db_elrendezes();

        }
        private void db_elrendezes() 
        {
            kutyusok_db.Height = this.Height - navControl.Height - 40;
            kutyusok_db.Width = this.Width - kennel_btn.Width - 30;
            kutyusok_db.Margin = new Thickness(
                15+kennel_btn.ActualWidth+20,
                navControl.ActualHeight+20,
                15,
                20
                );
            kutyusok_db.HorizontalAlignment = HorizontalAlignment.Center;

        
        }

    }
}
