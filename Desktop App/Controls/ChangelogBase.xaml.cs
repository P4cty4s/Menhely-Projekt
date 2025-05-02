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
    /// Changelog oldal, változtatások számon tartása, szűrés rájuk
    /// </summary>
    public partial class ChangelogBase : UserControl
    {
        //Módosítások
        private static List<Changelog> changelogs =new List<Changelog>();

        //Filterelt módosítás gyütemény
        private static List<Changelog> Filtered = new List<Changelog>();

        //Filter számontartó
        private static string[] usedFilters = new string[3]; //0.Elem = User | 1.Elem = Kategoria | 2.Elem = Alkategoria

        //Userek gyütemény
        private static Dictionary<int, string> users = UserDAO.GetNevek();
        public ChangelogBase()
        {
            InitializeComponent();
            FoAblak.currentContent = "Changelog";

            betoltes();
            Filtered = changelogs;
        }
        
        //Minden adat betöltése
        private void betoltes()
        {
            foreach (string item in users.Values)
            {
                User_cb.Items.Add(item);
            }

            changelogs = ChangelogDAO.GetAllChangelog();
            Changelog_dg.ItemsSource = changelogs;

            foreach (string item in changelogs.Select(q => q.Category).Distinct())
            {
                string[] tomb = item.Split(' ');

                if (!Category_cb.Items.Contains(tomb[0]))
                {
                    Category_cb.Items.Add(tomb[0]);
                }

                if (!SubCategory_cb.Items.Contains(tomb[1]))
                {
                    SubCategory_cb.Items.Add(tomb[1]);
                }
            }
        }

        //Filterek kezelése
        private void ManageFilter()
        {
            Filtered = changelogs;

            if (usedFilters[0] != "" && usedFilters[0] != null)
            {
                Filtered = Filtered.Where(q => q.UserId == users.First(s => s.Value == usedFilters[0].ToString()).Key).ToList();
            }

            if (usedFilters[1] != "" && usedFilters[1] != null)
            {
                Filtered = Filtered.Where(q => q.Category.Contains(usedFilters[1])).ToList();
            }

            if (usedFilters[2] != "" && usedFilters[2] != null)
            {
                Filtered = Filtered.Where(q => q.Category.Contains(usedFilters[2])).ToList();
            }

            Changelog_dg.ItemsSource = Filtered;
        }

        //User filter használata
        private void UserFilter(object sender, SelectionChangedEventArgs e)
        {
            if (User_cb.SelectedItem != null && User_cb.SelectedItem.ToString() != "")
            {
                usedFilters[0] = User_cb.SelectedItem.ToString();
                ManageFilter();
            }
        }

        //Kategória filter használata
        private void CategoryFilter(object sender, SelectionChangedEventArgs e)
        {
            if (Category_cb.SelectedItem != null && Category_cb.SelectedItem.ToString() != "")
            {
                usedFilters[1] = Category_cb.SelectedItem.ToString();
                ManageFilter();
            }
        }

        //Alkategória használata
        private void SubCategoryFilter(object sender, SelectionChangedEventArgs e)
        {
            if (SubCategory_cb.SelectedItem != null && SubCategory_cb.SelectedItem.ToString() != "")
            {
                usedFilters[2] = SubCategory_cb.SelectedItem.ToString();
                ManageFilter();
            }
        }

        //Filterek alaphelyzetbe állítása
        private void FilterReset(object sender, RoutedEventArgs e)
        {
            usedFilters = new string[3];

            Category_cb.SelectedIndex = -1;
            SubCategory_cb.SelectedIndex = -1;
            User_cb.SelectedIndex = -1;

            Changelog_dg.ItemsSource = changelogs;
        }
    }
}
