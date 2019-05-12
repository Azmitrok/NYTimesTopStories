using Nancy;
using NYTimesTopStoriesAPI.Models;
using NYTimesTopStoriesAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NYTimesTopStoriesAPI.Modules
{
    public class NYTimesTopStoriesModule : NancyModule
    {
        ISourceAPIService _sourceApiService;

        public NYTimesTopStoriesModule()
        {
            //TODO use DI
            _sourceApiService = new NYTimesAPIService();

            Get("/", _ => "Ento moe!!");            


            Get("/list/{section}", parameters =>
            {
                IEnumerable<ArticleView> result = _sourceApiService.GetListBySectionAsync(parameters.section).Result;
                var res = Response.AsJson(result);
                return res;
            });



            Get("/about", _ => "About - Ento moe!!");



        }

        private dynamic GetListBySection(dynamic parameters)
        {
            var test = new
            {
                Name = "Peter Shaw",
                Twitter = "shawty_ds",
                Occupation = "Software Developer"
            };

            return Response.AsJson(test);
        }
    }
}
