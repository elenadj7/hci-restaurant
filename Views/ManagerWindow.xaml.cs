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

namespace hci_restaurant.Views
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

        /*private void Logout_Click(object sender, RoutedEventArgs e)
        {
            StartWindow startWindow = new();
            startWindow.Show();
            this.Close();
        }*/
    }

}
