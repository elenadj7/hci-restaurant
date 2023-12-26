using hci_restaurant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
