using hci_restaurant.Models;
using hci_restaurant.Views;
using System.Linq;
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

        public void OpenAddNewItemWindow()
        {
            AddNewItemWindow addNewItemWindow = new();
            addNewItemWindow.ShowDialog();
        }

        public void OpenAddNewOrderWindow()
        {
            AddNewOrderWindow addNewOrderWindow = new();
            addNewOrderWindow.ShowDialog();
        }

        public void OpenAddNewProcurementWindow()
        {
            AddNewProcurementWindow addNewProcurementWindow = new();
            addNewProcurementWindow.ShowDialog();
        }

        public void OpenAddNewUserWindow()
        {
            AddNewUserWindow addNewUserWindow = new();
            addNewUserWindow.ShowDialog();
        }

        public void OpenAlertWindow(string message)
        {
            AlertWindow alert = new(message);
            alert.ShowDialog();
        }

        public void OpenConfirmWindow(string message)
        {
            ConfirmWindow confirm = new(message);
            confirm.ShowDialog();
        }

        public void OpenIncorrectAlertWindow(string message)
        {
            AlertWindow alertWindow = new(message, false);
            alertWindow.ShowDialog();
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

        public void OpenOrderDetailsWindow(string username, int orderId)
        {
            OrderDetailsWindow orderDetailsWindow = new(username, orderId);
            orderDetailsWindow.ShowDialog();
        }

        public void OpenProcurementDetailsWindow(string username, int procurementId)
        {
            ProcurementDetailsWindow procurementDetailsWindow = new(username, procurementId);
            procurementDetailsWindow.ShowDialog();
        }

        public void OpenUpdateItemWindow(ItemModel item)
        {
            UpdateItemWindow updateItemWindow = new(item);
            updateItemWindow.ShowDialog();
        }

        public void OpenUpdateUserWindow(UserModel user)
        {
            UpdateUserWindow updateUserWindow = new(user);
            updateUserWindow.ShowDialog();
        }
    }
}
