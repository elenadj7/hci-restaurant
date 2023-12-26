using hci_restaurant.ViewModels;
using System.Windows;

namespace hci_restaurant.Views
{
    /// <summary>
    /// Interaction logic for ProcurementDetailsWindow.xaml
    /// </summary>
    public partial class ProcurementDetailsWindow : Window
    {
        public ProcurementDetailsWindow()
        {
            InitializeComponent();
        }

        public ProcurementDetailsWindow(string username, int procurementId)
        {
            InitializeComponent();
            DataContext = new ProcurementDetailsViewModel(procurementId, username);
        }
    }
}
