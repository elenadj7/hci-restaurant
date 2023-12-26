using hci_restaurant.Models;
using hci_restaurant.ViewModels;
using System.Windows;

namespace hci_restaurant.Views
{
    /// <summary>
    /// Interaction logic for UpdateItemWindow.xaml
    /// </summary>
    public partial class UpdateItemWindow : Window
    {
        public UpdateItemWindow()
        {
            InitializeComponent();
        }

        public UpdateItemWindow(ItemModel item)
        {
            InitializeComponent();
            DataContext = new UpdateItemViewModel(item);
        }
    }
}
