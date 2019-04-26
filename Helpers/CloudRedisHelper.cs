﻿using Base.Helpers;
using CloudKeyFileProvider;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CloudRedis.Helpers
{
    internal class CloudRedisHelper
    {
        private readonly IConfiguration _configuration;

        private readonly string jsonKey = "";
        private GoogleCredential googleCredential;

        public CloudRedisHelper(IConfiguration configuration)
        {
            this._configuration = configuration;
            jsonKey = KeyProvider.GetCloudKey(_configuration["KeyFilesSetting:KeyFileName:Redis"], KeyType.GoogleCloudRedis);
        }

        public GoogleCredential GetGoogleCredential()
        {
            try
            {
                googleCredential = GoogleCredential.FromJson(jsonKey);
                return googleCredential;
            }
            catch (Exception ex)
            {
                #region Error Catch
                Logger.PringDebug($"-----Error{ System.Reflection.MethodBase.GetCurrentMethod().Name }-----");
                Logger.PringDebug(ex.ToString());
                return null;
                #endregion
            }
        }
    }
}
