using System.Collections.Generic;

namespace SearchEngines.Api.Services.Contracts
{
    public interface ISearchEnginesService
    {
        List<ISearchEngine> GetSearchEngines();
    }
}
