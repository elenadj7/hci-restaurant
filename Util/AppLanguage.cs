using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace hci_restaurant.Util
{
    class AppLanguage
    {
        public static void ChangeLanguage(Uri languageuri)
        {
            ResourceDictionary language = new() { Source = languageuri };

            App.Current.Resources.Clear();
            App.Current.Resources.MergedDictionaries.Add(language);

        }
    }
}
