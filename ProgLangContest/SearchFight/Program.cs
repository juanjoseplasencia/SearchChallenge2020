using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchFight
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:50938/api/search/";
            List<string> theProgLangs = args.ToList();
            Console.WriteLine();
            Console.Write("The results of programming languages search fight are: ");
            Console.WriteLine();
            SearchApiClient apiClient = new SearchApiClient(url);
            SearchManager searchManager = new SearchManager();
            try
            {
                var fullResults = searchManager.BuildResults(theProgLangs, apiClient);
                if (fullResults.Count > 0)
                {
                    ShowResults(fullResults);
                    Console.WriteLine();
                    ShowWinnersBySearchEngine(searchManager.BuildResultsBySearchEngine(fullResults));
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("No programming languages were provided for searching.");
                }
            }
            catch
            {
                Console.WriteLine();
                Console.WriteLine("An error ocurred during the search contest");
            }

            Console.ReadLine();
        }

        private static void ShowResults(Dictionary<string, Dictionary<string, long>> results)
        {
            int total = results.Count;
            foreach (var result in results)
            {
                Console.Write(results.Count - total + 1);
                Console.Write(" ");
                Console.Write(result.Key);
                Console.Write(":");
                foreach (var value in result.Value.Where(elem => elem.Key != "Total"))
                {
                    Console.Write($" {value.Key}: {value.Value}");
                }
                total--;
                Console.WriteLine();
            }
        }

        private static void ShowWinnersBySearchEngine(Dictionary<string, string> results)
        {
            foreach (var result in results)
            {
                Console.WriteLine($"{result.Key} winner : {result.Value}");
            }
        }

    }
}
