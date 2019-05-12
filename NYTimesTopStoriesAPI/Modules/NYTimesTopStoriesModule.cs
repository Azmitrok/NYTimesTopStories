using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
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
        private ISourceAPIService _sourceApiService;

        public NYTimesTopStoriesModule(IApiProvider apiProvider)
        {
            _sourceApiService = new NYTimesAPIService(apiProvider);

            Get("/", _ =>
            {
                try
                {
                    return Response.AsJson(_sourceApiService.GetAPIStatus());
                }
                catch (Exception exc)
                {
                    if (exc.InnerException != null)
                        return Response.AsJson(exc.InnerException.Message);
                    return Response.AsJson(exc.Message);
                }

            });


            Get("/list/{section}", parameters =>
            {
                try
                {
                    IEnumerable<ArticleView> result = _sourceApiService.GetListBySectionAsync(parameters.section).Result;
                    return Response.AsJson(result);
                }
                catch (Exception exc)
                {
                    if (exc.InnerException != null)
                        return Response.AsJson(exc.InnerException.Message);
                    return Response.AsJson(exc.Message);
                }
            });

            Get("/list/{section}/first", parameters =>
            {
                try
                {
                    ArticleView result = _sourceApiService.GetListBySectionFirstAsync(parameters.section).Result;
                    return Response.AsJson(result);
                }
                catch (Exception exc)
                {
                    if (exc.InnerException != null)
                        return Response.AsJson(exc.InnerException.Message);
                    return Response.AsJson(exc.Message);
                }
            });

            // contraint date using regex \d{4}-\d{2}-\d{2}
            Get("/list/{section}/{updatedDate:date}", parameters =>
            {
                try
                {
                    IEnumerable<ArticleView> result = _sourceApiService.GetListBySectionByUpdatedDateAsync(parameters.section, parameters.updatedDate).Result;
                    return Response.AsJson(result);
                }
                catch (Exception exc)
                {
                    if (exc.InnerException != null)
                        return Response.AsJson(exc.InnerException.Message);
                    return Response.AsJson(exc.Message);
                }
            });

            Get("/article/{shortUrl}", parameters =>
            {
                try
                {
                    ArticleView result = _sourceApiService.GetArticleByShortUrlAsync(parameters.shortUrl).Result;
                    return Response.AsJson(result);
                }
                catch (Exception exc)
                {
                    if (exc.InnerException != null)
                        return Response.AsJson(exc.InnerException.Message);
                    return Response.AsJson(exc.Message);
                }
            });

            Get("/group/{section}", parameters =>
            {
                try
                {
                    IEnumerable<ArticleGroupByDateView> result = _sourceApiService.GetGroupsBySectionAsync(parameters.section).Result;
                    return Response.AsJson(result);
                }
                catch (Exception exc)
                {
                    if (exc.InnerException != null)
                        return Response.AsJson(exc.InnerException.Message);
                    return Response.AsJson(exc.Message);
                }
            });

            Get("/about", _ => "The purpose of this exercise is check the candidate’s ability to create a standalone RESTful API web application using the Nancy framework for .Net. ");



        }
    }
}
