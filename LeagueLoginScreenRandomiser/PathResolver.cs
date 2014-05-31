using System;
using System.Collections.Generic;
using System.IO;

namespace LeagueLoginScreenRandomer
{
    public static class PathResolver
    {
        private const string _pathFirstHalf = @"\RADS\projects\lol_air_client\releases\";

        public static string GetPropertiesPath()
        {
            if (!string.IsNullOrEmpty(ConfigFile.ThemePropertiesPathOverride))
                return ConfigFile.ThemePropertiesPathOverride;

            var  clientVersion = GetLatestClientVersion();
            var configPathArray = new string[] { ConfigFile.InstallPath, _pathFirstHalf, clientVersion, @"\deploy\theme.properties" };
            return PathCombine(configPathArray);
        }

        public static string GetThemesPath()
        {
            var clientVersion = GetLatestClientVersion();
            var themePathArray = new string[] { ConfigFile.InstallPath, _pathFirstHalf, clientVersion, @"\deploy\mod\lgn\themes\" };
            return PathCombine(themePathArray);
        }

        private static string GetLatestClientVersion()
        {
            if (!string.IsNullOrEmpty(ConfigFile.VersionNumberOverride))
                return ConfigFile.VersionNumberOverride;

            var latestModifiedDate = new DateTime(1900, 1, 1);
            var latestFolderPath = "";
            foreach (var folder in Directory.GetDirectories(ConfigFile.InstallPath + _pathFirstHalf))
            {
                var folderInfo = new DirectoryInfo(folder);
                var created = folderInfo.LastWriteTime;

                if (created > latestModifiedDate)
                {
                    latestFolderPath = folder;
                    latestModifiedDate = created;
                }
            }
            return latestFolderPath.Replace(ConfigFile.InstallPath + _pathFirstHalf, "");
        }

        private static string PathCombine(IEnumerable<string> paths)
        {
            var path = "";
            foreach (var partialPath in paths)
            {
                path += partialPath;
            }
            return path;
        }

        public static string ClientPath()
        {
            var stringList = new string[] {ConfigFile.InstallPath, @"\lol.launcher.exe"};
            return PathCombine(stringList);
        }
    }
}
