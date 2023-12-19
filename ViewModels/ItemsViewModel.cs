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
    public class ItemsViewModel : ViewModelBase
    {
        private ObservableCollection<ItemModel> items = new();
        private readonly IWindowService windowService = new WindowService();
        private readonly IItemRepository repository = new ItemRepository();

        public ObservableCollection<ItemModel> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public ICommand DeleteItemCommand { get; set; }
        public ICommand EditItemCommand { get; set; }
        public ICommand NewItemCommand { get; set; }

        public ItemsViewModel()
        {
            Items = LoadAllItems();
            DeleteItemCommand = new ViewModelCommand(ExecuteDeleting, CanExecuteDelete);
            EditItemCommand = new ViewModelCommand(ExecuteEditing, CanExecuteEdit);
            NewItemCommand = new ViewModelCommand(ExecuteAddingItem);
        }
        
        private void ExecuteAddingItem(object parameter)
        {
            windowService.OpenAddNewItemWindow();
        }

        private ObservableCollection<ItemModel> LoadAllItems()
        {
            return repository.GetAll();
        }

        private void ExecuteDeleting(object parameter)
        {
            if (parameter is ItemModel item)
            {
                windowService.OpenConfirmWindow((string)Application.Current.TryFindResource("DeleteConfirm") + " " + item.Name + "?");
                if (ConfirmViewModel.CanBe)
                {
                    repository.DeleteItem(item.Id);
                    items.Remove(item);
                    windowService.OpenAlertWindow((string)Application.Current.TryFindResource("Deleted") + " " + item.Name + "!");
                }
            }
        }

        private bool CanExecuteDelete(object parameter)
        {
            return parameter is ItemModel;
        }

        private void ExecuteEditing(object parameter)
        {
            if (parameter is ItemModel item)
            {
                //TODO
            }
        }

        private bool CanExecuteEdit(object parameter)
        {
            return parameter is ItemModel;
        }
    }
}
