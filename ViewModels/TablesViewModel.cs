using hci_restaurant.Models;
using hci_restaurant.Models.Repositories;
using hci_restaurant.Repositories;
using hci_restaurant.Services;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WPF_LoginForm.ViewModels;

namespace hci_restaurant.ViewModels
{
    public class TablesViewModel : ViewModelBase
    {
        private string filter;
        private ObservableCollection<TableModel> tables;
        private readonly ITableRepository tableRepository = new TableRepository();
        private readonly IWindowService windowService = new WindowService();
        private readonly PubSubEvent<TableModel> addedTable = App.EventAggregator.GetEvent<PubSubEvent<TableModel>>();
        private readonly PubSubEvent<Tuple<int, int>> updatedTable = App.EventAggregator.GetEvent<PubSubEvent<Tuple<int, int>>>();

        public ObservableCollection<TableModel> Tables
        {
            get { return tables; }
            set
            {
                tables = value;
                OnPropertyChanged(nameof(Tables));
            }
        }

        public string Filter
        {
            get { return filter; }
            set
            {
                filter = value;
                OnPropertyChanged(nameof(Filter));
            }
        }

        public ICommand FilterCommand { get; set; }
        public ICommand EditTableCommand { get; set; }
        public ICommand DeleteTableCommand { get; set; }
        public ICommand AddTableCommand { get; set; }

        public TablesViewModel()
        {
            DeleteTableCommand = new ViewModelCommand(ExecuteDeleteTable, CanExecuteDeleteTable);
            FilterCommand = new ViewModelCommand(ExecuteFilter, CanExecuteFilter);
            AddTableCommand = new ViewModelCommand(ExecuteAddTable);
            EditTableCommand = new ViewModelCommand(ExecuteUpdateTable, CanExecuteUpdateTable);
            LoadTables();

            addedTable.Subscribe(OnAddedTable);
            updatedTable.Subscribe(OnUpdatedTable);
        }

        private void OnUpdatedTable(Tuple<int, int> table)
        {
            Tables.Where(t => t.Id == table.Item1).FirstOrDefault().Seats = table.Item2;
        }

        private void OnAddedTable(TableModel table)
        {
            Tables.Add(table);
        }

        private void LoadTables()
        {
            Tables = tableRepository.GetAll();
        }

        private void ExecuteDeleteTable(object parameter)
        {
            if(parameter is TableModel table)
            {
                windowService.OpenConfirmWindow((string)Application.Current.TryFindResource("DeleteConfirm") + " " + table.Id + "?");
                if (ConfirmViewModel.CanBe)
                {
                    tableRepository.DeleteTable(table.Id);
                    Tables.Remove(table);
                    ConfirmViewModel.CanBe = false;
                    windowService.OpenAlertWindow((string)Application.Current.TryFindResource("Deleted") + " " + table.Id + "!");
                }
            }
        }

        private bool CanExecuteDeleteTable(object parameter)
        {
            return parameter is TableModel;
        }

        private void ExecuteFilter(object parameter)
        {
            int seats;
            if(int.TryParse(Filter, out seats))
            {
                Tables = tableRepository.GetAllByFilter(seats);
                return;
            }

            Tables = tableRepository.GetAll();
        }

        private bool CanExecuteFilter(object parameter)
        {
            return int.TryParse(Filter, out _) || string.IsNullOrWhiteSpace(Filter);
        }

        private void ExecuteAddTable(object parameter)
        {
            windowService.OpenAddNewTableWindow();
        }

        private void ExecuteUpdateTable(object parameter)
        {
            if(parameter is  TableModel table)
            {
                windowService.OpenUpdateTableWindow(table);
            }
        }

        private bool CanExecuteUpdateTable(object parameter)
        {
            return parameter is TableModel;
        }
    }
}
