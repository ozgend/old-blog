using Cronom.Demo.WebApiService.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Cronom.Demo.WebApiService.Controllers.ActionFilters
{
    public class TokenRequiredAttribute : ActionFilterAttribute
    {

        private UserBusiness _userBusiness = new UserBusiness();

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string token = "";

            try
            {
                token = actionContext.Request.Headers.GetValues("Api-Token").First();
            }
            catch
            {
                actionContext.Response = CreateFailResponse();
                return;
            }
            
            bool isValid = _userBusiness.IsValid(token);
            if (isValid)
            {
                base.OnActionExecuting(actionContext);
            }
            else
            {
                actionContext.Response = CreateFailResponse();
                return;
            }
        }

        private HttpResponseMessage CreateFailResponse()
        {
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.Forbidden,
                Content = new ObjectContent(typeof(object),
                    new { Ok = false, Message = "Unauthorized." },
                    new JsonMediaTypeFormatter())
            };  
        }

    }
}