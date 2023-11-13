using hci_restaurant.Util;
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

namespace hci_restaurant.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public ManagerWindow()
        {
            InitializeComponent();
            // Load the dashboard page by default
            //MainFrame.Navigate(new Uri("DashboardPage.xaml", UriKind.Relative));
        }

        private void LightTheme_Click(object sender, RoutedEventArgs e)
        {
            AppTheme.ChangeTheme(new Uri("../Themes/LightTheme.xaml", UriKind.Relative));
        }

        private void DarkTheme_Click(object sender, RoutedEventArgs e)
        {
            AppTheme.ChangeTheme(new Uri("../Themes/DarkTheme.xaml", UriKind.Relative));
        }

        private void ProfessionalTheme_Click(object sender, RoutedEventArgs e)
        {
            AppTheme.ChangeTheme(new Uri("../Themes/ProfessionalTheme.xaml", UriKind.Relative));
        }

        private void EnglishLanguage_Click(object sender, RoutedEventArgs e)
        {
            AppLanguage.ChangeLanguage(new Uri("../Languages/EnglishLanguage.xaml", UriKind.Relative));
        }

        private void SerbianLanguage_Click(object sender, RoutedEventArgs e)
        {
            AppLanguage.ChangeLanguage(new Uri("../Languages/SerbianLanguage.xaml", UriKind.Relative));
        }

        private void SerbianCyrillicLanguage_Click(object sender, RoutedEventArgs e)
        {
            AppLanguage.ChangeLanguage(new Uri("../Languages/SerbianCyrillicLanguage.xaml", UriKind.Relative));
        }
    }

}
