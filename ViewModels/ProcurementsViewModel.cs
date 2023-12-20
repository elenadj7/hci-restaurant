using hci_restaurant.Models;
using hci_restaurant.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_LoginForm.ViewModels;

namespace hci_restaurant.ViewModels
{
    public class ProcurementsViewModel : ViewModelBase
    {
        private ObservableCollection<ProcurementHasItemModel> items;
        private ObservableCollection<UserModel> users;
        private ObservableCollection<ProcurementModel> procurements;
        private UserModel selectedUser;
        private ProcurementModel selectedProcurement;

        public ObservableCollection<UserModel> Users
        {
            get { return users; }
            set
            {
                users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public ObservableCollection<ProcurementModel> Procurements
        {
            get { return procurements; }
            set
            {
                procurements = value;
                OnPropertyChanged(nameof(Procurements));
            }
        }

        public UserModel SelectedUser
        {
            get => selectedUser;
            set
            {
                selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

        public ProcurementModel SelectedProcurement
        {
            get => selectedProcurement;
            set
            {
                selectedProcurement = value;
                OnPropertyChanged(nameof(SelectedProcurement));
            }
        }

        public ObservableCollection<ProcurementHasItemModel> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public ProcurementsViewModel()
        {
            EditCommand = new ViewModelCommand(ExecuteEditing, CanExecuteEdit);
            DeleteCommand = new ViewModelCommand(ExecuteDeleting, CanExecuteDelete);
        }

        private void ExecuteDeleting(object parameter)
        {
            if (parameter is ProcurementHasItemModel item)
            {
                 //todo
            }
        }

        private bool CanExecuteDelete(object parameter)
        {
            return parameter is ProcurementHasItemModel;
        }

        private void ExecuteEditing(object parameter)
        {
            if (parameter is ProcurementHasItemModel item)
            {
                //TODO
            }
        }

        private bool CanExecuteEdit(object parameter)
        {
            return parameter is ProcurementHasItemModel;
        }
    }
}
