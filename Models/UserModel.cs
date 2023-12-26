using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hci_restaurant.Models
{
    public class UserModel : INotifyPropertyChanged
    {
        private int salary;
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname {  get; set; }
        public short Role {  get; set; }

        public int Salary
        {
            get {  return salary; }
            set
            {
                salary = value;
                OnPropertyChanged(nameof(Salary));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return Username;
        }
    }
}
