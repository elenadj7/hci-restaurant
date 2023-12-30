using hci_restaurant.Models;
using hci_restaurant.Models.Repositories;
using hci_restaurant.Repositories;
using hci_restaurant.Services;
using Prism.Events;
using System.Windows;
using System.Windows.Input;
using WPF_LoginForm.ViewModels;

namespace hci_restaurant.ViewModels
{
    public class AddNewTableViewModel : ViewModelBase
    {
        private string seats;
        private readonly ITableRepository tableRepository = new TableRepository();
        private readonly IWindowService windowService = new WindowService();
        private readonly EventAggregator eventAggregator = (EventAggregator)App.EventAggregator;

        public string Seats
        {
            get { return seats; }
            set
            {
                seats = value;
                OnPropertyChanged(nameof(Seats));
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public AddNewTableViewModel()
        {
            AddCommand = new ViewModelCommand(ExecuteAddTable);
            CancelCommand = new ViewModelCommand(ExecuteCancel);
        }

        private void ExecuteAddTable(object parameter)
        {

            int s;
            if(!int.TryParse(Seats, out s))
            {
                windowService.OpenIncorrectAlertWindow((string)Application.Current.TryFindResource("AlertNewTable"));
                return;
            }

            int id = tableRepository.AddTable(s);
            TableModel table = new()
            {
                Id = id,
                Seats = s
            };

            eventAggregator.GetEvent<PubSubEvent<TableModel>>().Publish(table);
            windowService.OpenAlertWindow((string)Application.Current.TryFindResource("AddedTable"));
        }

        private void ExecuteCancel(object parameter)
        {
            windowService.Close(this);
        }
    }
}
