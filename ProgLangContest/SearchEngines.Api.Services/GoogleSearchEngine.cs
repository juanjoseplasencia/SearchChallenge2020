using System;
using System.Net.Http;
using SearchEngines.Api.Services.Contracts;

namespace SearchEngines.Api.Services
{
    [Serializable]
    public class GoogleSearchEngine : SearchEngine, ISearchEngine
    {
        private const string cx = "016891076219559894979:t8wudto_5c4";
        private const string apiKey = "AIzaSyCljwuNw21-HYycWms6jdSdGOB8KR6R5CU";

        public GoogleSearchEngine(string name, string url) : base(name, url)
        {
        }
        public GoogleSearchEngine() : this("Google", $"https://customsearch.googleapis.com/customsearch/v1?key={apiKey}&cx={cx}&q=")
        {
        }

        public override long Search(string searchTerm)
        {
            long resultsAsLong = 0;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(Url + searchTerm)
                };
                HttpResponseMessage response = client.GetAsync(Url + searchTerm).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (result.IndexOf("totalResults") != -1) {
                        int keyPositionBefore = result.IndexOf("totalResults") + 16;
                        string remainingPart = result.Substring(keyPositionBefore);
                        if (remainingPart.IndexOf("\",") != -1)
                        {
                            int keyPositionAfter = remainingPart.IndexOf("\",");
                            string resultsCounter = remainingPart.Substring(0, keyPositionAfter);
                            resultsAsLong = Convert.ToInt64(resultsCounter
                                .Replace(",", string.Empty)
                                .Replace(".", string.Empty));
                        }
                    }
                }
            }
            return resultsAsLong;
        }

    }
}
