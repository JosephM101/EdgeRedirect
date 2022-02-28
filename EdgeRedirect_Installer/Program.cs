using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EdgeRedirect_Installer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            WriteLine("----- EdgeRedirect Installer -----", ConsoleColor.Green, false);
            WriteLine("----------------------------------", ConsoleColor.Green, true);
            if (EdgeRedirect.Config.EdgeInstallationPath == null)
            {
                Console.WriteLine("Hmm... It looks like Edge isn't installed on your PC. Therefore, there's nothing to do! Press Enter to exit.");
                Console.ReadLine();
                Exit(0);
            }

            Console.Write("Checking if EdgeRedirect is already installed... ");
            Console.WriteLine();
            if (!AskQuestion("Do you want to install EdgeRedirect?", false, DefaultOption.Yes))
            {
                Exit(0);
            }
            bool uninstallEdge = AskQuestion("Do you want to uninstall Edge entirely?", false, DefaultOption.No);
            Thread.Sleep(5000);
        }

        static void Exit(int code)
        {
            Environment.Exit(code);
        }

        static void Write(string text, ConsoleColor textColor, bool resetColor = true)
        {
            Console.ForegroundColor = textColor;
            Console.Write(text);
            if (resetColor) Console.ResetColor();
        }

        static void WriteLine(string text, ConsoleColor textColor, bool resetColor = true)
        {
            Write(text + "\r\n", textColor, resetColor);
        }

        // private static void AskQuestion(string prompt, bool hasDefaultOption, bool defaultOption = true)
        // {
        //     Console.Write(String.Format("{0} [{1}]: ", prompt, hasDefaultOption ? "y/n" : (defaultOption ? "Y/n" : "y/N")));
        //     Console.WriteLine(Console.ReadLine());
        // }

        private static bool AskQuestion(string prompt, bool newLine, DefaultOption defaultOption)
        {
            string options;
            switch (defaultOption)
            {
                case DefaultOption.None:
                    options = "y/n";
                    break;
                case DefaultOption.Yes:
                    options = "Y/n";
                    break;
                case DefaultOption.No:
                    options = "y/N";
                    break;
                default:
                    options = "y/n";
                    break;
            }

            if (newLine)
                Console.WriteLine("");

            Console.Write(String.Format("{0} [{1}]: ", prompt, options));

            string answer = Console.ReadLine();

            if (answer == "")
            {
                switch (defaultOption)
                {
                    case DefaultOption.Yes:
                        return true;
                    case DefaultOption.No:
                        return false;
                    default:
                        WriteLine("Invalid input.", ConsoleColor.Red, true);
                        return AskQuestion(prompt, false, defaultOption);
                }
            }
            else
            {
                if (answer.ToLower() == "y") return true;
                else if (answer.ToLower() == "n") return false;
                else
                {
                    WriteLine("Invalid input.", ConsoleColor.Red, true);
                    return AskQuestion(prompt, false, defaultOption);
                }
            }
        }

        enum DefaultOption
        {
            None,
            Yes,
            No
        }
    }
}