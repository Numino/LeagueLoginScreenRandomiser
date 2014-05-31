using System.IO;

namespace LeagueLoginScreenRandomer
{
    public static class ThemePropertiesUpdater
    {
        public static void ChangeCurrentTheme(string propertiesPath, string themeToUse, bool isFirstRun)
        {
            var themeConfigLines = File.ReadAllLines(propertiesPath);

                for (var i = 0; i < themeConfigLines.Length; i++)
                {
                    var line = themeConfigLines[i];
                    if (line.Contains("themeConfig"))
                    {
                        var endofCurrentLoginScreen = line.IndexOf(',');
                        var endOfConfigString = line.Substring(endofCurrentLoginScreen);
                        var originalLoginScreen = "";
                        if (isFirstRun)
                        {
                            originalLoginScreen = "," + themeConfigLines[i].Split('=')[1].Split(',')[0];
                        }
                        themeConfigLines[i] = "themeConfig =" + themeToUse + originalLoginScreen + endOfConfigString;
                    }
                }
                File.WriteAllLines(propertiesPath, themeConfigLines);
        }

        public static string GetCurrentTheme(string propertiesPath)
        {
            var themeConfigLines = File.ReadAllLines(propertiesPath);
            {
                foreach (var line in themeConfigLines)
                {
                    if (line.Contains("themeConfig"))
                    {
                        return line.Split('=')[1].Split(',')[0];
                    }
                }
            }
            return "";
        }
    }
}
