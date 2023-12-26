using hci_restaurant.ViewModels;
using System.Windows;

namespace hci_restaurant.Views
{
    /// <summary>
    /// Interaction logic for ConfirmWindow.xaml
    /// </summary>
    public partial class ConfirmWindow : Window
    {
        public ConfirmWindow()
        {
            InitializeComponent();
        }

        public ConfirmWindow(string message)
        {
            InitializeComponent();
            DataContext = new ConfirmViewModel(message);
        }
    }
}
