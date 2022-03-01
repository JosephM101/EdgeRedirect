using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UrlTools;

namespace EdgeRedirect
{
    public static class Config
    {
        private static string ms_root = @"C:\Program Files (x86)\Microsoft\";
        public static string EdgeInstallationDir = ms_root + @"Edge\Application\";

        public static string OriginalEdgeFilename = "msedge.exe";
        public static string RenamedEdgeFilename = "_msedge.exe";

        public static string EdgeInstallationPath = EdgeInstallationDir + OriginalEdgeFilename;
        public static string EdgeInstallationPath_Renamed = EdgeInstallationDir + RenamedEdgeFilename;

        public static string[] EdgeAppDirs =
        {
            ms_root + "EdgeCore",
            ms_root + "EdgeUpdate",
            ms_root + "EdgeWebView"
        };
        public static string[] EdgeAppDirs_Moved =
        {
            ms_root + "_EdgeCore",
            ms_root + "_EdgeUpdate",
            ms_root + "_EdgeWebView"
        };

        public static string ConfigFilename = "config.json";
        public static string ConfigPath = EdgeInstallationDir + ConfigFilename;

        public static string ConfigGui_ExeName = "edgeredirect_config.exe";
        // public static string AppName = "EdgeRedirect";
        public static string AppName = AssemblyProduct;

        #region Assembly Info
        public static string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public static string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public static string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public static string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public static string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }
        #endregion
    }

    public class EdgeRedirectConfigModel
    {
        public class Root
        {
            public string browser_path { get; set; }
            public Search search_config = new Search();
        }
        public class Search
        {
            public bool redirect_searches = false;
            public Defs.SearchEngine search_engine { get; set; }
        }
    }

    public static class EdgeRedirectConfig
    {
        public static EdgeRedirectConfigModel.Root LoadConfig()
        {
            return JsonConvert.DeserializeObject<EdgeRedirectConfigModel.Root>(File.ReadAllText(Config.ConfigPath));
        }

        public static bool TryLoadConfig(out EdgeRedirectConfigModel.Root config, out Exception err, bool returnNewOnError = false)
        {
            try
            {
                config = JsonConvert.DeserializeObject<EdgeRedirectConfigModel.Root>(File.ReadAllText(Config.ConfigPath));
                err = null;
                return true;
            }
            catch (Exception ex)
            {
                if (returnNewOnError)
                {
                    config = new EdgeRedirectConfigModel.Root();
                }
                else
                {
                    config = null;
                }
                err = ex;
                return false;
            }
        }

        public static bool SaveConfig(EdgeRedirectConfigModel.Root config)
        {
            try
            {
                File.WriteAllText(Config.ConfigPath, JsonConvert.SerializeObject(config, Formatting.Indented), Encoding.UTF8);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}