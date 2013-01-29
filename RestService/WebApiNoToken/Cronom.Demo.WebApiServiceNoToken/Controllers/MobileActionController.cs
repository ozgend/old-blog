using Cronom.Demo.WebApiServiceNoToken.Business;
using Cronom.Demo.WebApiServiceNoToken.Helper;
using Cronom.Demo.WebApiServiceNoToken.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cronom.Demo.WebApiServiceNoToken.Controllers
{
    public class MobileActionController : ApiController
    {

        private UserBusiness _userBusiness = new UserBusiness();

        public object Search(Search search)
        {
            try
            {
                User user = _userBusiness.SearchUser(search);
                return new { Ok = true, User = user };
            }
            catch (Exception ex)
            {
                return new { Ok = false, Message = ex.Message };
            }
        }

        public object List()
        {
            try
            {
                return new { Ok = true, UserList = Util.Users };
            }
            catch (Exception ex)
            {
                return new { Ok = false, Message = ex.Message };
            }
        }

    }
}
