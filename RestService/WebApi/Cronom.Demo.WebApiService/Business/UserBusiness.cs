using Cronom.Demo.WebApiService.Helper;
using Cronom.Demo.WebApiService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cronom.Demo.WebApiService.Business
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

        public string Login(string email)
        {
            int index = Array.IndexOf(Util.Emails, email);
            if (index >= 0)
            {
                User u = Util.Users[index];
                string token = Util.RandomString();
                RestApplication.Cache.Add(token, u);
                return token;
            }

            throw new Exception(string.Format("Invalid user: {0}", email));
        }

        public bool IsValid(string token)
        {
            User u = RestApplication.Cache.Get<User>(token);
            if (u != null)
            {
                return true;
            }
            return false;
        }

    }
}