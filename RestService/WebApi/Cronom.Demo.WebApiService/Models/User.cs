using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cronom.Demo.WebApiService.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string DeviceId { get; set; }

    }
}