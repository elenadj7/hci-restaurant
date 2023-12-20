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
using System.Windows.Controls;
using System.Windows.Input;
using WPF_LoginForm.ViewModels;

namespace hci_restaurant.ViewModels
{
    class AddNewItemViewModel : ViewModelBase
    {
        private string name;
        private string description;
        private string price;
        private bool available;
        private ObservableCollection<CategoryModel> categories;
        private CategoryModel selectedCategory;
        private readonly ICategoryRepository categoryRepository = new CategoryRepository();
        private readonly IWindowService windowService = new WindowService();
        private readonly IItemRepository itemRepository = new ItemRepository();

        public CategoryModel SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public string Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged(nameof(Price));
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

        public bool IsAvailable
        {
            get { return available; }
            set
            {
                available = value;
                OnPropertyChanged(nameof(IsAvailable));
            }
        }

        public ICommand CancelCommand { get; set; }
        public ICommand AddCommand { get; set; }

        public AddNewItemViewModel()
        {
            Categories = categoryRepository.GetAll();
            CancelCommand = new ViewModelCommand(ExecuteCancel);
            AddCommand = new ViewModelCommand(ExecuteAddNewItem);
            SelectedCategory = Categories.ElementAt(0);
        }

        private void ExecuteCancel(object parameter)
        {
            windowService.Close(this);
        }

        private void ExecuteAddNewItem(object parameter)
        {
            ItemModel item = new()
            {
                Name = Name,
                Price = decimal.Parse(Price),
                Description = Description,
                Available = 0,
                Category = SelectedCategory.Name
            };

            if(IsAvailable)
            {
                item.Available = 1;
            }

            itemRepository.AddItem(item, SelectedCategory.Id);
            windowService.OpenAlertWindow((string)Application.Current.TryFindResource("AddedItem"));
        }
    }
}
