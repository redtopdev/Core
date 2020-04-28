using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Engaze.Core.Web
{
    public class RemoteConfiguration
    {
        public static void Initialize(IConfiguration config, bool suppressRemoteConfigError = false)
        {
            try
            {
                var remoteConfigurationUrl = config.GetValue<string>("RemoteConfigurationUrl");
                if (!Uri.IsWellFormedUriString(remoteConfigurationUrl, UriKind.Absolute))
                {
                    throw new Exception("Valid RemoteConfigurationUrl not found");
                }
                var encodedResult = ServiceHttpClient.Get(remoteConfigurationUrl).Result;

                var base64EncodedBytes = Convert.FromBase64String(encodedResult);

                var remoteConfigStr =  System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

                System.IO.File.WriteAllText("RemoteConfiguration.json", remoteConfigStr);

                var newconfigDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(remoteConfigStr);
                foreach (string key in newconfigDict.Keys)
                {
                    config[key] = newconfigDict[key].ToString();
                }
            }
            catch (Exception ex)
            {
                if (!suppressRemoteConfigError)
                {
                    throw;
                }
            }
        }
    }
}
