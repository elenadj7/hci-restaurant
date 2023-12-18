using hci_restaurant.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace hci_restaurant.Services
{
    internal class WindowService : IWindowService
    {
        public void Close(object viewModel)
        {
            Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.DataContext == viewModel);
            window?.Close();
        }

        public void OpenAlertWindow(string message)
        {
            AlertWindow alert = new(message);
            alert.ShowDialog();
        }

        public void OpenLoginWindow()
        {
            LoginWindow loginWindow = new();
            loginWindow.Show();
        }

        public void OpenMainWindow()
        {
            MainWindow mainWindow = new();
            mainWindow.Show();
        }

        public void OpenManagerWindow()
        {
            ManagerWindow managerWindow = new();
            managerWindow.Show();
        }
    }
}
