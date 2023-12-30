using System;
using System.Collections.ObjectModel;

namespace hci_restaurant.Models.Repositories
{
    public interface IProcurementRepository
    {
        ObservableCollection<ProcurementModel> GetAllByUser(string username);
        ObservableCollection<ProcurementModel> GetAllByYear(string username, int year);
        ObservableCollection<ProcurementModel> GetAllByMonth(string username, int month);
        ObservableCollection<ProcurementModel> GetAllByYearAndMonth(string username, int year, int month);
        int AddProcurement(String username);
        void AddProcurementHasItem(int procurementId, ItemModel item, decimal purchasePrice, int quantity);
        ObservableCollection<ProcurementHasItemModel> GetItemDataByUsernameAndProcurementId(string username, int procurementId);
        void DeleteProcurement(int id);
        void DeleteProcurementHasItem(int procurementId, int itemId);
        ProcurementModel GetProcurementById(int procurementId);
    }
}
