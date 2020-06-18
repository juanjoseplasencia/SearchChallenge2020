using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SearchEngines.Api.Services.Contracts;

namespace SearchEngines.Api.Test
{
    public class BaseTest
    {
        public TestContext TestContext { get; set; }
        public string SearchTerm { get; set; }
        public ISearchEnginesService SearchEnginesTestService { get; set; }
        public List<ISearchEngine> SearchEnginesTestList { get; set; }

        private void BuildSearchEnginesTestList()
        {
            SearchEnginesTestList = new List<ISearchEngine>();
            SearchEnginesTestList.Add(new MockSearchEngineA("A"));
            SearchEnginesTestList.Add(new MockSearchEngineB("B"));
        }

        [TestInitialize]
        public void TestInitialize() {
            BuildSearchEnginesTestList();
        }
    }

    public class MockSearchEnginesService : ISearchEnginesService
    {
        public readonly List<ISearchEngine> SearchEnginesList;
        public MockSearchEnginesService(List<ISearchEngine> searchEnginesList) {
            SearchEnginesList = searchEnginesList;
        }
        public List<ISearchEngine> GetSearchEngines()
        {
            return SearchEnginesList;
        }
    }

    public class MockSearchEngineA : ISearchEngine {

        public MockSearchEngineA(string name)
        {
            Name = name;
        }

        public string Name { get;}

        public long Search(string searchTerm)
        {
            return 0;
        }
    }

    public class MockSearchEngineB : ISearchEngine
    {

        public MockSearchEngineB(string name)
        {
            Name = name;
        }

        public string Name { get;}

        public long Search(string searchTerm)
        {
            return 0;
        }
    }

}
