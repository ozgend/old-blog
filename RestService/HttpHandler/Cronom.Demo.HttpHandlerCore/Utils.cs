using Cronom.Demo.HttpHandlerCore.Models;
using System;
using System.Linq;

namespace Cronom.Demo.HttpHandlerCore
{
    public class Utils
    {
        public static string[] Emails = { "deniz.ozgen@cronom.com", "jane.doe@company.com", "foo.bar@foobar.com" };
        public static User[] Users = { 
            new User{DeviceId="7777777777", Email= "deniz.ozgen@cronom.com", Id = 77, Name="Deniz", Surname = "Özgen"},
            new User{DeviceId="1234567890", Email= "jane.doe@company.com", Id = 123, Name="Jane", Surname = "Doe"},
            new User{DeviceId="5555554444", Email= "foo.bar@foobar.com", Id = 554, Name="Foo", Surname = "Bar"}
        };


        public static User SearchUser(Search search)
        {
            User user;

            if (search.Id > 0)
            {
                user = Users.SingleOrDefault(u => u.Id == search.Id);
            }
            else if (!string.IsNullOrEmpty(search.Email))
            {
                user = Users.SingleOrDefault(u => u.Email == search.Email);
            }
            else if (!string.IsNullOrEmpty(search.Name))
            {
                user = Users.SingleOrDefault(u => u.Name == search.Name);
            }
            else
            {
                throw new Exception("No search parameter specified.");
            }

            if (user != null)
            {
                return user;
            }
            throw new Exception("Search yields no result.");
        }


        public static User Login(string email) {
            var index = Array.IndexOf(Emails, email);
            if (index >= 0)
            {
                return Users[index];
            }
            throw new Exception("Invalid user.");
        }
    }
}
