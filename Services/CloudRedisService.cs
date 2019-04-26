using CloudRedis.Helpers;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Redis.V1;
using Grpc.Auth;
using Grpc.Core;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudRedis.Services
{
    public class CloudRedisService
    {
        private readonly IConfiguration configuration;
        private readonly GoogleCredential googleCredential;
        private readonly Channel channel;

        public CloudRedisService(IConfiguration configuration)
        {
            this.configuration = configuration;
            googleCredential = new CloudRedisHelper(configuration).GetGoogleCredential();

            channel = new Channel(
                CloudRedisClient.DefaultEndpoint.Host, CloudRedisClient.DefaultEndpoint.Port, googleCredential.ToChannelCredentials());
        }

        public async Task<Instance> CloudHealthAsync()
        {
            CloudRedisClient cloudRedisClient = CloudRedisClient.Create(channel);
            // Initialize request argument(s)
            InstanceName name = new InstanceName(
                configuration["GCPSetting:PROJECTNAME"],
                configuration["GCPSetting:LOCATIONNAME"],
                configuration["GCPSetting:INSTANCES:RedisINSTANCES"]
                );

            // Make the request
            Instance response = await cloudRedisClient.GetInstanceAsync(name);

            if (response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }

        ~CloudRedisService()
        {
            channel.ShutdownAsync().Wait();
        }
    }
}
