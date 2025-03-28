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
using Menhely_Projekt.Models;

namespace Menhely_Projekt.Controls
{
    /// <summary>
    /// Interaction logic for ChangelogBase.xaml
    /// </summary>
    public partial class ChangelogBase : UserControl
    {
        private static List<Changelog> changelogs =new List<Changelog>();
        public ChangelogBase()
        {
            InitializeComponent();
            FoAblak.currentContent = "Changelog";

            betoltes();
        }

        private void betoltes()
        {
            changelogs = ChangelogDAO.GetAllChangelog();
            Changelog_dg.ItemsSource = changelogs;

            List<string> Categories = new List<string>();
            foreach (string item in changelogs.Select(q => q.Category).Distinct())
            {
                Categories.Add(item);
                Category_cb.Items.Add(item);
            }

            Dictionary<int,string> users = new Dictionary<int,string>();

            users = UserDAO.GetNevek();

            foreach (string item in users.Values)
            {
                User_cb.Items.Add(item);
            }
        }
    }
}
