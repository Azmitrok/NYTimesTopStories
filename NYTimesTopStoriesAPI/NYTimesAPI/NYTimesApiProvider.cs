using Newtonsoft.Json;
using NYTimesTopStoriesAPI.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace NYTimesTopStoriesAPI
{
    public class NYTimesApiProvider : IApiProvider
    {
        private const string TopStoriesBySectionUrlTemplate = "https://api.nytimes.com/svc/topstories/v2/{0}.json?api-key={1}";

        private readonly string _apiKey;

        public NYTimesApiProvider()
        {
            _apiKey = ConfigurationManager.AppSettings["NYTimesApiKey"];
        }

        public async Task<string> GetArticlesBySectionContent(string section)
        {                        
            using (var client = new HttpClient())
            {
                try
                {
                    return await client.GetStringAsync(String.Format(TopStoriesBySectionUrlTemplate, section, _apiKey));
                }
                catch(Exception exc)
                {
                    throw new ApiException( exc.Message );
                }
                
                
            }
        }
    }
}
