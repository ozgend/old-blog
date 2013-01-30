using Newtonsoft.Json;
using System;

namespace Cronom.Demo.HttpHandlerCore
{
    public class RequestHelper
    {
        public static string InvokeAndSerialize(string command, string payload) {
           return JsonConvert.SerializeObject(Invoker.Instance.InvokeMethod(command, payload));
        }

        public static string Error(Exception ex)
        {
            object err = new { Ok = false, ex.Message };
            return JsonConvert.SerializeObject(err);
        }
    }
}
