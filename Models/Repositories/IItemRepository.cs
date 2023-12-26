using System.Collections.ObjectModel;

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
