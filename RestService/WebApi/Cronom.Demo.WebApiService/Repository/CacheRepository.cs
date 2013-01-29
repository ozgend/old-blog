using Cronom.Demo.WebApiService.Models;
using Cronom.Demo.WebApiService.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace Cronom.Demo.WebApiService.Repository
{
    public class CacheRepository : ICacheRepository
    {

        private Cache cache
        {
            get
            {
                return HttpContext.Current.Cache;
            }
        }

        public void Add(string key, object value)
        {
            if (cache[key] == null)
            {
                cache.Insert(key, value);
            }
            else
            {
                cache[key] = value;
            }
        }

        public T Get<T>(string key)
        {
            return Get(key, default(T));
        }

        public T Get<T>(string key, T @default)
        {
            var value = cache.Get(key);
            if (value == null)
            {
                return @default;
            }
            return (T)value;
        }

        public void Remove(string key)
        {
            if (cache[key] != null)
            {
                cache.Remove(key);
            }
        }

    }
}