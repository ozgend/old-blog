using System;
using System.Web;
using System.Web.SessionState;

namespace Cronom.Demo.HttpHandlerCore
{
    public class PostHandler : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            string responseData = null;
            try
            {
                string command = context.Request.RequestMethod();
                string payload = context.Request.RequestPayload();
                responseData = RequestHelper.InvokeAndSerialize(command, payload);
            }
            catch (Exception ex)
            {
                responseData = RequestHelper.Error(ex);
            }
            finally
            {
                context.Response.ContentType = "application/json";
                if (responseData != null) context.Response.Write(responseData);
                context.Response.End();
            }
        }

    }
}
