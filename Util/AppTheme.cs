using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace hci_restaurant.Util
{
    class AppTheme
    {
        public static void ChangeTheme(Uri themeuri)
        {
            ResourceDictionary Theme = new() { Source = themeuri };

            App.Current.Resources.Clear();
            App.Current.Resources.MergedDictionaries.Add(Theme);

        }
    }
}
