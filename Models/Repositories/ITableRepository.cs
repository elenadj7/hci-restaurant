using System.Collections.ObjectModel;

namespace hci_restaurant.Models.Repositories
{
    public interface ITableRepository
    {
        ObservableCollection<TableModel> GetAll();
        void AddTable(int seats);
        void DeleteTable(int id);
    }
}
