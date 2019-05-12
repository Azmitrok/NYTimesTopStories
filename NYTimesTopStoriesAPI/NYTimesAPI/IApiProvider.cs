using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NYTimesTopStoriesAPI
{
    public interface IApiProvider
    {
        Task<string> GetArticlesBySectionContent(string section);
    }
}
