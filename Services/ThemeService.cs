using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace hci_restaurant.Services
{
    public class ThemeService
    {
        private static string currentTheme = "Dark";
        public static string CurrentTheme
        {
            get { return currentTheme; }
            set
            {
                if(currentTheme != value)
                {
                    currentTheme = value;
                    SwitchTheme();
                }
            }
        }

        private static void ChangeThemeToDark()
        {
            ChangeTheme(new Uri("../Themes/DarkTheme.xaml", UriKind.Relative));
            CurrentTheme = "Dark";
        }

        private static void ChangeThemeToLight()
        {
            ChangeTheme(new Uri("../Themes/LightTheme.xaml", UriKind.Relative));
            CurrentTheme = "Light";
        }

        private static void ChangeThemeToProfessional()
        {
            ChangeTheme(new Uri("../Themes/ProfessionalTheme.xaml", UriKind.Relative));
            CurrentTheme = "Professional";
        }

        private static void ChangeTheme(Uri themeUri)
        {
            ResourceDictionary newTheme = new ResourceDictionary { Source = themeUri };

            ResourceDictionary languageResource = null;
            foreach (var mergedDictionary in App.Current.Resources.MergedDictionaries)
            {
                if (mergedDictionary.Source != null && mergedDictionary.Source.OriginalString.Contains("Languages/"))
                {
                    languageResource = mergedDictionary;
                    break;
                }
            }

            App.Current.Resources.MergedDictionaries.Clear();
            if (languageResource != null)
            {
                App.Current.Resources.MergedDictionaries.Add(languageResource);
            }
            App.Current.Resources.MergedDictionaries.Add(newTheme);
        }


        private static void SwitchTheme()
        {
            switch (currentTheme)
            {
                case "Light":
                    ChangeThemeToLight();
                    break;
                case "Dark":
                    ChangeThemeToDark();
                    break;
                case "Professional":
                    ChangeThemeToProfessional();
                    break;
            }
        }

        public static int GetIntFromTheme()
        {
            switch(currentTheme)
            {
                case "Light":
                    return 0;
                case "Dark":
                    return 1;
                case "Professional":
                    return 2;
            }

            return 0;
        }

        public static string GetStringFromTheme(int i)
        {
            switch (i)
            {
                case 1:
                    return "Light";
                case 2:
                    return "Dark";
                case 3:
                    return "Professional";
            }

            return "Dark";
        }
    }
}
