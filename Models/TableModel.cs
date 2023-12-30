using System.ComponentModel;
using System.Windows;

namespace hci_restaurant.Models
{
    public class TableModel : INotifyPropertyChanged
    {
        private int seats;
        public int Id { get; set; }

        public int Seats
        {
            get { return seats; }
            set
            {
                seats = value;
                OnPropertyChanged(nameof(Seats));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return Id.ToString() + " - " + Seats.ToString() + " " + (string)Application.Current.TryFindResource("seats");
        }
    }
}
