using hci_restaurant.Models;
using hci_restaurant.Models.Repositories;
using hci_restaurant.Repositories;
using hci_restaurant.Services;
using Prism.Events;
using System;
using WPF_LoginForm.ViewModels;
using System.Windows.Input;
using System.Windows;

namespace hci_restaurant.ViewModels
{
    public class UpdateTableViewModel : ViewModelBase
    {
        private string seats;
        private TableModel table;
        private IWindowService windowService = new WindowService();
        private ITableRepository tableRepository = new TableRepository();
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

        public ICommand UpdateCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public UpdateTableViewModel()
        {
            UpdateCommand = new ViewModelCommand(ExecuteUpdating);
            CancelCommand = new ViewModelCommand(ExecuteCaneling);
        }

        public UpdateTableViewModel(TableModel table)
        {
            this.table = table;
            UpdateCommand = new ViewModelCommand(ExecuteUpdating);
            CancelCommand = new ViewModelCommand(ExecuteCaneling);
        }

        public void ExecuteCaneling(object parameter)
        {
            windowService.Close(this);
        }

        public void ExecuteUpdating(object parameter)
        {
            if (!int.TryParse(Seats, out _))
            {
                windowService.OpenIncorrectAlertWindow((string)Application.Current.TryFindResource("AlertNewTable"));
                return;
            }


            tableRepository.UpdateTable(table.Id, int.Parse(seats));
            eventAggregator.GetEvent<PubSubEvent<Tuple<int, int>>>().Publish(Tuple.Create(table.Id, int.Parse(seats)));
            windowService.OpenAlertWindow((string)Application.Current.TryFindResource("UpdatedTable"));
        }
    }
}
