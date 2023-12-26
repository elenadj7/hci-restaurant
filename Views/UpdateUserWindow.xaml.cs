using hci_restaurant.Models;
using hci_restaurant.ViewModels;
using System.Windows;

namespace hci_restaurant.Views
{
    /// <summary>
    /// Interaction logic for UpdateUserWindow.xaml
    /// </summary>
    public partial class UpdateUserWindow : Window
    {
        public UpdateUserWindow()
        {
            InitializeComponent();
        }

        public UpdateUserWindow(UserModel user)
        {
            InitializeComponent();
            DataContext = new UpdateUserViewModel(user);
        }
    }
}
