using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NYTimesTopStoriesAPI.Models;


namespace NYTimesTopStoriesAPI.Services
{
    public class NYTimesAPIService : ISourceAPIService
    {
        NYTimesApiProvider _apiProvider;

        //private const string ApiKey = "46837SflDHAIiwq3sclJnOmqtAfbp5Xr";
        private const string DefaultSectionName = "home";

        public NYTimesAPIService()
        {            
            _apiProvider = new NYTimesApiProvider(ConfigurationManager.AppSettings["NYTimesApiKey"]);
        }

        public async Task<ArticleView> GetArticleByShortUrlAsync(string shortUrl)
        {
            var content = await _apiProvider.GetArticlesBySection(DefaultSectionName);            

            var jTokenList = ((JArray)JObject.Parse(content)["results"]).Where(j => GetLastSevenChars(j.Value<string>("short_url")) == shortUrl);

            return MapJsonResultToArticleViews(jTokenList).FirstOrDefault();
        }        

        public async Task<IEnumerable<ArticleGroupByDateView>> GetGroupsBySectionAsync(string section)
        {
            var content = await _apiProvider.GetArticlesBySection(section);

            var jTokens = ((JArray)JObject.Parse(content)["results"]).ToObject<JToken[]>();

            return jTokens.GroupBy(j => j.Value<DateTime>("updated_date").Date.ToShortDateString()).Select(g => new ArticleGroupByDateView { Date = g.Key, Total = g.Count() });
        }

        public async Task<IEnumerable<ArticleView>> GetListBySectionAsync(string section)
        {
            var content = await _apiProvider.GetArticlesBySection(section);            

            return MapJsonResultToArticleViews(((JArray)JObject.Parse(content)["results"]).ToObject<JToken[]>());
        }

        private IEnumerable<ArticleView> MapJsonResultToArticleViews(IEnumerable<JToken> jTokenList)
        {
            foreach (var jToken in jTokenList)
            {
                yield return new ArticleView
                {
                    Heading = jToken.Value<string>("title"),
                    Link = jToken.Value<string>("url"),
                    Updated = jToken.Value<DateTime>("updated_date")
                };
            }
        }

        public async Task<IEnumerable<ArticleView>> GetListBySectionByUpdatedDateAsync(string section, string updatedDate)
        {
            var list = await GetListBySectionAsync(section);

            var date = DateTime.Parse(updatedDate);

            return list.Where(a => a.Updated.Date == date);
        }

        public async Task<ArticleView> GetListBySectionFirstAsync(string section)
        {
            var list = await GetListBySectionAsync(section);

            return list.FirstOrDefault();
        }

        public string GetAPIStatus()
        {
            var statusResult = JObject.Parse(_apiProvider.GetArticlesBySection(DefaultSectionName).Result).Value<string>("status");

            return $"status:\"{statusResult}\"";
        }

        #region Private

        private string GetLastSevenChars(string str)
        {
            return str.Substring(str.Length - 7);
        }        

        #endregion
    }
}
