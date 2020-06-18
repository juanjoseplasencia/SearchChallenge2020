using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;

namespace SearchFight
{

    public class SearchManager
    {
        /// <summary>
        /// Builds the list of search results for all the programming languages.
        /// </summary>
        /// <param name="progLangs">List of programming languages for searching and building the search results</param>
        /// <param name="searchApiClient">Proxy Http client to invoke Search Api</param>
        /// <returns>Results of search by programming language</returns>
        public Dictionary<string, Dictionary<string, long>> BuildResults(List<string> progLangs, SearchApiClient searchApiClient)
        {
            Dictionary<string, Dictionary<string, long>> fullResults = new Dictionary<string, Dictionary<string, long>>();
            try
            {
                foreach (var progLang in progLangs)
                {
                    HttpResponseMessage response = searchApiClient.Client.GetAsync(searchApiClient.Url + progLang).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        var progLangResult = JsonConvert.DeserializeObject<Dictionary<string, long>>(result);
                        fullResults.Add(progLang, progLangResult);
                    }
                }

            }
            catch 
            {
                throw;
            }

            return fullResults;
        }

        /// <summary>
        /// Organizes the results of search by search engine to rank which programming language has the highest count of results and show it
        /// </summary>
        /// <param name="fullResults">Search results by programming language</param>
        /// <returns>Results of search grouped by search engine</returns>
        public Dictionary<string,string> BuildResultsBySearchEngine(Dictionary<string, Dictionary<string, long>> fullResults) {
            List<string> searchEngineNames = fullResults.First().Value.Keys.ToList();
            Dictionary<string, string> results = new Dictionary<string, string>();
            foreach (var item in searchEngineNames)
            {
                var resultsBySearchEngine = fullResults.Where(R => R.Value.Keys.Contains(item)).ToDictionary(R => R.Key, R => R.Value[item]);
                if (resultsBySearchEngine.Any()) { 
                    long languageWinner = resultsBySearchEngine.Values.Max();
                    results.Add(item, resultsBySearchEngine.First(R => R.Value == languageWinner).Key);
                }
            }
            return results;
        }

    }
}
