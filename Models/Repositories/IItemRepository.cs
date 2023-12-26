using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hci_restaurant.Models.Repositories
{
    interface IItemRepository
    {
        ObservableCollection<ItemModel> GetAll();
        void DeleteItem(int id);
        void AddItem(ItemModel item, int categoryId);
        ObservableCollection<ItemModel> GetAllByCategory(int categoryId);
        void UpdateItem(int id, decimal price, int quantity);
    }
}
