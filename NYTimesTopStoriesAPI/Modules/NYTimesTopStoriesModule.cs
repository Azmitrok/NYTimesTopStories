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

            Get("/", _ => Response.AsJson(_sourceApiService.GetAPIStatus()));


            Get("/list/{section}", parameters =>
            {
                IEnumerable<ArticleView> result = _sourceApiService.GetListBySectionAsync(parameters.section).Result;
                return Response.AsJson(result);
            });

            Get("/list/{section}/first", parameters =>
            {
                ArticleView result = _sourceApiService.GetListBySectionFirstAsync(parameters.section).Result;
                return Response.AsJson(result);
            });

            // contraint date using regex \d{4}-\d{2}-\d{2}
            Get("/list/{section}/{updatedDate:date}", parameters =>
            {
                IEnumerable<ArticleView> result = _sourceApiService.GetListBySectionByUpdatedDateAsync(parameters.section, parameters.updatedDate).Result;
                return Response.AsJson(result);
            });

            Get("/article/{shortUrl}", parameters =>
            {
                ArticleView result = _sourceApiService.GetArticleByShortUrlAsync(parameters.shortUrl).Result;
                return Response.AsJson(result);
            });
            
            Get("/group/{section}", parameters =>
            {
                IEnumerable<ArticleGroupByDateView> result = _sourceApiService.GetGroupsBySectionAsync(parameters.section).Result;
                return Response.AsJson(result);
            });

            Get("/about", _ => "The purpose of this exercise is check the candidate’s ability to create a standalone RESTful API web application using the Nancy framework for .Net. ");



        }
    }
}
