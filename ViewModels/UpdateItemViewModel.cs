using hci_restaurant.Models.Repositories;
using hci_restaurant.Models;
using hci_restaurant.Repositories;
using hci_restaurant.Services;
using System;
using WPF_LoginForm.ViewModels;
using Prism.Events;
using System.Windows.Input;
using System.Windows;

namespace hci_restaurant.ViewModels
{
    public class UpdateItemViewModel : ViewModelBase
    {
        private ItemModel item;
        private string price;
        private string quantity;
        private readonly IWindowService windowService = new WindowService();
        private readonly IItemRepository itemRepository = new ItemRepository();
        private readonly EventAggregator eventAggregator = (EventAggregator)App.EventAggregator;

        public string Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public string Quantity
        {
            get => quantity;
            set
            {
                quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public ICommand UpdateCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public UpdateItemViewModel()
        {
            UpdateCommand = new ViewModelCommand(ExecuteUpdating);
            CancelCommand = new ViewModelCommand(ExecuteCaneling);
        }

        public UpdateItemViewModel(ItemModel item)
        {
            UpdateCommand = new ViewModelCommand(ExecuteUpdating);
            CancelCommand = new ViewModelCommand(ExecuteCaneling);
            this.item = item;

            Quantity = item.Quantity.ToString();
            Price = item.Price.ToString();
        }

        public void ExecuteCaneling(object parameter)
        {
            windowService.Close(this);
        }

        public void ExecuteUpdating(object parameter)
        {
            int q;
            decimal p; 

            if (!int.TryParse(Quantity, out q) || !decimal.TryParse(Price, out p))
            {
                windowService.OpenIncorrectAlertWindow((string)Application.Current.TryFindResource("AlertNewItem"));
                return;
            }

            itemRepository.UpdateItem(item.Id, p, q);
            eventAggregator.GetEvent<PubSubEvent<Tuple<int, int, decimal>>>().Publish(Tuple.Create(item.Id, q, p));
            windowService.OpenAlertWindow((string)Application.Current.TryFindResource("UpdatedUser"));
        }
    }
}
