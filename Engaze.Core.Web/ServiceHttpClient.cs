using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Engaze.Core.Web
{
    public class ServiceHttpClient
    {
        public static async Task<string> Get(string endpointUrl)
        {
            using (var client = new HttpClient())
            {
              
                ////MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                ////client.DefaultRequestHeaders.Accept.Add(contentType);
                //HTTP GET
                var response = await client.GetAsync(endpointUrl);

                if (response.IsSuccessStatusCode)
                {
                    string stringData = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(stringData))
                    {
                        return null;
                    }

                    return stringData;
                }

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                throw new Exception($"Call to the service {response.RequestMessage.RequestUri} " +
                    $"failed with the status code : {response.StatusCode} and reason phrase :{ response.ReasonPhrase}");

            }
        }
    }
}

