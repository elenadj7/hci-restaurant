using hci_restaurant.Models;
using hci_restaurant.Models.Repositories;
using hci_restaurant.Repositories;
using hci_restaurant.Services;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
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
        private readonly ICategoryRepository categoryRepository = new CategoryRepository();
        private readonly PubSubEvent<ItemModel> addedItem = App.EventAggregator.GetEvent<PubSubEvent<ItemModel>>();
        private readonly PubSubEvent<Tuple<int, int, decimal>> modifiedItem = App.EventAggregator.GetEvent<PubSubEvent<Tuple<int, int, decimal>>>();
        private ObservableCollection<CategoryModel> categories = new();
        private CategoryModel selectedCategory = new()
        {
            Id = -1,
            Name = "All"
        };


        public CategoryModel SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));

                if(selectedCategory.Id == -1)
                {
                    Items = repository.GetAll();
                }
                else
                {
                    Items = repository.GetAllByCategory(selectedCategory.Id);
                }
            }
        }

        public ObservableCollection<CategoryModel> Categories
        {
            get { return categories; }
            set
            {
                categories = value;
                OnPropertyChanged(nameof(Categories));
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

        public ICommand DeleteItemCommand { get; set; }
        public ICommand EditItemCommand { get; set; }
        public ICommand NewItemCommand { get; set; }

        public ItemsViewModel()
        {
            Items = LoadAllItems();
            DeleteItemCommand = new ViewModelCommand(ExecuteDeleting, CanExecuteDelete);
            EditItemCommand = new ViewModelCommand(ExecuteEditing, CanExecuteEdit);
            NewItemCommand = new ViewModelCommand(ExecuteAddingItem);
            Categories.Add(selectedCategory);
            ObservableCollection<CategoryModel> tmp = categoryRepository.GetAll();
            foreach (CategoryModel t in tmp)
            {
                Categories.Add(t);
            }

            addedItem.Subscribe(OnAddedItem);
            modifiedItem.Subscribe(OnModifiedItem);
        }

        private void OnModifiedItem(Tuple<int, int, decimal> item)
        {
            ItemModel i = Items.Where(i => i.Id == item.Item1).FirstOrDefault();
            i.Quantity = item.Item2;
            i.Price = item.Item3;
        }

        private void OnAddedItem(ItemModel item)
        {
            Items.Add(item);
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
                    ConfirmViewModel.CanBe = false;
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
                windowService.OpenUpdateItemWindow(item);
            }
        }

        private bool CanExecuteEdit(object parameter)
        {
            return parameter is ItemModel;
        }
    }
}
