﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace NYTimesTopStoriesAPI.NYTimesAPI
{
    public class NYTimesApiProvider
    {
        private const string TopStoriesBySectionUrlTemplate = "https://api.nytimes.com/svc/topstories/v2/{0}.json?api-key={1}";

        private readonly string _apiKey;

        public NYTimesApiProvider(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<string> GetArticlesBySection(string section)
        {            
            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(String.Format(TopStoriesBySectionUrlTemplate, section, _apiKey));
                
            }
        }
    }
}
