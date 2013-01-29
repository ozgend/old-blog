using Cronom.Demo.HttpHandlerCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;

namespace Cronom.Demo.HttpHandlerCore
{
    internal class MobileActions
    {


        public object Login(string payload)
        {
            try
            {
                string email = payload.ToModel<User>().Email;
                User user = Utils.Login(email);
                return new { Ok = false, User = user };
            }
            catch (Exception ex)
            {
                return new { Ok = false, Message = ex.Message };
            }

        }

        public object Search(string payload)
        {
            try
            {
                Search search = payload.ToModel<Search>();
                User user = Utils.SearchUser(search);
                return new { Ok = true, User = user };
            }
            catch (Exception ex)
            {
                return new { Ok = false, Message = ex.Message };
            }
        }

        public object List(string payload)
        {
            try
            {
                return new { Ok = true, UserList = Utils.Users };
            }
            catch (Exception ex)
            {
                return new { Ok = false, Message = ex.Message };
            }
        }

    }
}
