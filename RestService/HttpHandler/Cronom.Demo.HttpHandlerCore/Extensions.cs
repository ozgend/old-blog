using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Web;

namespace Cronom.Demo.HttpHandlerCore
{
    public static class Extensions
    {
        public static T ToModel<T>(this string payload)
        {
            return JsonConvert.DeserializeObject<T>(payload);
        }

        public static string RequestMethod(this HttpRequest request)
        {
            return request.Path.Substring(1, request.Path.Length - 1).Split('/')[1];
        }

        public static string RequestPayload(this HttpRequest request)
        {
            var sb = new StringBuilder();
            Stream s = request.InputStream;
            var streamLength = Convert.ToInt32(s.Length);
            var streamArray = new byte[streamLength];

            s.Read(streamArray, 0, streamLength);

            for (var i = 0; i < streamLength; i++)
            {
                sb.Append(Convert.ToChar(streamArray[i]));
            }

            return sb.ToString();
        }
    }
}
