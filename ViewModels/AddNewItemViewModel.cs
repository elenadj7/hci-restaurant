using hci_restaurant.Models;
using hci_restaurant.Models.Repositories;
using hci_restaurant.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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
        private readonly ICategoryRepository categoryRepository = new CategoryRepository();

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

        public AddNewItemViewModel()
        {
            Categories = categoryRepository.GetAll();
        }
    }
}
