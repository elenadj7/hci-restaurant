using hci_restaurant.Models;

namespace hci_restaurant.Services
{
    internal interface IWindowService
    {
        void OpenLoginWindow();
        void Close(object viewModel);
        void OpenManagerWindow();
        void OpenMainWindow();
        void OpenAlertWindow(string message);
        void OpenIncorrectAlertWindow(string message);
        void OpenConfirmWindow(string message);
        void OpenAddNewUserWindow();
        void OpenAddNewItemWindow();
        void OpenAddNewProcurementWindow();
        void OpenUpdateUserWindow(UserModel user);
        void OpenProcurementDetailsWindow(string username, int procurementId);
        void OpenOrderDetailsWindow(string username, int orderId);
        void OpenAddNewOrderWindow();
        void OpenUpdateItemWindow(ItemModel item);
        void OpenAddNewTableWindow();
        void OpenUpdateTableWindow(TableModel table);
    }
}
