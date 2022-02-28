using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EdgeRedirect
{
    public static class Config
    {
        public static string EdgeInstallationDir = @"C:\Program Files (x86)\Microsoft\Edge\Application\";
        public static string EdgeInstallationPath = EdgeInstallationDir + "msedge.exe";
        public static string RenamedEdgePath = EdgeInstallationDir + "_msedge.exe";
        public static string ConfigFilename = "config.json";
        public static string ConfigPath = EdgeInstallationDir + ConfigFilename;
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

        public static bool TryLoadConfig(out EdgeRedirectConfigModel.Root config, bool returnNewOnError = false)
        {
            try
            {
                config = JsonConvert.DeserializeObject<EdgeRedirectConfigModel.Root>(File.ReadAllText(Config.ConfigPath));
                return true;
            }
            catch
            {
                if (returnNewOnError)
                {
                    config = new EdgeRedirectConfigModel.Root();
                }
                else
                {
                    config = null;
                }
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