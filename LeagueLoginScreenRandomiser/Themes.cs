using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LeagueLoginScreenRandomer
{
    public static class Themes
    {
        public static string GetRandomTheme(string themeFoldersPath)
        {
            var themeFolderList = ThemeFolderList(themeFoldersPath);
            var randomNumber = new Random().Next(0, themeFolderList.Count());
            return themeFolderList[randomNumber];
        }
        public static string GetSequentialTheme(string propertiesPath, string themeFoldersPath)
        {
            var currentTheme = ThemePropertiesUpdater.GetCurrentTheme(propertiesPath);
            if (currentTheme.Equals(""))
                return GetRandomTheme(propertiesPath);

            var themeFolderList = ThemeFolderList(themeFoldersPath);
            for (var i = 0; i < themeFolderList.Count; i++)
            {
                if (i + 1 >= themeFolderList.Count)
                    return themeFolderList[0];
                if (themeFolderList[i].Equals(currentTheme))
                    return themeFolderList[i+1];
            }

            return GetRandomTheme(propertiesPath);
        }

        private static IList<string> ThemeFolderList(string propertiesPath)
        {
            var themeFolderList = Directory.GetDirectories(propertiesPath);
            var themeFolders = new List<string>();
            foreach (var themeFolder in themeFolderList)
            {
                if (themeFolder.Contains("parchment"))
                    continue;
                var fileList = Directory.GetFiles(themeFolder);
                foreach (var file in fileList)
                {
                    if (file.Contains("login.swf"))
                        themeFolders.Add(themeFolder.Replace(propertiesPath, ""));
                }
            }
            return themeFolders;
        }
    }
}
