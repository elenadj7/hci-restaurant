using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hci_restaurant.Models
{
    public class ProcurementModel
    {
        public int Id { get; set; }
        public string UserUsername { get; set; }
        public short IsFinished { get; set; }
        public DateTime Ordered {  get; set; }
        public DateTime Arrived { get; set; }
        public override string ToString()
        {
            return Id.ToString() + "-" + Ordered;
        }
    }
}
