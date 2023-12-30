using hci_restaurant.Models;
using hci_restaurant.ViewModels;
using System.Windows;

namespace hci_restaurant.Views
{
    /// <summary>
    /// Interaction logic for UpdateTableWindow.xaml
    /// </summary>
    public partial class UpdateTableWindow : Window
    {
        public UpdateTableWindow()
        {
            InitializeComponent();
        }

        public UpdateTableWindow(TableModel table)
        {
            InitializeComponent();
            DataContext = new UpdateTableViewModel(table);
        }
    }
}
