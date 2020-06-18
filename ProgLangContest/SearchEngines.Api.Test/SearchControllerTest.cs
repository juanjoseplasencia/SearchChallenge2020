using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using SearchEngines.Api.Services.Contracts;
using SearchEngines.Api.Controllers;

namespace SearchEngines.Api.Test
{
    [TestClass]
    public class SearchControllerTest : BaseTest
    {
        [TestMethod]
        public void Search_HappyPath()
        {
            SearchTerm = "java";
            SearchEnginesTestService = new MockSearchEnginesService(SearchEnginesTestList);
            SearchController searchController = new SearchController(SearchEnginesTestService);
            var result = searchController.BuildResults(SearchTerm);

            Assert.IsTrue(result.Values.Count > 0, "There were no results from search.");
        }

        [TestMethod]
        public void Search_SearchEngineNames_Empty()
        {
            SearchTerm = "java";
            SearchEnginesTestList = new List<ISearchEngine>();
            SearchEnginesTestService = new MockSearchEnginesService(SearchEnginesTestList);
            SearchController searchController = new SearchController(SearchEnginesTestService);
            var result = searchController.BuildResults(SearchTerm);
            
            Assert.IsFalse(result.Values.Count > 0, "There were results from search");
        }

        [TestMethod]
        public void Search_SearchTerm_Empty()
        {
            SearchTerm = string.Empty;
            SearchEnginesTestService = new MockSearchEnginesService(SearchEnginesTestList);
            SearchController searchController = new SearchController(SearchEnginesTestService);
            var result = searchController.BuildResults(SearchTerm);

            Assert.IsFalse(result.Values.Any(V => V > 0), "There were results from search");
        }

        [TestMethod]
        public void Search_SearchTerm_Null()
        {
            SearchEnginesTestService = new MockSearchEnginesService(SearchEnginesTestList);
            SearchController searchController = new SearchController(SearchEnginesTestService);
            var result = searchController.BuildResults(null);

            Assert.IsFalse(result.Values.Any(V => V > 0), "There were results from search");
        }

    }
}
