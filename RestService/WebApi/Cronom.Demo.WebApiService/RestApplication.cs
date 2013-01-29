using Cronom.Demo.WebApiService.Helper;
using Cronom.Demo.WebApiService.Repository;
using Cronom.Demo.WebApiService.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cronom.Demo.WebApiService
{
    public class RestApplication
    {

        public static ICacheRepository Cache
        {
            get { return IOCContainer.Instance.Create<ICacheRepository>(); }
        }


        public static void Start()
        {
            IOCContainer.Instance.Register<ICacheRepository>(
                delegate
                {
                    return new CacheRepository();
                },
                IOCContainer.InitializationTypes.Singleton
            );
        }

    }
}