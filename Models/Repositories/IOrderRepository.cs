using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hci_restaurant.Models.Repositories
{
    public interface IOrderRepository
    {
        ObservableCollection<OrderModel> GetAllByUsername(string username);
        int AddOrder(int tableId, string userUsername);
        int AddOrderWithNote(int tableId, string note, string userUsername);
        void DeleteOrder(int orderId);
        ObservableCollection<OrderHasItemModel> GetItemDataByUsernameAndOrderId(string username, int orderId);
        void AddOrderHasItem(int orderId, ItemModel item, int quantity);
        void DeleteOrderHasItem(int orderId, int itemId);
        OrderModel GetOrderById(int orderId);
    }
}
