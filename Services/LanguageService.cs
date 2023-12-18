using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace hci_restaurant.Services
{
    public class LanguageService
    {
        private static string currentLanguage = "English";
        public static string CurrentLanguage
        {
            get { return currentLanguage; }
            set
            {
                if (currentLanguage != value)
                {
                    currentLanguage = value;
                    SwitchLanguage();
                }
            }
        }

        private static void ChangeLanguageToEnglish()
        {
            ChangeTheme(new Uri("../Languages/EnglishLanguage.xaml", UriKind.Relative));
            CurrentLanguage = "English";
        }

        private static void ChangeLanguageToSerbian()
        {
            ChangeTheme(new Uri("../Languages/SerbianLanguage.xaml", UriKind.Relative));
            CurrentLanguage = "Serbian";
        }

        private static void ChangeLanguageToSerbianCyrillic()
        {
            ChangeTheme(new Uri("../Languages/SerbianCyrillicLanguage.xaml", UriKind.Relative));
            CurrentLanguage = "Serbian (Cyrillic)";
        }

        private static void ChangeTheme(Uri themeUri)
        {
            ResourceDictionary newLanguage = new() { Source = themeUri };

            ResourceDictionary themeResource = null;
            foreach (var mergedDictionary in App.Current.Resources.MergedDictionaries)
            {
                if (mergedDictionary.Source != null && mergedDictionary.Source.OriginalString.Contains("Themes/"))
                {
                    themeResource = mergedDictionary;
                    break;
                }
            }

            App.Current.Resources.MergedDictionaries.Clear();
            if (themeResource != null)
            {
                App.Current.Resources.MergedDictionaries.Add(themeResource);
            }
            App.Current.Resources.MergedDictionaries.Add(newLanguage);
        }


        private static void SwitchLanguage()
        {
            switch (currentLanguage)
            {
                case "English":
                    ChangeLanguageToEnglish();
                    break;
                case "Serbian":
                    ChangeLanguageToSerbian();
                    break;
                case "Serbian (Cyrillic)":
                    ChangeLanguageToSerbianCyrillic();
                    break;
            }
        }

        public static int GetIntFromLanguage()
        {
            switch (currentLanguage)
            {
                case "English":
                    return 0;
                case "Serbian":
                    return 1;
                case "Serbian (Cyrillic)":
                    return 2;
            }

            return 0;
        }

        public static string GetStringFromLanguage(int i)
        {
            switch (i)
            {
                case 0:
                    return "English";
                case 1:
                    return "Serbian";
                case 2:
                    return "Serbian (Cyrillic)";
            }

            return "English";
        }
    }
}
