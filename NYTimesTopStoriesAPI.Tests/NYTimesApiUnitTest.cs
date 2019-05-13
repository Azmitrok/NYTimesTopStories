using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using NYTimesTopStoriesAPI.Models;
using NYTimesTopStoriesAPI.Services;

namespace NYTimesTopStoriesAPI.Tests
{
    [TestClass]
    public class NYTimesApiUnitTest
    {
        private ISourceAPIService _service;

        private JArray _sourceJArrayResults;

        [TestInitialize]
        public void Initialize()
        {
            _service = new NYTimesAPIService(new TestApiProvider());

            _sourceJArrayResults = (JArray)JObject.Parse(File.ReadAllText("Data/TestData.json"))["results"];
        }


        [TestMethod]
        public void ApiStatusTest()
        {
            Assert.AreEqual("status:\"OK\"", _service.GetAPIStatus());
        }

        [TestMethod]
        public void GetListBySectionTest()
        {
            IEnumerable<ArticleView> articles = _service.GetListBySectionAsync("home").Result;            

            Assert.AreEqual(_sourceJArrayResults.Count, articles.Count());
        }

        [TestMethod]
        public void GetListBySectionFirstTest()
        {
            ArticleView article = _service.GetListBySectionFirstAsync("home").Result;

            Assert.AreEqual(_sourceJArrayResults.First.Value<string>("title"), article.Heading);
        }

        [TestMethod]
        public void GetListBySectionByUpdatedDateTest()
        {
            IEnumerable<ArticleView> articles = _service.GetListBySectionByUpdatedDateAsync("home", "2019-05-10").Result;            

            Assert.AreEqual(8, articles.Count());
        }
        
        [TestMethod]
        public void GetArticleByShortUrlTest()
        {
            ArticleView article = _service.GetArticleByShortUrlAsync("2vU1134").Result;

            Assert.AreEqual("Russia Is Targeting Europe\u2019s Elections. So Are Far-Right Copycats.", article.Heading);
        }

        [TestMethod]
        public void GetGroupsBySectionTest()
        {
            IEnumerable<ArticleGroupByDateView> articlesGroups = _service.GetGroupsBySectionAsync("home").Result;

            Assert.AreEqual(5, articlesGroups.Count());

            Assert.AreEqual(8, articlesGroups.First(g => g.Date == "10.05.2019").Total);
        }


    }
}
