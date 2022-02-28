using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdgeRedirect
{
    internal static class EdgeCommandParser
    {
        internal static string GetUrlFromArguments(string arguments)
        {
            /* Example argument:
             * --single-argument microsoft-edge:?launchContext1=Microsoft.Windows.Search_cw5n1h2txyewy&url=https%3A%2F%2Fwww.bing.com%2Fsearch%3Fq%3Dduckduckgo%2Bsearch%2Bengine%26form%3DWSBEDG%26qs%3DLS%26cvid%3D0d031794184c46e2ba1b9150dcabb9c4%26pq%3Dduckduckgo%26cc%3DUS%26setlang%3Den-US%26nclid%3D3EFD95B504A17A976E6E53DC65FE1B0A%26ts%3D1645989532388%26nclidts%3D1645989532%26tsms%3D388%26wsso%3DModerate
             * Parsed URL: https://www.bing.com/search?q=duckduckgo+search+engine&form=WSBEDG&qs=LS&cvid=0d031794184c46e2ba1b9150dcabb9c4&pq=duckduckgo&cc=US&setlang=en-US&nclid=3EFD95B504A17A976E6E53DC65FE1B0A&ts=1645989532388&wsso=Moderate
             */

            string rawUrl = "";
            string parsedUrl = "";
            string single_argument = arguments.Substring(arguments.IndexOf("--single-argument") + "--single-argument".Length);

            // Remove the "microsoft-edge:" prefix
            single_argument = single_argument.Substring(single_argument.IndexOf(":") + 1);

            // Remove the "?launchContext1=" prefix
            single_argument = single_argument.Substring(single_argument.IndexOf("?") + 1);

            // Remove the "url=" prefix
            rawUrl = single_argument.Substring(single_argument.IndexOf("&url=") + 5);

            // Parse the URL by converting the %xx hexadecimal characters to their equivalent character
            for (int i = 0; i < rawUrl.Length; i++)
            {
                if (rawUrl[i] == '%')
                {
                    parsedUrl += (char)Convert.ToInt32(rawUrl.Substring(i + 1, 2), 16);
                    i += 2;
                }
                else
                {
                    parsedUrl += rawUrl[i];
                }
            }

            return UrlDecoder.DecodeUrl(parsedUrl);
        }
    }
}