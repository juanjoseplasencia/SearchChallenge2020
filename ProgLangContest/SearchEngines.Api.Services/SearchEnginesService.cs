using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SearchEngines.Api.Services.Contracts;

namespace SearchEngines.Api.Services
{
    public class SearchEnginesService : ISearchEnginesService
    {
        public List<ISearchEngine> GetSearchEngines() {
            const string SearchEngineBaseName = "SearchEngine";
            List<ISearchEngine> searchEngines = new List<ISearchEngine>();
            string assemblyPath = AppDomain.CurrentDomain.BaseDirectory + "SearchEngines.Api.Services.dll";

            Assembly assembly;
            assembly = Assembly.LoadFrom(assemblyPath);
            var searchEnginesFromService = assembly.DefinedTypes.Where(T => T.ImplementedInterfaces.Any()
            && T.BaseType.Name == SearchEngineBaseName);

            foreach (var searchEngineItem in searchEnginesFromService)
            {
                ISearchEngine searchEngine;
                searchEngine = (ISearchEngine)assembly.CreateInstance(searchEngineItem.FullName);
                if (searchEngine != null)
                    searchEngines.Add(searchEngine);
            }
            return searchEngines;
        }
    }
}
