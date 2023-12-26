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
    public class OrderDetailsViewModel : ViewModelBase
    {
        private ObservableCollection<OrderHasItemModel> items;
        private readonly IWindowService windowService = new WindowService();
        private readonly IOrderRepository orderRepository = new OrderRepository();
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

        public ObservableCollection<OrderHasItemModel> Items
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
        public OrderDetailsViewModel()
        {
            CloseCommand = new ViewModelCommand(ExecuteClosing);
        }

        public OrderDetailsViewModel(int orderId, string username)
        {
            Items = LoadAllData(orderId, username);
            CloseCommand = new ViewModelCommand(ExecuteClosing);
        }

        private ObservableCollection<OrderHasItemModel> LoadAllData(int orderId, string username)
        {
            return orderRepository.GetItemDataByUsernameAndOrderId(username, orderId);
        }

        private void ExecuteClosing(object parameter)
        {
            windowService.Close(this);
        }

        private void SetTotal()
        {
            Total = (string)Application.Current.TryFindResource("Total") + " " + Items.Sum(item => item.Item.Price * item.Quantity);
        }
    }
}
