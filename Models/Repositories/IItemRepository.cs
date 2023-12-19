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
    }
}
