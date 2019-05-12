using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NYTimesTopStoriesAPI.Models;
using NYTimesTopStoriesAPI.NYTimesAPI;

namespace NYTimesTopStoriesAPI.Services
{
    public class NYTimesAPIService : ISourceAPIService
    {
        NYTimesApiProvider _apiProvider;

        private const string ApiKey = "46837SflDHAIiwq3sclJnOmqtAfbp5Xr";

        public NYTimesAPIService()
        {
            _apiProvider = new NYTimesApiProvider(ApiKey);
        }

        public ArticleView GetArticleByShortUrl(string section)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ArticleGroupByDateView> GetGroupsBySection(string section)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ArticleView>> GetListBySectionAsync(string section)
        {
            var content = await _apiProvider.GetArticlesBySection(section);

            return MapJsonResultToArticleViews((JArray)JObject.Parse(content)["results"]);
        }

        private IEnumerable<ArticleView> MapJsonResultToArticleViews(JArray jArray)
        {            
            foreach (var jObject in jArray)
            {
                yield return new ArticleView
                {
                    Heading = jObject.Value<string>("title"),
                    Link = jObject.Value<string>("url"),
                    Updated = jObject.Value<DateTime>("updated_date")
                };                
            }            
        }

        public IEnumerable<ArticleView> GetListBySectionByUpdatedDate(string section, string updatedDate)
        {
            throw new NotImplementedException();
        }

        public ArticleView GetListBySectionFirst(string section)
        {
            throw new NotImplementedException();
        }

        public bool IsAPIWorking()
        {
            throw new NotImplementedException();
        }
    }
}
