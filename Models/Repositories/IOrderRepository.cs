using System.Collections.ObjectModel;

namespace hci_restaurant.Models.Repositories
{
    public interface IOrderRepository
    {
        ObservableCollection<OrderModel> GetAllByUsername(string username);
        ObservableCollection<OrderModel> GetAllByYear(string username, int year);
        ObservableCollection<OrderModel> GetAllByMonth(string username, int month);
        ObservableCollection<OrderModel> GetAllByYearAndMonth(string username, int year, int month);
        int AddOrder(int tableId, string userUsername);
        int AddOrderWithNote(int tableId, string note, string userUsername);
        void DeleteOrder(int orderId);
        ObservableCollection<OrderHasItemModel> GetItemDataByUsernameAndOrderId(string username, int orderId);
        void AddOrderHasItem(int orderId, ItemModel item, int quantity);
        void DeleteOrderHasItem(int orderId, int itemId);
        OrderModel GetOrderById(int orderId);
    }
}
