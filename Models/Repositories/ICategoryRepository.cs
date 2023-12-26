using System.Collections.ObjectModel;

namespace hci_restaurant.Models.Repositories
{
    interface ICategoryRepository
    {
        ObservableCollection<CategoryModel> GetAll();
    }
}
