using Cronom.Demo.HttpHandlerCore.Models;
using System;

namespace Cronom.Demo.HttpHandlerCore
{
    internal class MobileActions
    {
        public object Login(string payload)
        {
            try
            {
                var email = payload.ToModel<User>().Email;
                var user = Utils.Login(email);
                return new { Ok = false, User = user };
            }
            catch (Exception ex)
            {
                return new { Ok = false, ex.Message };
            }

        }

        public object Search(string payload)
        {
            try
            {
                var search = payload.ToModel<Search>();
                var user = Utils.SearchUser(search);
                return new { Ok = true, User = user };
            }
            catch (Exception ex)
            {
                return new { Ok = false, ex.Message };
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
                return new { Ok = false, ex.Message };
            }
        }

    }
}
