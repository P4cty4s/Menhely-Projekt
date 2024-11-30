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
            #region DB_Elrendezes
            kutyusok_db.Margin = new Thickness(
                20 + 15 + kennel_btn.ActualWidth,
                navControl.ActualHeight + 20,
                15,
                20
                );
            #endregion

        }
    }
}
