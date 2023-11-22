using hci_restaurant.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace hci_restaurant
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, EventArgs e)
        {
            StartWindow startWindow = new();
            startWindow.Show();
            startWindow.IsVisibleChanged += (s, ev) =>
            {
                if (startWindow.IsVisible == false && startWindow.IsLoaded)
                {
                    ManagerWindow mainWindow = new();
                    mainWindow.Show();
                    startWindow.Close();
                }
            };
        }
    }
}
