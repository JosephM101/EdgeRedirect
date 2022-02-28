/*
 * EdgeRedirect
 * Written by Joseph Mignone (Joseph .M 101)
 * Intercepts queries made to msedge.exe, and reroutes them to a browser and search engine of your choice.
 * 
 * Example query made to msedge.exe (search query from Windows Search/Cortana): microsoft-edge:?launchContext1=Microsoft.Windows.Search_cw5n1h2txyewy&url=https%3A%2F%2Fwww.bing.com%2Fsearch%3Fq%3Dduckduckgo%2Bsearch%2Bengine%26form%3DWSBEDG%26qs%3DLS%26cvid%3D0d031794184c46e2ba1b9150dcabb9c4%26pq%3Dduckduckgo%26cc%3DUS%26setlang%3Den-US%26nclid%3D3EFD95B504A17A976E6E53DC65FE1B0A%26ts%3D1645989532388%26nclidts%3D1645989532%26tsms%3D388%26wsso%3DModerate
 * 
 * Installation:
 * The project executable replaces the default msedge executable, renaming the original to _msedge.exe
 * 
 * Notes:
 * The queries to the browser (including searches made through Windows Search/Cortana) were found to have originated from the Shell Experience Host process. This may not always be the case; more experimentation needs to be done.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EdgeRedirect
{
    internal static class Program
    {
        static void StartConfigGUI()
        {
            string configGui_path = "edgeredirect_config.exe";
            if (File.Exists(configGui_path))
            {
                ProcessStartInfo psi = new ProcessStartInfo(configGui_path);
                psi.UseShellExecute = true;
                Process.Start(psi);
            }
        }

        static EdgeRedirectConfigModel.Root config;

        static void Main(string[] args)
        {
            if (!EdgeRedirectConfig.TryLoadConfig(out config))
            {
                MessageBox.Show("Could not load configuration. You may not have run the configuration tool yet.\r\n\r\nClick OK to open the configuration tool.", "EdgeRedirect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                StartConfigGUI();
                Environment.Exit(1);
            }

            // Check for arguments
            if (args.Length > 0)
            {
                if (args[0] == "info")
                {
                    // Print version information. Can be used for verification to check if the msedge.exe file is not the original Microsoft Edge executable.
                    string assemblyTitle = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyTitleAttribute>().Title;
                    string versionString = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                    Console.WriteLine(String.Format("{0} v{1}", assemblyTitle, versionString));
                }
                else
                {
                    string browser_exePath = config.browser_path;
                    if (File.Exists(browser_exePath))
                    {
                        switch (args[0])
                        {
                            // Arguments passed to Edge typically have "--single-argument" as the first argument. Everything in the second argument is what we need.
                            case "--single-argument":
                                string parsed = EdgeCommandParser.GetUrlFromArguments(args[1]);
                                Process process = new Process();
                                process.StartInfo.FileName = browser_exePath;
                                process.StartInfo.Arguments = parsed;
                                process.Start();
                                break;
                            default:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("ERROR: Unrecognized or invalid argument.");
                                Console.ResetColor();
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Could not find/open the specified browser. Please check your configuration.\r\n\r\nClick OK to open the configuration tool.", "EdgeRedirect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        StartConfigGUI();
                        Environment.Exit(1);
                    }
                }
            }
            else
            {
                // Assume the user wants to open Edge
                // Check if the renamed executable exists
            }
        }
    }
}