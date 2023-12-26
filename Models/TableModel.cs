using System.Windows;

namespace hci_restaurant.Models
{
    public class TableModel
    {
        public int Id { get; set; }
        public int Seats { get; set; }

        public override string ToString()
        {
            return Id.ToString() + " - " + Seats.ToString() + " " + (string)Application.Current.TryFindResource("seats");
        }
    }
}
