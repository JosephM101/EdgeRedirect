﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using UrlTools;

namespace EdgeRedirect_GUI
{
    public partial class MainForm : Form
    {
        WebBrowser[] WebBrowsers;
        string configPath = EdgeRedirect.Config.ConfigPath;
        EdgeRedirect.EdgeRedirectConfigModel.Root config;

        public bool cancel = false;

        public MainForm()
        {
            InitializeComponent();
        }

        void ForceBringToFront()
        {
            if (WindowState == FormWindowState.Minimized)
                WindowState = FormWindowState.Normal;
            else
            {
                TopMost = true;
                Focus();
                BringToFront();
                TopMost = false;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(configPath))
            {
                config = EdgeRedirect.EdgeRedirectConfig.LoadConfig();
            }
            else
            {
                config = new EdgeRedirect.EdgeRedirectConfigModel.Root();
            }

            WebBrowsers = GetAllWebBrowsers();
            comboBox_browsers.Items.AddRange(WebBrowsers);
            comboBox_browsers.SelectedItem = comboBox_browsers.Items.Cast<WebBrowser>().Where(x => x.ExecPath == config.browser_path).FirstOrDefault();

            checkBox_redirectSearchesSetting.Checked = config.search_config.redirect_searches;

            comboBox_redirectSearches_searchEngine.Items.AddRange(Defs.SearchEngines.Values.ToArray());
            string selectedEngine = "";
            Defs.SearchEngines.TryGetValue(config.search_config.search_engine, out selectedEngine);
            comboBox_redirectSearches_searchEngine.SelectedItem = selectedEngine;

            ForceBringToFront();
        }

        WebBrowser[] GetAllWebBrowsers()
        {
            RegistryKey browserKeys;
            List<WebBrowser> webBrowsers = new List<WebBrowser>();

            browserKeys = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Clients\StartMenuInternet");
            if (browserKeys == null)
                browserKeys = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Clients\StartMenuInternet");
            string[] browserNames = browserKeys.GetSubKeyNames();
            for (int i = 0; i < browserNames.Length; i++)
            {
                RegistryKey browserKey = browserKeys.OpenSubKey(browserNames[i]);
                RegistryKey browserKeyPath = browserKey.OpenSubKey(@"shell\open\command");
                webBrowsers.Add(new WebBrowser((string)browserKey.GetValue(null), browserKeyPath.GetValue(null).ToString().StripQuotes()));
            }
            return webBrowsers.ToArray();
        }

        private void comboBox_browsers_Format(object sender, ListControlConvertEventArgs e)
        {
            e.Value = ((WebBrowser)e.ListItem).BrowserName;
        }

        private void button_done_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox_browsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            string execPath = ((WebBrowser)comboBox_browsers.SelectedItem).ExecPath;
            config.browser_path = execPath;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!cancel)
            {
                e.Cancel = true;
                if (comboBox_browsers.SelectedIndex == -1)
                {
                    MessageBox.Show("You must select a browser to use.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    EdgeRedirect.EdgeRedirectConfig.SaveConfig(config);
                    e.Cancel = false;
                }
            }
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            cancel = true;
            Close();
        }

        private void comboBox_redirectSearches_searchEngine_SelectedIndexChanged(object sender, EventArgs e)
        {
            config.search_config.search_engine = Defs.SearchEngines.FirstOrDefault(x => x.Value == comboBox_redirectSearches_searchEngine.Text).Key;
        }

        private void checkBox_redirectSearchesSetting_CheckedChanged(object sender, EventArgs e)
        {
            config.search_config.redirect_searches = checkBox_redirectSearchesSetting.Checked;
        }
    }

    internal static class Extensions
    {
        internal static string StripQuotes(this string s)
        {
            if (s.EndsWith("\"") && s.StartsWith("\""))
            {
                return s.Substring(1, s.Length - 2);
            }
            else
            {
                return s;
            }
        }
    }

    public class WebBrowser
    {
        string _BrowserName = "";
        string _ExecPath = "";

        public WebBrowser(string BrowserName, string ExecPath)
        {
            _BrowserName = BrowserName;
            _ExecPath = ExecPath;
        }
        public string BrowserName
        {
            get
            {
                return _BrowserName;
            }
        }
        public string ExecPath
        {
            get
            {
                return _ExecPath;
            }
        }
        public override string ToString()
        {
            return _BrowserName;
        }
    }
}