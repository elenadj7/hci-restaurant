using hci_restaurant.Models;
using hci_restaurant.Models.Repositories;
using hci_restaurant.Repositories;
using hci_restaurant.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_LoginForm.ViewModels;

namespace hci_restaurant.ViewModels
{
    public class AddNewOrderViewModel : ViewModelBase
    {
        private ObservableCollection<ItemModel> items = new();
        private ObservableCollection<TableModel> tables = new();
        private ObservableCollection<OrderHasItemModel> orderHasItems = new();
        private ItemModel selectedItem;
        private TableModel selectedTable;
        private string quantity;
        private readonly IItemRepository itemRepository = new ItemRepository();
        private readonly IWindowService windowService = new WindowService();
        private readonly IOrderRepository orderRepository = new OrderRepository();
        private readonly ITableRepository tableRepository = new TableRepository();


        public TableModel SelectedTable
        {
            get { return selectedTable; }
            set
            {
                selectedTable = value;
                OnPropertyChanged(nameof(SelectedTable));
            }
        }

        public ObservableCollection<TableModel> Tables
        {
            get { return tables; }
            set
            {
                tables = value;
                OnPropertyChanged(nameof(Tables));
            }
        }

        public ObservableCollection<OrderHasItemModel> OrderHasItems
        {
            get { return orderHasItems; }
            set
            {
                orderHasItems = value;
                OnPropertyChanged(nameof(OrderHasItems));
            }
        }

        public ItemModel SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public ObservableCollection<ItemModel> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public string Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public ICommand DeleteItemCommand { get; set; }
        public ICommand AddItemCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public AddNewOrderViewModel()
        {
            DeleteItemCommand = new ViewModelCommand(ExecuteDeletingItem, CanExecuteDeletingItem);
            CancelCommand = new ViewModelCommand(ExecuteCanceling);
            AddItemCommand = new ViewModelCommand(ExecuteAddingItem, CanExecuteAddingItem);
            AddCommand = new ViewModelCommand(ExecuteAdding, CanExecuteAdding);

            LoadItems();
        }

        private void LoadItems()
        {
            ObservableCollection<ItemModel> i1 = itemRepository.GetAllByCategory(2);
            ObservableCollection<ItemModel> i2 = itemRepository.GetAllByCategory(3);

            foreach (ItemModel i in i1)
            {
                Items.Add(i);
            }

            foreach (ItemModel i in i2)
            {
                Items.Add(i);
            }

            Tables = tableRepository.GetAll();
        }

        private void ExecuteAddingItem(object parameter)
        {
            if (!int.TryParse(Quantity, out _))
            {
                windowService.OpenIncorrectAlertWindow((string)Application.Current.TryFindResource("AlertNewOrder"));
                return;
            }

            OrderHasItemModel orderHasItemModel = new()
            {
                Item = SelectedItem,
                Quantity = int.Parse(Quantity),
            };

            OrderHasItems.Add(orderHasItemModel);
        }

        private bool CanExecuteAddingItem(object parameter)
        {
            if (SelectedItem == null)
            {
                return false;
            }

            return true;
        }

        private void ExecuteCanceling(object parameter)
        {
            windowService.Close(this);
        }

        private void ExecuteDeletingItem(object parameter)
        {
            if (parameter is OrderHasItemModel o)
            {
                OrderHasItems.Remove(o);
            }
        }

        private bool CanExecuteDeletingItem(object parameter)
        {
            return parameter is OrderHasItemModel;
        }

        private void ExecuteAdding(object parameter)
        {
            ClaimsPrincipal? currentUser = Thread.CurrentPrincipal as ClaimsPrincipal;
            if (currentUser != null && (currentUser?.Identity is ClaimsIdentity identity))
            {
                string username = identity.FindFirst(ClaimTypes.Name)?.Value;
                int oId = orderRepository.AddOrder(SelectedTable.Id, username);

                foreach (OrderHasItemModel o in OrderHasItems)
                {
                    orderRepository.AddOrderHasItem(oId, o.Item, o.Quantity);
                }

                windowService.OpenAlertWindow((string)Application.Current.TryFindResource("AddedOrder"));
            }
        }

        private bool CanExecuteAdding(object parameter)
        {
            if (OrderHasItems.Count == 0)
            {
                return false;
            }

            return true;
        }
    }
}
