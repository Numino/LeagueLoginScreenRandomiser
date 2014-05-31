using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueLoginScreenRandomer
{
    public static class ConfigFile
    {
        public static string InstallPath { get; set; }
        public static string Mode { get; set; }
        public static string VersionNumberOverride { get; set; }
        public static string ThemePropertiesPathOverride { get; set; }
        public static string ThemeFolderPathOverride { get; set; }
        public static bool OpenLeagueAfterwards { get; set; }

        public static void CreateNewFile(string installPath)
        {
            var configFileContents = new List<string>
            {
                "leagueInstallLocation=" +installPath,
                "mode=random",
                "overrideversionnumber=",
                "fullPath-theme.properties=",
                "fullPath-themeFolder=",
                "openLeagueAfterwards=1"
            };
            File.WriteAllLines("leagueconfig.txt", configFileContents);
        }

        public static bool Exists()
        {
            return File.Exists("leagueconfig.txt");
        }

        public static void Load()
        {
            using (var streamReader = new StreamReader("leagueconfig.txt"))
            {
                String line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var configLine = line.Split('=');
                    switch (configLine[0])
                    {
                        case ("leagueInstallLocation"):
                            if (!string.IsNullOrWhiteSpace(configLine[1]))
                            InstallPath = configLine[1];
                            break;
                        case ("mode"):
                            if (!string.IsNullOrWhiteSpace(configLine[1]))
                                Mode = configLine[1];
                            else
                                Mode = "random";
                            break;
                        case ("overrideversionnumber"):
                            if (!string.IsNullOrWhiteSpace(configLine[1]))
                                VersionNumberOverride = configLine[1];
                            break;
                        case ("fullPath-theme.properties"):
                            if (!string.IsNullOrWhiteSpace(configLine[1]))
                                ThemePropertiesPathOverride = configLine[1];
                            break;
                        case ("fullPath-themeFolder"):
                            if (!string.IsNullOrWhiteSpace(configLine[1]))
                                ThemeFolderPathOverride = configLine[1];
                            break;
                        case ("openLeagueAfterwards"):
                            if (!string.IsNullOrWhiteSpace(configLine[1]) && configLine[1].Equals("1"))
                                OpenLeagueAfterwards = true;
                            break;
                    }
                }
            }
        }
    }
}
