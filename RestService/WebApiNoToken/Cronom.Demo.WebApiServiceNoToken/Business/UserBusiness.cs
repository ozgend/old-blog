using Cronom.Demo.WebApiServiceNoToken.Helper;
using Cronom.Demo.WebApiServiceNoToken.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cronom.Demo.WebApiServiceNoToken.Business
{
    public class UserBusiness
    {

        public User SearchUser(Search search)
        {
            User user = null;

            if (search.Id > 0)
            {
                user = Util.Users.SingleOrDefault(u => u.Id == search.Id);
            }
            else if (!string.IsNullOrEmpty(search.Email))
            {
                user = Util.Users.SingleOrDefault(u => u.Email == search.Email);
            }
            else if (!string.IsNullOrEmpty(search.Name))
            {
                user = Util.Users.SingleOrDefault(u => u.Name == search.Name);
            }
            else {
                throw new Exception("No search parameter specified.");
            }

            if (user != null)
            {
                return user;
            }
            else
            {
                throw new Exception("Search yields no result.");
            }
        }


    }
}