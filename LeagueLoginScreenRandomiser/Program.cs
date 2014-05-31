using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace LeagueLoginScreenRandomer
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                var isFirstRun = !ConfigFile.Exists();
                if (isFirstRun)
                     Setup();
                try
                {
                    ConfigFile.Load();
                }
                catch (Exception)
                {
                    Console.WriteLine("Unable to read leagueconfig.txt, check the file is not open and try again, otherwise try deleting the file");
                    Console.WriteLine("Press any key to exit");
                    Console.ReadKey();
                    return;
                }

                var propertiesPath = PathResolver.GetPropertiesPath();
                var themeFoldersPath = PathResolver.GetThemesPath();

                BackupThemeFile(isFirstRun, propertiesPath);

                string themeToUse;
                if (ConfigFile.Mode.Equals("seq", StringComparison.OrdinalIgnoreCase))
                    themeToUse = Themes.GetSequentialTheme(propertiesPath, themeFoldersPath);
                else
                    themeToUse = Themes.GetRandomTheme(themeFoldersPath);

                ThemePropertiesUpdater.ChangeCurrentTheme(propertiesPath, themeToUse, isFirstRun);

                if (ConfigFile.OpenLeagueAfterwards)
                    Process.Start(PathResolver.ClientPath());
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("--------------------------------------------------------------------");
                Console.WriteLine("Error - make sure the client is closed and no config files are open");
                Console.WriteLine("If you have setup the randomiser before try deleting the leagueconfig.txt");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }

        private static void BackupThemeFile(bool isFirstRun, string propertiesPath)
        {
            if (!isFirstRun || File.Exists("BACKUPtheme.properties")) return;
            File.Copy(propertiesPath, "BACKUPtheme.properties");
            Console.WriteLine("Created backup 'BACKUPtheme.properties");
        }

        private static void Setup()
        {
            var currentLocation = Application.StartupPath;
            var folders =Directory.GetDirectories(currentLocation);
            foreach (var folder in folders)
            {
                if (folder.Contains("RADS"))
                {
                    ConfigFile.CreateNewFile(currentLocation);
                    return;
                }
            }

            Console.WriteLine("Please locate the League of Legends folder");
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("Usually found in the Riot Games folder, here are some common places:");
            Console.WriteLine("--C:\\Riot Games\\League of Legends");
            Console.WriteLine("--C:\\Program Files (x86)\\Riot Games\\League of Legends");
            Console.WriteLine("--C:\\Program Files\\Riot Games\\League of Legends");
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("The default login screen change mode is Random, you can make it Sequential(change in order) by changing the leagueconfig.txt file");
            Console.WriteLine("Just change mode=random to mode=seq");

            for (int i = 0; i < 5; i++)
            {
                var folderBrowserDialog = new FolderBrowserDialog();
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    var installPath = folderBrowserDialog.SelectedPath;
                    var directories = Directory.GetDirectories(installPath);
                    foreach (var folder in directories)
                    {
                        if (folder.Contains("RADS"))
                        {
                            ConfigFile.CreateNewFile(installPath);
                            return;
                        }
                    }
                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("--------------------------------------------------------------------");
                Console.WriteLine("Cannot find the league folders");
                Console.WriteLine("Please make sure you select the League of Legends folder where the game is installed");
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Still can't find folders try restarting the program");
            throw new Exception();
        }
    }
}