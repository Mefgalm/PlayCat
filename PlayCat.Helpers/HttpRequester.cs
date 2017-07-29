using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PlayCat.Helpers
{
    public class Headers
    {
        public int ContentLenght { get; set; }
    }

    public static class HttpRequester
    {
        public static Headers GetHeaders(string url)
        {
            if (url is null)
                throw new ArgumentNullException(nameof(url));

            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            var webResponse = (HttpWebResponse)webRequest.GetResponse();
            
            webResponse.Close();

            return new Headers()
            {
                ContentLenght = int.Parse(webResponse.Headers["Content-Length"]),
            };
        }
    }
}
 