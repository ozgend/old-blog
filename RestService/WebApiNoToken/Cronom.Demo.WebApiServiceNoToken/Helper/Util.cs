using Cronom.Demo.WebApiServiceNoToken.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Cronom.Demo.WebApiServiceNoToken.Helper
{
    public class Util
    {

        public static string[] Emails = { "deniz.ozgen@cronom.com", "jane.doe@company.com", "foo.bar@foobar.com" };
        public static User[] Users = { 
            new User{DeviceId="7777777777", Email= "deniz.ozgen@cronom.com", Id = 77, Name="Deniz", Surname = "Özgen"},
            new User{DeviceId="1234567890", Email= "jane.doe@company.com", Id = 123, Name="Jane", Surname = "Doe"},
            new User{DeviceId="5555554444", Email= "foo.bar@foobar.com", Id = 554, Name="Foo", Surname = "Bar"}
        };

        private static readonly Random _random = new Random();

        public static string RandomString()
        {
            int len = 16;
            string chars = "0123456789abcdef";
            StringBuilder sb = new StringBuilder(len);
            for (int i = 0; i < len; i++)
            {
                sb.Append(chars[_random.Next(chars.Length)]);
            }
            return sb.ToString();
        }



    }
}