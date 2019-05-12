using NYTimesTopStoriesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NYTimesTopStoriesAPI.Services
{
    public interface ISourceAPIService
    {
        string GetAPIStatus();
        Task<IEnumerable<ArticleView>> GetListBySectionAsync(string section);
        Task<ArticleView> GetListBySectionFirstAsync(string section);
        Task<IEnumerable<ArticleView>> GetListBySectionByUpdatedDateAsync(string section, string updatedDate);
        Task<ArticleView> GetArticleByShortUrlAsync(string shortUrl);
        Task<IEnumerable<ArticleGroupByDateView>> GetGroupsBySectionAsync(string section);


    }
}
