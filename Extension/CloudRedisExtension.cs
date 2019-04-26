using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudRedis.Extension
{
    public static class CloudRedisExtension
    {
        /// <summary>
        /// add Redis
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddKTRedis(this IServiceCollection services)
        {
            services.AddDistributedRedisCache(o =>
            {
                o.Configuration = "localhost";
            });
            return services;
        }
    }
}
