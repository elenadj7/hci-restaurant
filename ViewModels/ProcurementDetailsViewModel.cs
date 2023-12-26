using hci_restaurant.Models;
using hci_restaurant.Models.Repositories;
using hci_restaurant.Repositories;
using hci_restaurant.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_LoginForm.ViewModels;

namespace hci_restaurant.ViewModels
{
    public class ProcurementDetailsViewModel : ViewModelBase
    {
        private ObservableCollection<ProcurementHasItemModel> items;
        private readonly IWindowService windowService = new WindowService();
        private readonly IProcurementRepository procurementRepository = new ProcurementRepository();
        private string total;

        public string Total
        {
            get { return total; }
            set
            {
                total = value;
                OnPropertyChanged(nameof(Total));
            }
        }

        public ObservableCollection<ProcurementHasItemModel> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged(nameof(Items));
                SetTotal();
            }
        }

        public ICommand CloseCommand { get; set; }
        public ProcurementDetailsViewModel()
        {
            CloseCommand = new ViewModelCommand(ExecuteClosing);
        }

        public ProcurementDetailsViewModel(int procurementId, string username)
        {
            Items = LoadAllData(procurementId, username);
            CloseCommand = new ViewModelCommand(ExecuteClosing);
        }

        private ObservableCollection<ProcurementHasItemModel> LoadAllData(int procurementId, string username)
        {
            return procurementRepository.GetItemDataByUsernameAndProcurementId(username, procurementId);
        }

        private void ExecuteClosing(object parameter)
        {
            windowService.Close(this);
        }

        private void SetTotal()
        {
            Total = (string)Application.Current.TryFindResource("Total") + " " + Items.Sum(item => item.PurchasePrice * item.Quantity);
        }
    }
}
