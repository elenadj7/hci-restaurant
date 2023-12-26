using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hci_restaurant.Models.Repositories
{
    public interface ITableRepository
    {
        ObservableCollection<TableModel> GetAll();
        void AddTable(int seats);
        void DeleteTable(int id);
    }
}
