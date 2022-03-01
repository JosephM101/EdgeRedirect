using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdgeRedirect_Installer
{
    internal static class Functions
    {
        internal static string GetCommandOutput(string exe, string args)
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = exe;
                process.StartInfo.Arguments = args;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();
                process.WaitForExit();
                return process.StandardOutput.ReadToEnd();
            }
            catch
            {
                return "";
            }
        }

        internal static bool AskQuestion(string prompt, bool newLine, DefaultOption defaultOption)
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

        internal enum DefaultOption
        {
            None,
            Yes,
            No
        }

        internal static void Write(string text, ConsoleColor textColor, bool resetColor = true)
        {
            Console.ForegroundColor = textColor;
            Console.Write(text);
            if (resetColor) Console.ResetColor();
        }

        internal static void WriteLine(string text, ConsoleColor textColor, bool resetColor = true)
        {
            Write(text + "\r\n", textColor, resetColor);
        }

        // private static void AskQuestion(string prompt, bool hasDefaultOption, bool defaultOption = true)
        // {
        //     Console.Write(String.Format("{0} [{1}]: ", prompt, hasDefaultOption ? "y/n" : (defaultOption ? "Y/n" : "y/N")));
        //     Console.WriteLine(Console.ReadLine());
        // }
    }
}
