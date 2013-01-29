using System.Web;
using System.Web.Mvc;

namespace Cronom.Demo.WebApiServiceNoToken
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}