using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace UrlTools
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
            // [Description("Unknown")]
            Unknown = 4,
            // [Description("Use browser default")]
            Default = 5
        }

        public static Dictionary<Defs.SearchEngine, string> SearchEngines = new Dictionary<Defs.SearchEngine, string>()
        {
            {Defs.SearchEngine.DuckDuckGo, "DuckDuckGo"},
            {Defs.SearchEngine.Google, "Google"},
            {Defs.SearchEngine.Yahoo, "Yahoo!"},
            {Defs.SearchEngine.Default, "Use browser default"},
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

        internal static Dictionary<string, string> SearchQueryKeys = new Dictionary<string, string>()
        {
            // Values for converting a url-encoded search query into its original form (the way it was typed into the search engine)
            // For example, replace "+" with " "
            {"%2B", " "},
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
            {"%5D", "]"},

            // Also include keys for formatted URLs (ones that don't use the %xx encoding)
            {"+", " "},
            {"/", "/"},
            {"\\", "\\"},
            {":", ":"},
            {"=", "="},
            {"?", "?"},
            {"#", "#"},
            {"%", "%"},
            {"&", "&"},
            {";", ";"},
            {"^", "^"},
            {"!", "!"},
            {" ", " "},
            {"\"", "\""},
            {"<", "<"},
            {">", ">"},
            {"{", "{"},
            {"}", "}"},
            {"|", "|"},
            {"~", "~"},
            {"[", "["},
            {"]", "]"}
        };
    }

    public static class UrlDecoder
    {
        public static string GetDomain(this Uri uri)
        {
            return GetDomain(uri.OriginalString);
        }

        public static string GetDomain(string url)
        {
            // Get the domain of a URL
            string domain = "";
            if (url.Contains("http://"))
            {
                domain = url.Substring(7);
            }
            else if (url.Contains("https://"))
            {
                domain = url.Substring(8);
            }
            else
            {
                domain = url;
            }
            if (domain.Contains("/"))
            {
                domain = domain.Substring(0, domain.IndexOf("/"));
            }
            return domain;
        }

        public static string DecodeUrl(string raw)
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

        internal static string GetOriginalSearchQuery(string parsedSearchQuery)
        {
            // Get how the search query was originally typed in by the user
            string originalSearchQuery = "";
            for (int i = 0; i < parsedSearchQuery.Length; i++)
            {
                if (parsedSearchQuery[i] == '%')
                {
                    string key = parsedSearchQuery.Substring(i, 3);
                    if (Defs.SearchQueryKeys.ContainsKey(key))
                    {
                        originalSearchQuery += Defs.SearchQueryKeys[key];
                        i += 2;
                    }
                    else
                    {
                        originalSearchQuery += parsedSearchQuery[i];
                    }
                }
                else
                {
                    originalSearchQuery += parsedSearchQuery[i];
                }
            }
            return originalSearchQuery;
        }

        public static bool IsSearchQuery(string url)
        {
            // Check if the Bing URL is a search query
            string parsedUrl = DecodeUrl(url);
            if (parsedUrl.Contains("?"))
            {
                string[] urlParts = parsedUrl.Split('?');
                if (urlParts.Length > 1)
                {
                    string[] queryParts = urlParts[1].Split('&');
                    if (queryParts.Length > 1)
                    {
                        foreach (string queryPart in queryParts)
                        {
                            if (queryPart.Contains("q="))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
    }

    public class SearchQuery
    {
        public string DecodedQuery { get; internal set; } // The search query that was originally entered by the user
        public Uri OriginalQuery { get; internal set; }
        public Defs.SearchEngine SearchEngine { get; internal set; } // The search engine used

        private SearchQuery(string query, Defs.SearchEngine _SearchEngine, Uri _OriginalQuery)
        {
            DecodedQuery = query;
            SearchEngine = _SearchEngine;
            OriginalQuery = _OriginalQuery;
        }

        public static bool DecodeSearchQuery(string _url, out SearchQuery searchQuery)
        {
            Defs.SearchEngine searchEngine = Defs.SearchEngine.Unknown;

            //string url = HttpUtility.UrlDecode(_url);

            Uri searchQueryUrl = new Uri(_url);

            // Get the search engine used so we know what to look for:
            string domain = searchQueryUrl.GetDomain();
            if (domain.Contains("duckduckgo"))
            {
                searchEngine = Defs.SearchEngine.DuckDuckGo;
            }
            else if (domain.Contains("google"))
            {
                searchEngine = Defs.SearchEngine.Google;
            }
            else if (domain.Contains("bing"))
            {
                searchEngine = Defs.SearchEngine.Bing;
            }
            else if (domain.Contains("yahoo"))
            {
                searchEngine = Defs.SearchEngine.Yahoo;
            }

            if (searchEngine == Defs.SearchEngine.Unknown)
            {
                searchQuery = null;
                return false;
            }

            // Get the user-entered search query from the URL
            NameValueCollection query = HttpUtility.ParseQueryString(searchQueryUrl.Query);
            string rawQuery = null;

            switch (searchEngine)
            {
                case Defs.SearchEngine.DuckDuckGo:
                    rawQuery = query.Get("q");
                    break;
                case Defs.SearchEngine.Google:
                    rawQuery = query.Get("q");
                    break;
                case Defs.SearchEngine.Bing:
                    rawQuery = query.Get("q");
                    break;
                case Defs.SearchEngine.Yahoo:
                    rawQuery = query.Get("p");
                    break;
            }

            // Return constructed SearchQuery object
            searchQuery = new SearchQuery(rawQuery, searchEngine, searchQueryUrl);
            return true;
        }
    }

    public static class UrlEncoder
    {
        // Convert search query to a search URL using the specified search engine
        public static string EncodeSearchQuery(string searchQuery, Defs.SearchEngine searchEngine)
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
