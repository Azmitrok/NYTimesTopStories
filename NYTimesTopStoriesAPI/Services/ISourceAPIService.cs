using NYTimesTopStoriesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NYTimesTopStoriesAPI.Services
{
    interface ISourceAPIService
    {
        bool IsAPIWorking();
        Task<IEnumerable<ArticleView>> GetListBySectionAsync(string section);
        ArticleView GetListBySectionFirst(string section);
        IEnumerable<ArticleView> GetListBySectionByUpdatedDate(string section, string updatedDate);
        ArticleView GetArticleByShortUrl(string section);
        IEnumerable<ArticleGroupByDateView> GetGroupsBySection(string section);


    }
}
