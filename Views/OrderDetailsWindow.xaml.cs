using hci_restaurant.ViewModels;
using System.Windows;

namespace hci_restaurant.Views
{
    /// <summary>
    /// Interaction logic for OrderDetailsWindow.xaml
    /// </summary>
    public partial class OrderDetailsWindow : Window
    {
        public OrderDetailsWindow()
        {
            InitializeComponent();
        }

        public OrderDetailsWindow(string username, int orderId)
        {
            InitializeComponent();
            DataContext = new OrderDetailsViewModel(orderId, username);
        }
    }
}
