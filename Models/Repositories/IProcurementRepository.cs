using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hci_restaurant.Models.Repositories
{
    public interface IProcurementRepository
    {
        ObservableCollection<ProcurementModel> GetAllByUser(string username);
        int AddProcurement(String username);
        void AddProcurementHasItem(int procurementId, ItemModel item, decimal purchasePrice, int quantity);
        ObservableCollection<ProcurementHasItemModel> GetItemDataByUsernameAndProcurementId(string username, int procurementId);
        void DeleteProcurement(int id);
        void DeleteProcurementHasItem(int procurementId, int itemId);
    }
}
