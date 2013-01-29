using Cronom.Demo.WebApiService.Business;
using Cronom.Demo.WebApiService.Controllers.ActionFilters;
using Cronom.Demo.WebApiService.Helper;
using Cronom.Demo.WebApiService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cronom.Demo.WebApiService.Controllers
{
    public class MobileActionController : ApiController
    {

        private UserBusiness _userBusiness = new UserBusiness();

        public object Login(User user)
        {
            try
            {
                string token = _userBusiness.Login(user.Email);
                return new { Ok = true, Token = token };
            }
            catch (Exception ex)
            {
                return new { Ok = false, Message = ex.Message };
            }
        }

        [TokenRequiredAttribute]
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

        [TokenRequiredAttribute]
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
