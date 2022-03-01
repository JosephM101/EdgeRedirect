using EdgeRedirect;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

            if (FileVersionInfo.GetVersionInfo(Config.EdgeInstallationPath).ProductName.Contains("EdgeReflect"))
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

            }

            if (!AskQuestion("Do you want to install EdgeRedirect?", false, DefaultOption.Yes))
            {
                Exit(0);
            }
            bool uninstallEdge = AskQuestion("Do you want to uninstall Edge entirely?", false, DefaultOption.No);

            if (uninstallEdge)
                UninstallEdge();
        }

        #region Main Functions
        static void UninstallEdge()
        {
            Console.Write("Uninstalling Edge... ");
            try
            {
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
        #endregion

        #region Shortcut Functions

        static void Exit(int code)
        {
            Console.ResetColor();
            Console.WriteLine("Exiting...");
            Environment.Exit(code);
        }
        #endregion
    }
}