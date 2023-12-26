using hci_restaurant.Views;
using Prism.Events;
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
        public static readonly IEventAggregator EventAggregator = new EventAggregator();

        protected void ApplicationStart(object sender, EventArgs e)
        {
            LoginWindow loginWindow = new();
            loginWindow.Show();
        }
    }
}
