using EdgeRedirect;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static EdgeRedirect_Installer.Functions;

namespace EdgeRedirect_Installer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() > 0)
            {
                if (args[0] == "--fix-edge")
                {
                    if (!AskQuestion("Download and (re)install Microsoft Edge?", true, DefaultOption.Yes))
                        Exit(0);

                    RepairEdge();
                }
            }

            bool installed = false;

            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Green;
            WriteLine("----- EdgeRedirect Installer -----", ConsoleColor.Green, false);
            WriteLine("----------------------------------", ConsoleColor.Green);

            if (!File.Exists(Config.EdgeInstallationPath))
            {
                WriteLine("Hmm... It looks like Edge isn't installed on your PC.", ConsoleColor.Gray);
                Console.WriteLine("Nothing to do! Press Enter to exit.");
                Console.ReadLine();
                Exit(0);
            }

            Console.Write("Checking if EdgeRedirect is already installed... ");

            if (FileVersionInfo.GetVersionInfo(Config.EdgeInstallationPath).ProductName.Contains(Config.AppName))
            {
                installed = true;
                WriteLine("yes", ConsoleColor.Green);
            }
            else
            {
                WriteLine("no", ConsoleColor.Red);
            }

            if (installed)
            {
                if (!AskQuestion("EdgeRedirect is already installed! Would you like to uninstall it?", true, DefaultOption.No))
                    Exit(0);
                Uninstall();
            }

            if (!AskQuestion("Do you want to install EdgeRedirect?", false, DefaultOption.Yes))
                Exit(0);

            UninstallEdge(false);
            Install();
        }

        #region Main Functions
        static void Install()
        {
            try
            {
                Console.Write("Installing EdgeRedirect... ");
                // Get path of this executable
                string thisPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                File.Copy(Path.Combine(thisPath, Config.OriginalEdgeFilename), Config.EdgeInstallationPath, true);
                File.Copy(Path.Combine(thisPath, Config.ConfigGui_ExeName), Path.Combine(Config.EdgeInstallationDir, Config.ConfigGui_ExeName), true);

                WriteLine("done", ConsoleColor.Green);
                Console.WriteLine();
                WriteLine("EdgeRedirect is now installed!", ConsoleColor.Green);
                Console.Write("Press Enter to start the configurator.");
                Console.ReadLine();

                string configGui_path = Config.ConfigGui_ExeName;
                if (File.Exists(configGui_path))
                {
                    try
                    {
                        Process.Start(configGui_path).WaitForExit();
                    }
                    catch { }
                }

                if (AskQuestion("To ensure everything works properly, it is recommended that you restart your computer.\r\nWould you like to restart now?", false, DefaultOption.Yes))
                {
                    Process.Start("cmd", "/C shutdown /r /t 0");
                }
            }
            catch (Exception ex)
            {
                WriteLine("failed", ConsoleColor.Red);
                Failed(ex);
            }
        }

        static void DeleteFile(string file)
        {
            if (File.Exists(file))
                File.Delete(file);
        }

        static void Uninstall()
        {
            try
            {
                Console.Write("Uninstalling EdgeRedirect... ");

                // Delete EdgeRedirect executable
                DeleteFile(Path.Combine(Config.EdgeInstallationDir, Config.OriginalEdgeFilename));

                // Delete configurator
                DeleteFile(Path.Combine(Config.EdgeInstallationDir, Config.ConfigGui_ExeName));

                // Delete config file
                DeleteFile(Config.ConfigPath);

                // Put Edge back in its place
                if (File.Exists(Path.Combine(Config.EdgeInstallationDir, Config.RenamedEdgeFilename)))
                    File.Move(Path.Combine(Config.EdgeInstallationDir, Config.RenamedEdgeFilename), Path.Combine(Config.EdgeInstallationDir, Config.OriginalEdgeFilename));

                // Rename all of the Edge directories to their original names
                for (int i = 0; i < Config.EdgeAppDirs_Moved.Length; i++)
                    if (Directory.Exists(Config.EdgeAppDirs_Moved[i]))
                        Directory.Move(Config.EdgeAppDirs_Moved[i], Config.EdgeAppDirs[i]);

                WriteLine("done", ConsoleColor.Green);
                Console.WriteLine();
                WriteLine("EdgeRedirect has been uninstalled.", ConsoleColor.Green);
                Console.Write("Press Enter to exit.");
                Console.ReadLine();
                Exit(0);
            }
            catch (Exception ex)
            {
                WriteLine("failed", ConsoleColor.Red);
                Failed(ex);
            }
        }

        static void UninstallEdge(bool completeUninstall)
        {
            try
            {
                Console.Write("Killing all Edge processes... ");
                // Kill processes
                WriteLine("done", ConsoleColor.Green);

                Console.Write("Renaming files/folders... ");
                for (int i = 0; i < Config.EdgeAppDirs.Length; i++)
                    if (Directory.Exists(Config.EdgeAppDirs[i]))
                        Directory.Move(Config.EdgeAppDirs[i], Config.EdgeAppDirs_Moved[i]);

                if (File.Exists(Path.Combine(Config.EdgeInstallationDir, Config.OriginalEdgeFilename)))
                {
                    if (File.Exists(Path.Combine(Config.EdgeInstallationDir, Config.RenamedEdgeFilename)))
                        File.Delete(Path.Combine(Config.EdgeInstallationDir, Config.RenamedEdgeFilename));
                    File.Move(Path.Combine(Config.EdgeInstallationDir, Config.OriginalEdgeFilename), Path.Combine(Config.EdgeInstallationDir, Config.RenamedEdgeFilename));
                }

                WriteLine("done", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                WriteLine("failed", ConsoleColor.Red);
                Console.Write("Uninstalling Edge ");
                Write("failed ", ConsoleColor.Red);
                Console.Write("with code ");
                Write(ex.HResult.ToString(), ConsoleColor.Red);
                Console.WriteLine(".");
                Write("Error message: ", ConsoleColor.Red);
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        private static void RepairEdge()
        {
            // Repair Edge by deleting the original folders, and downloading/installing a new copy
            Console.Write("Deleting old Microsoft Edge directories... ");
            try
            {
                List<string> dirs = new List<string>();
                dirs.AddRange(Config.EdgeAppDirs_Moved);
                foreach (string dir in dirs)
                    if (Directory.Exists(dir))
                        Directory.Delete(dir, true);

                WriteLine("done", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                WriteLine("failed", ConsoleColor.Red);
                Failed(ex);
            }

            Console.Write("Downloading Edge installer... ");

            // Download the latest version of Edge
            string requestUrl = "https://c2rsetup.officeapps.live.com/c2r/downloadEdge.aspx?platform=Default&source=EdgeStablePage&Channel=Stable&language=en";
            string userDownloadFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
            string downloadPath = userDownloadFolder + "\\MicrosoftEdgeSetup.exe";

            try
            {
                if (File.Exists(downloadPath))
                    File.Delete(downloadPath);

                using (WebClient client = new WebClient())
                    client.DownloadFile(requestUrl, downloadPath);

                WriteLine("done", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                WriteLine("failed", ConsoleColor.Red);
                Failed(ex);
            }

            Console.Write("Installing Edge... ");
            // Silently install
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo(downloadPath);
                //startInfo.Arguments = "/qb";
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;
                Process.Start(startInfo).WaitForExit();
                WriteLine("done", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                WriteLine("failed", ConsoleColor.Red);
                Failed(ex);
            }

            // WriteLine("Edge was reinstalled successfully.", ConsoleColor.Green);
            WriteLine("Press Enter to exit...", ConsoleColor.Green);
            Console.ReadLine();
            Exit(0);
        }
        #endregion
    }
}