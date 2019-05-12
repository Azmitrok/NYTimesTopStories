using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYTimesTopStoriesAPI.Tests
{
    public class TestApiProvider : IApiProvider
    {
        public Task<string> GetArticlesBySectionContent(string section)
        {
            return Task.Run(() =>
            {
                return File.ReadAllText("Data/TestData.json");
            });
        }
    }
}
