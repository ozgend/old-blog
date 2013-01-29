using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cronom.Demo.WebApiService.Repository.Interfaces
{
    public interface ICacheRepository
    {

        void Add(string key, object value);
        T Get<T>(string key);
        T Get<T>(string key, T @default);
        void Remove(string key);

    }
}