using System;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace Xamarin.Demos
{
    internal class ThemeHelper
    {
        /// <summary>
        /// Selects the light or dark themes.
        /// </summary>
        /// <param name="theme">The id of the theme to be selected, 0 is light and 1 is dark.</param>
        internal static void SelectTheme(int theme)
        {
            try
            {
                #region Validation

                //If none selected, fallback to Light mode.
                if (theme < 0 || theme > 1)
                    theme = 0;

                #endregion

                //Copy all MergedDictionarys into a auxiliar list.
                var dictionaryList = Application.Current.Resources.MergedDictionaries.ToList();

                #region Selected Theme

                //Search for the specified culture.
                var requestedCulture = $"Resources/{ThemeIdToName(theme)}.xaml";
                var requestedResource = dictionaryList.FirstOrDefault(d => d.Source?.OriginalString == requestedCulture);

                #endregion

                //If we have the requested resource, remove it from the list and place at the end.
                //Then this theme will be our current style table.
                Application.Current.Resources.MergedDictionaries.Remove(requestedResource);
                Application.Current.Resources.MergedDictionaries.Add(requestedResource);

                GC.Collect(0);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        internal static string ThemeIdToName(int id)
        {
            switch (id)
            {
                case 0:
                    return "Light";
                case 1:
                    return "Dark";
                default:
                    return null;
            }
        }
    }
}