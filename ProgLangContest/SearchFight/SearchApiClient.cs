using System;
using System.Net.Http;

namespace SearchFight
{
    public class SearchApiClient
    {
        public string Url { get; set; }
        public HttpClient Client { get; }
        public SearchApiClient(string url)
        {
            Client = new HttpClient
            {
                BaseAddress = new Uri(url)
            };
            Url = url;
        }
    }
}
