using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SearchEngines.Api.Services.Contracts;

namespace SearchEngines.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchEnginesService _searchEnginesService;

        public SearchController(ISearchEnginesService searchEnginesService)
        {
            _searchEnginesService = searchEnginesService;
        }

        /// <summary>
        /// Performs the search for a search term (programming language name) over the list of search engines
        /// </summary>
        /// <param name="searchEngines">List of search engines</param>
        /// <param name="searchTerm">Term to search for</param>
        /// <returns>Results of search by search engine</returns>
        private Dictionary<string, long> Search(List<ISearchEngine> searchEngines, string searchTerm)
        {
            Dictionary<string, long> results = new Dictionary<string, long>();
            if (searchEngines.Count > 0 && !string.IsNullOrEmpty(searchTerm))
            {
                foreach (var searchEngine in searchEngines)
                {
                    long result = searchEngine.Search(searchTerm);
                    results.Add(searchEngine.Name, result);
                }
                results.Add("Total", results.Sum(item => item.Value));
            }
            return results;
        }

        /// <summary>
        /// Builds the list of search results for all the  search engines.
        /// </summary>
        /// <param name="progLang">Programming language to search for</param>
        /// <returns>Results of search by programming language</returns>
        public Dictionary<string, long> BuildResults(string progLang)
        {
            var searchEngines = _searchEnginesService.GetSearchEngines();
            return Search(searchEngines, progLang);
        }

        // GET api/search/java
        [Route("{searchTerm}")]
        [HttpGet]
        public JsonResult GetSearchResults(string searchTerm)
        {
            return new JsonResult(BuildResults(searchTerm));
        }

        // GET api/search
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Api", "Running" };
        }

    }
}
