using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EdgeRedirect
{
    public static class Defs
    {
        public enum SearchEngine
        {
            // [Description("DuckDuckGo")]
            DuckDuckGo = 0,
            // [Description("Google")]
            Google = 1,
            // [Description("Yahoo!")]
            Yahoo = 2,
            // [Description("Bing")]
            Bing = 3,
        }

        public static Dictionary<Defs.SearchEngine, string> SearchEngines = new Dictionary<Defs.SearchEngine, string>()
        {
            {Defs.SearchEngine.DuckDuckGo, "DuckDuckGo"},
            {Defs.SearchEngine.Google, "Google"},
            {Defs.SearchEngine.Yahoo, "Yahoo!"},
            //{Defs.SearchEngine.Bing, "Bing"}
        };

        internal static Dictionary<string, string> UrlEncodeKeys = new Dictionary<string, string>()
        {
            // Values for encoding raw search queries into URL-friendly strings
            {"+", "%2B"},
            {"/", "%2F"},
            {"\\", "%5C"},
            {":", "%3A"},
            {"=", "%3D"},
            {"?", "%3F"},
            {"#", "%23"},
            {"%", "%25"},
            {"&", "%26"},
            {";", "%3B"},
            {"^", "%5E"},
            {"!", "%21"},
            {" ", "%20"},
            {"\"", "%22"},
            {"<", "%3C"},
            {">", "%3E"},
            {"{", "%7B"},
            {"}", "%7D"},
            {"|", "%7C"},
            {"~", "%7E"},
            {"[", "%5B"},
            {"]", "%5D"}
        };

        internal static Dictionary<string, string> UrlKeys = new Dictionary<string, string>()
        {
            // Values for decoding encoded URL characters into their human-readable equivelants.
            {"%2B", "+"},
            {"%2F", "/"},
            {"%5C", "\\"},
            {"%3A", ":"},
            {"%3D", "="},
            {"%3F", "?"},
            {"%23", "#"},
            {"%25", "%"},
            {"%26", "&"},
            {"%3B", ";"},
            {"%5E", "^"},
            {"%21", "!"},
            {"%20", " "},
            {"%22", "\""},
            {"%3C", "<"},
            {"%3E", ">"},
            {"%7B", "{"},
            {"%7D", "}"},
            {"%7C", "|"},
            {"%7E", "~"},
            {"%5B", "["},
            {"%5D", "]"}
        };
    }

    internal static class UrlDecoder
    {
        internal static string DecodeUrl(string raw)
        {
            // Convert a URL containing encoded characters into their respective human-readable characters
            // For example: %2B is converted to +

            string decoded = "";
            for (int i = 0; i < raw.Length; i++)
            {
                if (raw[i] == '%')
                {
                    string key = raw.Substring(i, 3);
                    if (Defs.UrlKeys.ContainsKey(key))
                    {
                        decoded += Defs.UrlKeys[key];
                        i += 2;
                    }
                    else
                    {
                        decoded += raw[i];
                    }
                }
                else
                {
                    decoded += raw[i];
                }
            }

            return decoded;
        }
    }

    internal static class UrlEncoder
    {
        // Convert search query to a search URL using the specified search engine
        internal static string EncodeSearchQuery(string searchQuery, Defs.SearchEngine searchEngine)
        {
            // Convert unsupported URL characters to their URL-encoded equivalents
            string formattedQuery = "";
            for (int i = 0; i < searchQuery.Length; i++)
            {
                // Use UrlEncodeKeys to convert unsupported characters to their URL-encoded equivalents
                if (Defs.UrlEncodeKeys.ContainsKey(searchQuery[i].ToString()))
                {
                    formattedQuery += Defs.UrlEncodeKeys[searchQuery[i].ToString()];
                }
                else
                {
                    formattedQuery += searchQuery[i];
                }
            }

            string encoded = "";
            switch (searchEngine)
            {
                case Defs.SearchEngine.DuckDuckGo:
                    encoded = "https://duckduckgo.com/?q=" + formattedQuery;
                    break;
                case Defs.SearchEngine.Bing:
                    encoded = "https://www.bing.com/search?q=" + formattedQuery;
                    break;
                case Defs.SearchEngine.Google:
                    encoded = "https://www.google.com/search?q=" + formattedQuery;
                    break;
                case Defs.SearchEngine.Yahoo:
                    encoded = "https://search.yahoo.com/search?p=" + formattedQuery;
                    break;
                default:
                    throw new ArgumentException("The specified search engine was not valid.");
            }
            return encoded;
        }
    }
}
