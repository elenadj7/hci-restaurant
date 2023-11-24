using hci_restaurant.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
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
                    ClaimsPrincipal? currentUser = Thread.CurrentPrincipal as ClaimsPrincipal;
                    if (currentUser != null && (currentUser?.Identity is ClaimsIdentity identity))
                    {
                        short role = short.Parse(identity.FindFirst(ClaimTypes.Role)?.Value);
                        if(role == 1)
                        {
                            ManagerWindow managerWindow = new();
                            managerWindow.Show();
                        }
                        else
                        {
                            MainWindow mainWindow = new();
                            mainWindow.Show();
                        }

                        startWindow.Close();
                    }

                }
            };
        }
    }
}
