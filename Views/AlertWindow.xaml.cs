using hci_restaurant.ViewModels;
using System.Windows;

namespace hci_restaurant.Views
{
    /// <summary>
    /// Interaction logic for AlertWindow.xaml
    /// </summary>
    public partial class AlertWindow : Window
    {
        public AlertWindow()
        {
            InitializeComponent();
        }

        public AlertWindow(string message)
        {
            InitializeComponent();
            DataContext = new AlertViewModel(message);
        }

        public AlertWindow(string message, bool correct)
        {
            InitializeComponent();
            DataContext = new AlertViewModel(message, correct);
        }
    }
}
