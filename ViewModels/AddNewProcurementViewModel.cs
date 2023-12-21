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
using System.Xml.Linq;
using WPF_LoginForm.ViewModels;

namespace hci_restaurant.ViewModels
{
    public class AddNewProcurementViewModel : ViewModelBase
    {
        private ObservableCollection<ItemModel> items = new();
        private ObservableCollection<ProcurementHasItemModel> procurementHasItems = new();
        private ItemModel selectedItem;
        private string quantity;
        private string purchasePrice;
        private readonly IItemRepository itemRepository = new ItemRepository();
        private readonly IWindowService windowService = new WindowService();
        private readonly IProcurementRepository procurementRepository = new ProcurementRepository();

        public ObservableCollection<ProcurementHasItemModel> ProcurementHasItems
        {
            get { return procurementHasItems; }
            set
            {
                procurementHasItems = value;
                OnPropertyChanged(nameof(ProcurementHasItems));
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

        public String Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public string PurchasePrice
        {
            get { return purchasePrice; }
            set
            {
                purchasePrice = value;
                OnPropertyChanged(nameof(PurchasePrice));
            }
        }

        public ICommand DeleteItemCommand { get; set; }
        public ICommand AddItemCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public AddNewProcurementViewModel()
        {
            DeleteItemCommand = new ViewModelCommand(ExecuteDeletingItem, CanExecuteDeletingItem);
            CancelCommand = new ViewModelCommand(ExecuteCanceling);
            AddItemCommand = new ViewModelCommand(ExecuteAddingItem, CanExecuteAddingItem);
            AddCommand = new ViewModelCommand(ExecuteAdding, CanExecuteAdding);
            LoadItems();
        }

        private void LoadItems()
        {
            ObservableCollection<ItemModel> i1 = itemRepository.GetAllByCategory(1);
            ObservableCollection<ItemModel> i2 = itemRepository.GetAllByCategory(3);

            foreach (ItemModel i in i1)
            {
                Items.Add(i);
            }

            foreach (ItemModel i in i2)
            {
                Items.Add(i);
            }
        }

        private void ExecuteAddingItem(object parameter)
        {
            if(!int.TryParse(Quantity, out _) || !decimal.TryParse(PurchasePrice, out _))
            {
                windowService.OpenIncorrectAlertWindow((string)Application.Current.TryFindResource("AlertNewItem"));
                return;
            }

            ProcurementHasItemModel procurementHasItemModel = new()
            {
                Item = SelectedItem,
                Quantity = int.Parse(Quantity),
                PurchasePrice = decimal.Parse(PurchasePrice),
            };

            ProcurementHasItems.Add(procurementHasItemModel);
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
            if(parameter is ProcurementHasItemModel p)
            {
                ProcurementHasItems.Remove(p);
            }
        }

        private bool CanExecuteDeletingItem(object parameter)
        {
            return parameter is ProcurementHasItemModel;
        }

        private void ExecuteAdding(object parameter)
        {
            ClaimsPrincipal? currentUser = Thread.CurrentPrincipal as ClaimsPrincipal;
            if (currentUser != null && (currentUser?.Identity is ClaimsIdentity identity))
            {
                string username = identity.FindFirst(ClaimTypes.Name)?.Value;
                int pId = procurementRepository.AddProcurement(username);

                foreach (ProcurementHasItemModel p in ProcurementHasItems)
                {
                    procurementRepository.AddProcurementHasItem(pId, p.Item, p.PurchasePrice, p.Quantity);
                }

                windowService.OpenAlertWindow((string)Application.Current.TryFindResource("AddedProcurement"));
            }
        }

        private bool CanExecuteAdding(object parameter)
        {
            if(ProcurementHasItems.Count == 0)
            {
                return false;
            }

            return true;
        }
    }
}
