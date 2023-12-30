using System.Collections.ObjectModel;

namespace hci_restaurant.Models.Repositories
{
    public interface ITableRepository
    {
        ObservableCollection<TableModel> GetAll();
        int AddTable(int seats);
        void DeleteTable(int id);
        void UpdateTable(int id, int seats);
        ObservableCollection<TableModel> GetAllByFilter(int seats);
    }
}
