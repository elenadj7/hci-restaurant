using System;

namespace hci_restaurant.Models
{
    public class ProcurementModel
    {
        public int Id { get; set; }
        public string UserUsername { get; set; }
        public DateTime Ordered {  get; set; }
        public override string ToString()
        {
            return Id.ToString() + "-" + Ordered;
        }
    }
}
